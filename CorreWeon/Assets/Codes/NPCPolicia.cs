using UnityEngine;

public class NPCPolicia : MonoBehaviour
{
    public float radio = 3f; 
    public float velocidad = 2f;
    public GameObject[] prediccion;
    Vector3 direccion;
    public float timeStep;

    public EstadoPolicia estadoActual = EstadoPolicia.Estatico;
    void Update()
    {
        if (DifficultyManager.dificultadActual == NivelDificultad.Dificil)
        {
            estadoActual = EstadoPolicia.Alerta;
        }

        switch (estadoActual)
        {
            case EstadoPolicia.Estatico:
                EstadoEstatico();
                break;

            case EstadoPolicia.Alerta:
                EstadoAlerta();
                break;
        }

        ActualizarPrediccion();
    }
    void EstadoEstatico()
    {
        // Quieto
    }
    void EstadoAlerta()
    {
        GameObject[] objetosNegros = GameObject.FindGameObjectsWithTag("Negro");

        foreach (GameObject objetoNegro in objetosNegros)
        {
            float distancia = Vector3.Distance(
                transform.position,
                objetoNegro.transform.position);

            if (distancia < radio)
            {
                direccion =
                    (objetoNegro.transform.position -
                    transform.position).normalized;

                transform.position +=
                    direccion * velocidad * Time.deltaTime;
            }
        }
    }
    void ActualizarPrediccion()
    {
        for (int i = 0; i < prediccion.Length; i++)
        {
            prediccion[i].transform.position =
                PredictPositionMRU(
                    transform.position,
                    direccion * timeStep,
                    i);
        }
    }
    /// <returns>Nueva posición según el MRU</returns>
    public Vector3 PredictPositionMRU(Vector3 startPos, Vector3 velocity, float time)
    {
        // Fórmula MRU: posición = posición inicial + velocidad * tiempo
        return startPos + velocity * time;
    }
    


}
