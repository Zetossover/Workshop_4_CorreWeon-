using UnityEngine;

public class NPC : MonoBehaviour
{
    public float radio = 3f; 
    public float velocidad = 2f;
    bool TieneDinero = true;

    void Update()
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
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Negro"))
        {
            TieneDinero = false;
        }
    }
}
