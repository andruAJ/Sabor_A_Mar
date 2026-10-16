using UnityEngine;
using UnityEngine.Animations;

public class ObjectFollowsSource : MonoBehaviour
{
    public Transform source;
    public PositionConstraint positionConstraint;

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("colisionando con el lider ");
        if (other.transform == source)
        {
            positionConstraint.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.LogWarning("dirigiéndome al lider " );
        if (other.transform == source) 
        {
            positionConstraint.enabled = true;
        }
    }
}
