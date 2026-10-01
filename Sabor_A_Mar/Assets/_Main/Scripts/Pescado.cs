using UnityEngine;

public class Pescado : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            // Call the method to handle the collision with the player
            
        }
    }
}
