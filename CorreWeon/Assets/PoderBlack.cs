using UnityEngine;

public class PoderBlack : MonoBehaviour
{
    
    public Material materialNegro;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            CambiarTagYColor();
        }
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
}

