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
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
            other.gameObject.TryGetComponent<Pescado>(out Pescado pescadoScript);
            other.gameObject.TryGetComponent<Patacon>(out Patacon pataconScript);
            bool pataconCortado = pataconScript != null && pataconScript.CurrentState == Patacon.PataconState.Cortado;
            bool pescadoCortado = pescadoScript != null && pescadoScript.CurrentState == Pescado.PescadoState.Cortado;

            if (pataconCortado || pescadoCortado)
            {
                other.gameObject.TryGetComponent<PositionConstraint>(out PositionConstraint positionConstraint);
                if (positionConstraint != null)
                {
                    positionConstraint.enabled = true;
                    positionConstraint.SetSource(0, new ConstraintSource { sourceTransform = this.transform, weight = 1 });
                }
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
