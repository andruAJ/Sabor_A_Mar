using UnityEngine;
using UnityEngine.Animations;

public class ObjectFollowsSource : MonoBehaviour
{
    public Transform source;
    public PositionConstraint positionConstraint;

    private void OnTriggerStay(Collider other)
    {
        
        if (other.transform == source)
        {
            Debug.LogWarning("me quedo quieto");
            positionConstraint.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.transform == source) 
        {
            Debug.LogWarning("me muevo");
            positionConstraint.enabled = true;
        }
    }
}
