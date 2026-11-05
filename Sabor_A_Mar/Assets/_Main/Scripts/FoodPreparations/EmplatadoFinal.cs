using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class EmplatadoFinal : MonoBehaviour
{
    //Evento
    public static event Action <String> ComidaEnPlato;

    public Transform pescadoPosition;
    public Transform pataconPosition;
    public Transform arrozPosition;

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Pescado>(out Pescado pescado);
        other.TryGetComponent<Patacon>(out Patacon patacon);
        other.TryGetComponent<Arroz_Coco>(out Arroz_Coco arroz);

        bool pescadoListo = pescado != null && pescado.CurrentState == Pescado.PescadoState.Frito;
        bool pataconListo = patacon != null && patacon.CurrentState == Patacon.PataconState.Frito;
        bool arrozListo = arroz != null && arroz.CurrentState == Arroz_Coco.ArrozState.ArrozListo;

        if (pescadoListo || pataconListo || arrozListo)
        {
            string comidaEmplatada = other.gameObject.tag.ToString();
            AcoplarComida(other.gameObject, comidaEmplatada);
            ComidaEnPlato?.Invoke(comidaEmplatada);
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
