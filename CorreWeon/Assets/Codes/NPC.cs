using UnityEngine;
using System;

public class NPC : MonoBehaviour
{
    public float radio = 3f; 
    public float velocidad = 2f;
    bool TieneDinero = true;

    public float radioAlarma = 8f;
    public EstadoNPC estadoActual = EstadoNPC.Estatico;
    public static event Action OnDineroRobado;

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
        Debug.Log("AHHH que miedo yo me voy");
        Destroy(gameObject, 3f);
    }

    void EstadoEnojo()
    {
        Debug.Log("ven pa ca negro ql");
        Destroy(gameObject, 10f);
    }

    void EstadoLlamada()
    {
        Debug.Log("alo honorables fuerzas de la ley?");
        Destroy(gameObject, 5f);
    }

    void EstadoDecepcion()
    {
        Debug.Log("bueno la vida sigue");
        Destroy(gameObject, 2f);
    }
    void ActivarAlarma()
    {
        NPC[] npcs = FindObjectsByType<NPC>(FindObjectsSortMode.None);

        foreach (NPC npc in npcs)
        {
            if (npc == this)
                continue;

            float distancia = Vector3.Distance(
                transform.position,
                npc.transform.position
            );

            if (distancia <= radioAlarma)
            {
                Debug.Log(npc.gameObject.name + " pasó a ALERTA");
                npc.estadoActual = EstadoNPC.Alerta;
                Debug.Log("Distancia a " + npc.name + ": " + distancia);
            }
        }
    }
    void ElegirReaccion()
    {
        int total =
            probMiedo +
            probEnojo +
            probLlamada +
            probDecepcion;

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
}
