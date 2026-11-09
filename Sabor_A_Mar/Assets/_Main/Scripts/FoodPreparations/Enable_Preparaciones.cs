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
        
        if (other.gameObject.CompareTag("Pescado") || other.gameObject.CompareTag("Platano"))
        {
            Debug.LogWarning("Hizo colisión con el sartén");
            other.gameObject.TryGetComponent<Patacon>(out Patacon pataconScript);
            other.gameObject.TryGetComponent<Pescado>(out Pescado pescadoScript);
            bool pataconDisponibleTabla = pataconScript != null && pataconScript.CurrentState != Patacon.PataconState.Cortado && pataconScript.CurrentState != Patacon.PataconState.Dorado && pataconScript.CurrentState != Patacon.PataconState.Aplastado && pataconScript.CurrentState != Patacon.PataconState.Frito;      
            bool pescadoDisponibleTabla = pescadoScript != null && pescadoScript.CurrentState != Pescado.PescadoState.Cortado && pescadoScript.CurrentState != Pescado.PescadoState.Frito;
            bool pataconDisponibleSarten = pataconScript != null && pataconScript.CurrentState != Patacon.PataconState.Verde && pataconScript.CurrentState != Patacon.PataconState.Pelado && pataconScript.CurrentState != Patacon.PataconState.Dorado && pataconScript.CurrentState != Patacon.PataconState.Frito;
            bool pescadoDisponibleSarten = pescadoScript != null && pescadoScript.CurrentState == Pescado.PescadoState.Cortado;
            other.gameObject.TryGetComponent<PositionConstraint>(out PositionConstraint positionConstraint);
            //this.gameObject.GetComponent<SphereCollider>().enabled = false;
            if (positionConstraint != null)
            {
                positionConstraint.enabled = true;
                positionConstraint.SetSource(0, new ConstraintSource { sourceTransform = this.transform, weight = 1 });
            }
            
            if (pataconDisponibleTabla || pescadoDisponibleTabla)
            {  
                switch (this.gameObject.tag)
                {
                    case "Tabla":
                        Debug.LogWarning("OnTablaCollided invoked");
                        OnTablaCollided?.Invoke(other);
                        break;
                    case "Sarten": 
                        positionConstraint.enabled = false;
                        //this.gameObject.GetComponent<SphereCollider>().enabled = true;
                        break;
                }
            }
            else if (pataconDisponibleSarten || pescadoDisponibleSarten)
            {
                switch (this.gameObject.tag)
                {
                    case "Tabla":
                        positionConstraint.enabled = false;
                        //this.gameObject.GetComponent<SphereCollider>().enabled = true;
                        break;
                    case "Sarten":
                        Debug.LogWarning("OnSartenCollided invoked");
                        OnSartenCollided?.Invoke(other);
                        VFX_Manager.Instance.PrenderEfecto(VFXType.Aceite, false, this.transform);
                        break;
                }
            }
            else //necesario si el patacón está dorado o frito
            {
                positionConstraint.enabled = false;
                //this.gameObject.GetComponent<SphereCollider>().enabled = true;
            }
        }     
    }
    public void ActivarCollider() 
    {
        this.gameObject.GetComponent<SphereCollider>().enabled = true;
    }
}
