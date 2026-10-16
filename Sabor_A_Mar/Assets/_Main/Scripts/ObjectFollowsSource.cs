using UnityEngine;

public class ObjectFollowsSource : MonoBehaviour
{
    public Transform source;
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform == source) 
        { 
            
        }
    }
}
