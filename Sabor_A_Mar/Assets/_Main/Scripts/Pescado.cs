using UnityEngine;

public class Pescado : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            Debug.Log("Hand entro en el área");

        }
    }
}
