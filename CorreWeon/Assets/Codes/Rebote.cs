using UnityEngine;

public class Rebote : MonoBehaviour
{
    public float spd;
    private float a;
    private float w;
    public Transform myTransform; 

    void Start()
    {
        
        if (myTransform == null)
        {
            myTransform = transform; 
        }
    }

    void Update()
    {
        a = Input.GetAxis("Horizontal"); 
        w = Input.GetAxis("Vertical"); 

        if (a != 0 || w != 0)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        float rotacionY = a * spd * Time.deltaTime; 
        float rotacionX = w * spd * Time.deltaTime; 

        myTransform.Rotate(rotacionX, rotacionY, 0);
    }
    
}
