using System.Collections.Generic;
using UnityEngine;

public class OcclusionController : MonoBehaviour
{
    public Transform jugador;

    private List<Renderer> objetosTransparentes = new List<Renderer>();

    void LateUpdate()
    {
        // Restaurar materiales
        foreach (Renderer rend in objetosTransparentes)
        {
            Color color = rend.material.color;
            color.a = 1f;
            rend.material.color = color;
        }

        objetosTransparentes.Clear();

        Vector3 direccion = jugador.position - transform.position;
        float distancia = direccion.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direccion, distancia);

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform == jugador)
                continue;

            Renderer rend = hit.collider.GetComponent<Renderer>();

            if (rend != null)
            {
                Color color = rend.material.color;
                color.a = 0.3f;
                rend.material.color = color;

                objetosTransparentes.Add(rend);
            }
        }
    }
}
