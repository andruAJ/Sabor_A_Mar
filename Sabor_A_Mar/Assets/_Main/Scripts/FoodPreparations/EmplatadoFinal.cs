using System;
using UnityEngine;

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
            AcoplarComida(other.gameObject, comidaEmplatada);
            ComidaEnPlato?.Invoke(other.gameObject, comidaEmplatada);
        }
        else if (pescadoListo || pataconListo)
        {
            string comidaEmplatada = other.gameObject.tag.ToString();
            GameObject Plato = other.transform.GetChild(0).gameObject;
            GameObject arrozEnBowl = Plato.transform.GetChild(1).gameObject;
            AcoplarComida(arrozEnBowl, comidaEmplatada);
            ComidaEnPlato?.Invoke(arrozEnBowl, comidaEmplatada);
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
            case "Coco":
                comida.transform.position = arrozPosition.position;
                comida.transform.rotation = arrozPosition.rotation;
                break;
        }
    }
}
