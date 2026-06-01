using UnityEngine;
using System.Collections;
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
    public float tiempoSpawnPolicia = 30f;

    public Transform jugador;
    private List<GameObject> npcsActivos = new List<GameObject>();

    void Start()
    {
        GameObject objJugador = GameObject.FindGameObjectWithTag("Negro");
        if (objJugador != null)
        {
            jugador = objJugador.transform;
            InvokeRepeating("GenerarPolicia", tiempoSpawnPolicia, tiempoSpawnPolicia);
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto con el tag 'Negro' en la escena.");
        }
    }

    void Update()
    {
        if (jugador == null) return;

        npcsActivos.RemoveAll(item => item == null);

        if (npcsActivos.Count < maxNpcs)
        {
            GenerarNpc();
        }
    }

    void GenerarNpc()
    {
        Vector3 posicion = ObtenerPosicionAleatoria();
        GameObject nuevoNpc = Instantiate(npcPrefab, posicion, Quaternion.identity);
        npcsActivos.Add(nuevoNpc);
    }

    void GenerarPolicia()
    {
        if (jugador == null) return;
        Vector3 posicion = ObtenerPosicionAleatoria();
        Instantiate(policiaPrefab, posicion, Quaternion.identity);
    }

    Vector3 ObtenerPosicionAleatoria()
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0; 
        randomDirection = randomDirection.normalized;

        float dist = Random.Range(radioSpawnMin, radioSpawnMax);
        return jugador.position + (randomDirection * dist);
    }

    void OnDrawGizmosSelected()
    {
        Transform tempJugador = jugador;
        if (tempJugador == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Negro");
            if (obj != null) tempJugador = obj.transform;
        }

        if (tempJugador != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(tempJugador.position, radioSpawnMin);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(tempJugador.position, radioSpawnMax);
        }
    }
}