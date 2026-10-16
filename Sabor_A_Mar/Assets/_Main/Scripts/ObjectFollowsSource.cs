using UnityEngine;
using UnityEngine.Animations;

public class ObjectFollowsSource : MonoBehaviour
{
    public Transform source;
    public PositionConstraint positionConstraint;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning("colisionando con el lider ");
        if (collision.transform == source)
        {
            positionConstraint.enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.LogWarning("dirigiéndome al lider " );
        if (collision.transform == source) 
        {
            positionConstraint.enabled = true;
        }
    }
}
