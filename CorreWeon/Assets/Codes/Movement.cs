using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Movement : MonoBehaviour
{
    public float spd;
    private float a;
    private float w;
    public Transform myTransform;
    public Material materialNegro;

    public bool puedeMoverse = true;
    public bool capturado = false;
    void Update()
    {
        a = Input.GetAxis("Horizontal");
        w = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.R))
        {
            CambiarTagYColor();
        }

        if (puedeMoverse && (a != 0 || w != 0))
        {
            Move();
        }

    }

    private void Move()
    {
        Vector3 dir = new Vector3(a, 0, w); 
        myTransform.Translate(dir * Time.deltaTime * spd);
    }
    private void CambiarTagYColor()
    {
        gameObject.tag = "Negro";

        if (materialNegro != null)
        {
            GetComponent<Renderer>().material = materialNegro;
        }
        else
        {
            Debug.LogWarning("No Tiene el Material");
        }
    }
    private void SerCapturado()
    {
        puedeMoverse = false;
        capturado = true;

        gameObject.tag = "Capturado";

        GameState.juegoPausado = true;
        GameState.jugadorCapturado = true;

        Debug.Log("El jugador ha sido capturado");
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Blanco"))
        {
            Debug.Log("*LE ROBA*");
        }
        if (other.gameObject.CompareTag("Policia"))
        {
            SerCapturado();
        }
    }
}

