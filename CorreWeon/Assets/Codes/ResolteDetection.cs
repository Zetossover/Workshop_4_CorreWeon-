using UnityEngine;

public class ResolteDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Suelo") && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("*LE ROBA*");
        }
    }
}
