using UnityEngine;
using System;

public class NPC : MonoBehaviour
{
    public float radio = 3f; 
    public float velocidad = 2f;
    bool TieneDinero = true;

    [Header("Alarmas")]
    public float radioAlarmaNPC = 8f;
    public float radioAlarmaPolicia = 15f;

    public EstadoNPC estadoActual = EstadoNPC.Estatico;
    public static event Action OnDineroRobado;

    [Header("Miedo")]
    public float radioPanico = 20f;
    bool panicoActivado = false;

    [Header("Llamada")]
    public float tiempoLlamadaPolicia = 9f;
    public GameObject policiaPrefab;
    public Transform puntoLlamadaPolicia;

    private bool llamadaIniciada = false;
    float timerLlamada = 0f;
    bool llamadaRealizada = false;

    [Header("Enojo")]
    public float velocidadEnojo = 4f;

    [Header("Probabilidades")]

    [Range(0, 100)]
    public int probMiedo = 40;

    [Range(0, 100)]
    public int probEnojo = 25;

    [Range(0, 100)]
    public int probLlamada = 20;

    [Range(0, 100)]
    public int probDecepcion = 15;
    void Update()
    {
        switch (estadoActual)
        {
            case EstadoNPC.Estatico:
                EstadoEstatico();
                break;

            case EstadoNPC.Alerta:
                EstadoAlerta();
                break;

            case EstadoNPC.Miedo:
                EstadoMiedo();
                break;

            case EstadoNPC.Enojo:
                EstadoEnojo();
                break;

            case EstadoNPC.Llamada:
                EstadoLlamada();
                break;

            case EstadoNPC.Decepcion:
                EstadoDecepcion();
                break;
        }
    }
    void EstadoAlerta()
    {
        GameObject[] objetosNegros = GameObject.FindGameObjectsWithTag("Negro");

        if (TieneDinero == true)
        {
            foreach (GameObject objetoNegro in objetosNegros)
            {

                float distancia = Vector3.Distance(transform.position, objetoNegro.transform.position);


                if (distancia < radio)
                {

                    Vector3 direccion = (transform.position - objetoNegro.transform.position).normalized;

                    transform.position += direccion * velocidad * Time.deltaTime;

                    //Debug.Log("AHHH UN VENEKO CORRAN");
                }
            }
        }
    }
    void EstadoEstatico()
    {
        // No hace nada
    }

    void EstadoMiedo()
    {
        if (!panicoActivado)
        {
            panicoActivado = true;

            ActivarPanico();

            Debug.Log("AHHH que miedo yo me voy");

            Destroy(gameObject, 3f);
        }
    }

    void EstadoEnojo()
    {
        GameObject[] objetosNegros = GameObject.FindGameObjectsWithTag("Negro");

        foreach (GameObject objetoNegro in objetosNegros)
        {
            float distancia = Vector3.Distance(transform.position, objetoNegro.transform.position);

            if (distancia < radio)
            {
                Vector3 direccion = (objetoNegro.transform.position - transform.position).normalized;

                transform.position += direccion * velocidadEnojo * Time.deltaTime;
            }
        }

        Destroy(gameObject, 10f);
    }

    void EstadoLlamada()
    {
        if (llamadaRealizada)
            return;

        timerLlamada += Time.deltaTime;

        if (timerLlamada >= tiempoLlamadaPolicia)
        {
            llamadaRealizada = true;

            for (int i = 0; i < 3; i++)
            {
                Instantiate(policiaPrefab, puntoLlamadaPolicia.position, Quaternion.identity);
            }

            Debug.Log("Alo Honorables fuerzas de la ley?");

            Destroy(gameObject);
        }
    }

    void EstadoDecepcion()
    {
        Debug.Log("bueno la vida sigue");
        Destroy(gameObject, 2f);
    }

    void ActivarAlarma()
    {
        // ALERTAR NPCs
        NPC[] npcs = FindObjectsByType<NPC>(FindObjectsSortMode.None);

        foreach (NPC npc in npcs)
        {
            if (npc == this)
                continue;

            float distancia = Vector3.Distance(transform.position, npc.transform.position);

            if (distancia <= radioAlarmaNPC)
            {
                npc.estadoActual = EstadoNPC.Alerta;

                Debug.Log(npc.name + " recibió alerta de NPC");
            }
        }

        // ALERTAR POLICÍAS
        NPCPolicia[] policias =
            FindObjectsByType<NPCPolicia>(FindObjectsSortMode.None);

        foreach (NPCPolicia policia in policias)
        {
            float distancia = Vector3.Distance(
                transform.position,
                policia.transform.position);

            if (distancia <= radioAlarmaPolicia)
            {
                policia.estadoActual = EstadoPolicia.Alerta;

                Debug.Log(policia.name + " recibió alerta policial");
            }
        }
    }

    void ActivarPanico()
    {
        NPC[] npcs = FindObjectsByType<NPC>(FindObjectsSortMode.None);

        foreach (NPC npc in npcs)
        {
            if (npc == this)
                continue;

            float distancia = Vector3.Distance(transform.position, npc.transform.position);

            if (distancia <= radioPanico)
            {
                npc.estadoActual = EstadoNPC.Alerta;
            }
        }

        NPCPolicia[] policias = FindObjectsByType<NPCPolicia>(FindObjectsSortMode.None);

        foreach (NPCPolicia policia in policias)
        {
            float distancia = Vector3.Distance(transform.position, policia.transform.position);

            if (distancia <= radioPanico)
            {
                policia.estadoActual = EstadoPolicia.Alerta;
            }
        }
    }

    void ElegirReaccion()
    {
        int total = probMiedo + probEnojo + probLlamada + probDecepcion;

        int random = UnityEngine.Random.Range(0, total);

        if (random < probMiedo)
        {
            estadoActual = EstadoNPC.Miedo;
        }
        else if (random < probMiedo + probEnojo)
        {
            estadoActual = EstadoNPC.Enojo;
        }
        else if (random < probMiedo + probEnojo + probLlamada)
        {
            estadoActual = EstadoNPC.Llamada;
        }
        else
        {
            estadoActual = EstadoNPC.Decepcion;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Negro"))
        {
            TieneDinero = false;

            ActivarAlarma();

            OnDineroRobado?.Invoke();

            ElegirReaccion();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioAlarmaNPC);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radioAlarmaPolicia);
    }
}
