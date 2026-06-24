using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SpawnerManager : MonoBehaviour
{
    [Header("Configuración NPC")]
    public GameObject npcPrefab;
    public int maxNpcs = 10;
    public float radioSpawnMin = 5f;
    public float radioSpawnMax = 15f;

    [Header("Configuración Policía")]
    public GameObject policiaPrefab;
    public float tiempoSpawnPolicia = 15f;

    public float radioSpawnPoliciaMin = 20f;
    public float radioSpawnPoliciaMax = 35f;

    public Transform jugador;

    private List<GameObject> npcsActivos = new List<GameObject>();

    private int siguienteSpawnPolicia;

    void Start()
    {
        GameObject objJugador = GameObject.FindGameObjectWithTag("Negro");

        if (objJugador != null)
        {
            jugador = objJugador.transform;

            siguienteSpawnPolicia = Mathf.RoundToInt(tiempoSpawnPolicia);
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto con el tag 'Negro' en la escena.");
        }
    }

    void Update()
    {
        if (GameState.juegoPausado)
            return;

        if (jugador == null)
            return;

        npcsActivos.RemoveAll(item => item == null);

        if (npcsActivos.Count < maxNpcs)
        {
            GenerarNpc();
        }

        if (Timer.elapsedTime >= siguienteSpawnPolicia)
        {
            GenerarPolicia();

            siguienteSpawnPolicia += Mathf.RoundToInt(tiempoSpawnPolicia);
        }
    }

    void GenerarNpc()
    {
        Vector3 posicion = ObtenerPosicionAleatoria();

        GameObject nuevoNpc = Instantiate(npcPrefab, posicion, Quaternion.identity);

        if (DifficultyManager.dificultadActual == NivelDificultad.Dificil)
        {
            NPC npc = nuevoNpc.GetComponent<NPC>();

            if (npc != null && npc.estadoActual == EstadoNPC.Estatico)
            {
                npc.estadoActual = EstadoNPC.Alerta;
            }
        }

        npcsActivos.Add(nuevoNpc);
    }

    void GenerarPolicia()
    {
        if (jugador == null)
            return;

        Vector3 posicion =
            ObtenerPosicionAleatoriaPolicia();

        GameObject nuevaPolicia = Instantiate(policiaPrefab, posicion, Quaternion.identity);

        if (DifficultyManager.dificultadActual == NivelDificultad.Dificil)
        {
            NPCPolicia policia = nuevaPolicia.GetComponent<NPCPolicia>();

            if (policia != null)
            {
                policia.estadoActual = EstadoPolicia.Alerta;
            }
        }
    }

    Vector3 ObtenerPosicionAleatoria()
    {
        Vector3 randomDirection = Random.insideUnitSphere;

        randomDirection.y = 0;
        randomDirection = randomDirection.normalized;

        float dist = Random.Range(
            radioSpawnMin,
            radioSpawnMax);

        return jugador.position + (randomDirection * dist);
    }

    Vector3 ObtenerPosicionAleatoriaPolicia()
    {
        Vector3 randomDirection = Random.insideUnitSphere;

        randomDirection.y = 0;
        randomDirection = randomDirection.normalized;

        float dist = Random.Range(
            radioSpawnPoliciaMin,
            radioSpawnPoliciaMax);

        return jugador.position + (randomDirection * dist);
    }

    void OnDrawGizmosSelected()
    {
        Transform tempJugador = jugador;

        if (tempJugador == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Negro");

            if (obj != null)
                tempJugador = obj.transform;
        }

        if (tempJugador != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(
                tempJugador.position,
                radioSpawnMin);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                tempJugador.position,
                radioSpawnMax);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(
                tempJugador.position,
                radioSpawnPoliciaMin);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(
                tempJugador.position,
                radioSpawnPoliciaMax);
        }
    }
}