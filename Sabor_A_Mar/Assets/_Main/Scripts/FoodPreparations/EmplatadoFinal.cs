using System;
using UnityEngine;
using UnityEngine.Animations;

public class EmplatadoFinal : MonoBehaviour
{
    //Evento
    public static event Action <GameObject, String> ComidaEnPlato;

    public Transform pescadoPosition;
    public Transform pataconPosition;
    public Transform arrozPosition;

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Pescado>(out Pescado pescado);
        other.TryGetComponent<Patacon>(out Patacon patacon);
        other.TryGetComponent<Bowl>(out Bowl arroz);

        bool pescadoListo = pescado != null && pescado.CurrentState == Pescado.PescadoState.Frito;
        bool pataconListo = patacon != null && patacon.CurrentState == Patacon.PataconState.Frito;
        bool arrozListo = arroz != null && arroz.arrozServido;
        if (arrozListo) 
        {
            string comidaEmplatada = "Arroz";
            GameObject Plato = other.transform.GetChild(0).gameObject;
            GameObject arrozEnBowl = Plato.transform.GetChild(1).gameObject;
            AcoplarComida(arrozEnBowl, comidaEmplatada);
            ComidaEnPlato?.Invoke(arrozEnBowl, comidaEmplatada);
            
        }
        else if (pescadoListo || pataconListo)
        {
            string comidaEmplatada = other.gameObject.tag.ToString();
            AcoplarComida(other.gameObject, comidaEmplatada);
            ComidaEnPlato?.Invoke(other.gameObject, comidaEmplatada);
        }
    }
    public void AcoplarComida(GameObject comida, string tipo) 
    {

        switch (tipo)
        {
            case "Pescado":
                comida.transform.position = pescadoPosition.position;
                comida.transform.rotation = pescadoPosition.rotation;
                break;
            case "Platano":
                comida.transform.position = pataconPosition.position;
                comida.transform.rotation = pataconPosition.rotation;
                break;
            case "Arroz":
                comida.transform.position = arrozPosition.position;
                comida.transform.rotation = arrozPosition.rotation;
                comida.TryGetComponent<PositionConstraint>(out PositionConstraint constraint);
                constraint.enabled = true;
                constraint.SetSource(0, new ConstraintSource { sourceTransform = arrozPosition, weight = 1 });
                comida.TryGetComponent<RotationConstraint>(out RotationConstraint rotationConstraint);
                rotationConstraint.enabled = true;
                rotationConstraint.SetSource(0, new ConstraintSource { sourceTransform = arrozPosition, weight = 1 });
                break;
        }
    }
}
