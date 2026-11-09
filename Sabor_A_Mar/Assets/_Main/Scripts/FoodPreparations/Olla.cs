using System;
using System.Collections;
using UnityEngine;

public class Olla : MonoBehaviour
{
    private Arroz_Coco.ArrozState currentState;
    public Arroz_Coco arroz_Coco;
    public Bowl bowl;

    //bool para cocciones e ingredientes agregados
    public bool lecheServida , arrozServido , aguaServida , salServida , lecheCocinada , arrozCocinado;

    //eventos
    public static event Action<Arroz_Coco.ArrozState> PasoActual;
    public static event Action<bool> cocoEncimaDeOlla;

    //Elementos en la olla
    public GameObject Paso4;
    public GameObject leche;  //aquí la referencia será manual, pueedo intentar hacer para que coga la referencia desde el objeto cómo tal pero se complica si doy la opción de que hayan varios cocos que puedas coger, por lo que sólo pondré 1 y ese será el de la referencia manual
    public GameObject lecheEnElBowl;
    public GameObject arrozEnelBowl;
    public GameObject aguaEnelBowl;
    public GameObject aguaEnOlla;
    private string collisionTag;

    private void OnTriggerEnter(Collider other)
    {
        collisionTag = other.gameObject.tag;
        currentState = arroz_Coco.CurrentState;
        if (other.gameObject.tag == "TapaOlla" && currentState == Arroz_Coco.ArrozState.ArrozCocido) { StartCoroutine(Coccion());}
        Debug.LogWarning("Olla OnTriggerEnter. CollisionTag: " + collisionTag + ", CurrentState: " + currentState);
        switch (collisionTag)
        {
            case "Coco":
                Debug.LogWarning("Coco encima de la olla");
                cocoEncimaDeOlla?.Invoke(true);
                break;
            case "BowlConLeche":
                //efecto visual
                other.gameObject.tag = "Bowl";
                lecheServida = true;
                Debug.LogWarning("lecheServida: " + lecheServida);
                lecheEnElBowl.SetActive(false);
                other.TryGetComponent<PourDetector>(out PourDetector pourDetector);
                pourDetector.enabled = false;
                Paso4.SetActive(true);
                StartCoroutine(Coccion());
                break;
            case "Arroz":
                arrozServido = true;
                Debug.LogWarning("Arroz fue servido en la olla");
                arrozEnelBowl.SetActive(false);
                PasoActual?.Invoke(Arroz_Coco.ArrozState.ArrozAgregado);
                break;
            case "Agua":
                aguaServida = true;
                aguaEnelBowl.SetActive(false);
                aguaEnOlla.SetActive(true);
                Debug.LogWarning("Agua fue servida en la olla");
                if (salServida) PasoActual?.Invoke(Arroz_Coco.ArrozState.Agua_SalAgregados); StartCoroutine(Coccion());
                break;
            case "Salero":
                salServida = true;
                Debug.LogWarning("Sal fue servida en la olla");
                if (aguaServida) PasoActual?.Invoke(Arroz_Coco.ArrozState.Agua_SalAgregados); StartCoroutine(Coccion());
                break;
            default:
                Debug.LogWarning("el tag del objeto es: " + collisionTag);
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Coco")
        {
            cocoEncimaDeOlla?.Invoke(false);
        } 
    }
    private void EfectoLecheEnOlla() 
    {
        //Poner VFX
    }
    public IEnumerator Coccion()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 5f)
        {
            elapsedTime += Time.deltaTime;
            if (leche.gameObject.activeInHierarchy) {leche.transform.Translate(Vector3.down * Time.deltaTime * 0.022f); }  //el 0.022f es el resultado de dividir la distancia a recorrer (0.11) entre el tiempo que dura la cocción (5s)
            yield return null;
        }
        if (elapsedTime >= 5f)
        {
            lecheCocinada = lecheServida;
            arrozCocinado = arrozServido;
            if (lecheCocinada && !arrozCocinado) {leche.SetActive(false); PasoActual?.Invoke(Arroz_Coco.ArrozState.LecheHervida);}
            else if (arrozCocinado && lecheCocinada && currentState != Arroz_Coco.ArrozState.ArrozCocido) { PasoActual?.Invoke(Arroz_Coco.ArrozState.ArrozCocido); }
            else if (arrozCocinado && lecheCocinada && currentState == Arroz_Coco.ArrozState.ArrozCocido)
            { Debug.LogWarning("Se metió al else de arroz listo");
                PasoActual?.Invoke(Arroz_Coco.ArrozState.ArrozListo); } //tengo sospecha que este else puede dar a casos donde no se aplicarian, para solucionarlo debería de poner el condicional que sólo sea con el current state arroz cocido
        }
    }
}
