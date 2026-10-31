using System;
using UnityEngine;
using UnityEngine.Animations;

public class Enable_Preparaciones : MonoBehaviour
{
    public static event Action<Collider> OnTablaCollided;
    public static event Action<Collider> OnSartenCollided;
    private PositionConstraint positionConstraint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pescado") || other.gameObject.CompareTag("Platano"))
        {  
            other.gameObject.TryGetComponent<Patacon>(out Patacon pataconScript);
            other.gameObject.TryGetComponent<Pescado>(out Pescado pescadoScript);
            bool pataconDisponible = pataconScript != null && pataconScript.CurrentState != Patacon.PataconState.Cortado && pataconScript.CurrentState != Patacon.PataconState.Dorado && pataconScript.CurrentState != Patacon.PataconState.Aplastado && pataconScript.CurrentState != Patacon.PataconState.Frito;
            bool pescadoDisponible = pescadoScript != null && pescadoScript.CurrentState != Pescado.PescadoState.Cortado && pescadoScript.CurrentState != Pescado.PescadoState.Frito;

            if (pataconDisponible || pescadoDisponible)
            {
                this.gameObject.GetComponent<SphereCollider>().enabled = false;

                other.gameObject.TryGetComponent<PositionConstraint>(out PositionConstraint positionConstraint);
                if (positionConstraint != null)
                {
                    Debug.LogWarning("Entré al segundo if de Patacon Cortado");
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
           
    }
    public void ActivarCollider() 
    {
        this.gameObject.GetComponent<SphereCollider>().enabled = true;
    }
}
