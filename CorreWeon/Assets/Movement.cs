using UnityEngine;

public class Movement : MonoBehaviour
{
    public float spd;
    private float a;
    private float w;
    public Transform myTransform;
    public Material materialNegro;

    void Update()
    {
        a = Input.GetAxis("Horizontal"); 
        w = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.R))
        {
            CambiarTagYColor();
        }

        if (a != 0 || w != 0)
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
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Blanco"))
        {
            Debug.Log("*LE ROBA*");
        }
        if (other.gameObject.CompareTag("Policia"))
        {
            Destroy(gameObject);
        }
    }
}

