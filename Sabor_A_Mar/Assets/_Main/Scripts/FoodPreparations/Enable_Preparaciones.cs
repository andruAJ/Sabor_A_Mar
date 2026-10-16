using System;
using UnityEngine;
using UnityEngine.Animations;
using static FoodGrabbed;

public class Enable_Preparaciones : MonoBehaviour
{
    public static event Action<Collider> OnTablaCollided;
    public static event Action<Collider> OnSartenCollided;
    private PositionConstraint positionConstraint;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        positionConstraint = other.gameObject.GetComponent<PositionConstraint>();
        if (positionConstraint != null)
        {
            positionConstraint.enabled = true;
            positionConstraint.SetSource(0, new ConstraintSource { sourceTransform = this.transform, weight = 1 });
        }
        switch (this.gameObject.tag) 
        {
            case "Tabla":
                OnTablaCollided?.Invoke(other);
                break;
            case "Sarten":
                OnSartenCollided?.Invoke(other);
                break;
            default:
                Debug.LogWarning("Chequear el tag de este objeto:  " + this);
                break;
        }   
    }
}
