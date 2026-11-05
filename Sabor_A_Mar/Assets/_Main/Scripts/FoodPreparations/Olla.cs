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
        lecheServida = false;
        if (other.gameObject.tag == "TapaOlla" && currentState == Arroz_Coco.ArrozState.ArrozCocido) { StartCoroutine(Coccion());}
        
    }
    private void OnTriggerStay(Collider other) 
    {
        //si hay problema con el regado por ángulos debe ser por la normalización de los ángulos 
        var rot = other.transform.localEulerAngles;
        float x = rot.x % 360;
        float z = rot.z % 360;

        bool xIsMax = Mathf.Abs(x) > Mathf.Abs(z);
        float maxValue = xIsMax ? x : z;
        float delta = Mathf.DeltaAngle(0, maxValue);
        if (Mathf.Abs(delta) > 90f && Mathf.Abs(delta) < 270f)
        {
            Debug.LogWarning("El ángulo del objeto es correcto");
            switch (collisionTag)
            {
                case "BowlConLeche":
                    //efecto visual
                    other.gameObject.tag = "Bowl";
                    lecheServida = true;
                    lecheEnElBowl.SetActive(false);
                    Paso4.SetActive(true);
                    Debug.LogWarning("Leche del bowl fue servida en la olla");
                    StartCoroutine(Coccion());
                    break;
                case "Arroz":
                    arrozServido = true;
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
                    if(aguaServida) PasoActual?.Invoke(Arroz_Coco.ArrozState.Agua_SalAgregados); StartCoroutine(Coccion());
                    break;
            }
        } 
        if(collisionTag == "Coco")
        {
            cocoEncimaDeOlla?.Invoke(true);
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
            if (lecheCocinada) {leche.SetActive(false); PasoActual?.Invoke(Arroz_Coco.ArrozState.LecheHervida);}
            else if (arrozCocinado) { PasoActual?.Invoke(Arroz_Coco.ArrozState.ArrozCocido); }
            else { PasoActual?.Invoke(Arroz_Coco.ArrozState.ArrozListo); } //tengo sospecha que este else puede dar a casos donde no se aplicarian, para solucionarlo debería de poner el condicional que sólo sea con el current state arroz cocido
        }
    }
}
