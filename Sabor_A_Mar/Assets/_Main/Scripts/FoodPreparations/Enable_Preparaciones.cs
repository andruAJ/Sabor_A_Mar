using System;
using UnityEngine;
using UnityEngine.Animations;
using static FoodGrabbed;

public class Enable_Preparaciones : MonoBehaviour
{
    public static event Action<Collision> OnTablaCollided;
    public static event Action<Collision> OnSartenCollided;
    public Pescado pescadoScript;
    private PositionConstraint positionConstraint;

    public void OnCollisionEnter(Collision collision)
    {
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        positionConstraint = collision.gameObject.GetComponent<PositionConstraint>();
        if (positionConstraint != null)
        {
            positionConstraint.enabled = true;
            positionConstraint.SetSource(0, new ConstraintSource { sourceTransform = this.transform, weight = 1 });
        }
        switch (this.gameObject.tag) 
        {
            case "Tabla":
                OnTablaCollided?.Invoke(collision);
                break;
            case "Sarten":
                OnSartenCollided?.Invoke(collision);
                break;
            default:
                Debug.LogWarning("Chequear el tag de este objeto:  " + this);
                break;
        }   
    }
}
