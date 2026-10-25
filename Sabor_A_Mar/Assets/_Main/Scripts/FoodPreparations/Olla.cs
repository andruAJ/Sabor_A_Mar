using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Olla : MonoBehaviour
{
    private Arroz_Coco.ArrozState currentState;
    public Arroz_Coco arroz_Coco;
    public Bowl bowl;

    //bool para cocciones e ingredientes agregados
    public bool lecheServida , arrozServido , aguaServida , salServida , lecheCocinada , arrozCocinado;

    //evento
    public static event Action<Arroz_Coco.ArrozState> PasoActual;
    private string collisionTag;

    private void OnTriggerEnter(Collider other)
    {
        collisionTag = other.gameObject.tag;
        currentState = arroz_Coco.CurrentState;
        arrozServido = false;
        lecheServida = false;
        if (other.gameObject.tag == "TapaOlla" && currentState == Arroz_Coco.ArrozState.ArrozCocido) { }
        
    }
    private void OnTriggerStay(Collider other) 
    {
        //si ´hay problema con el regado por ángulos debe ser por la normalización de los ángulos 
        var rot = other.transform.localEulerAngles;
        float x = rot.x % 360;
        float z = rot.z % 360;

        bool xIsMax = Mathf.Abs(x) > Mathf.Abs(z);
        float maxValue = xIsMax ? x : z;
        float delta = Mathf.DeltaAngle(0, maxValue);
        if (Mathf.Abs(delta) > 90f && Mathf.Abs(delta) < 270f)
        {
            switch (collisionTag)
            {
                case "BowlConLeche":
                    //efecto visual
                    other.gameObject.tag = "Bowl";
                    lecheServida = true;
                    StartCoroutine(Coccion());
                    break;
                case "Coco":
                    PasoActual?.Invoke(Arroz_Coco.ArrozState.CocoRallado);
                    break;
                case "Arroz":
                    arrozServido = true;
                    PasoActual?.Invoke(Arroz_Coco.ArrozState.ArrozAgregado);
                    break;
                case "Agua":
                    aguaServida = true;
                    if (salServida) PasoActual?.Invoke(Arroz_Coco.ArrozState.Agua_SalAgregados); StartCoroutine(Coccion());
                    break;
                case "Salero":
                    salServida = true;
                    if(aguaServida) PasoActual?.Invoke(Arroz_Coco.ArrozState.Agua_SalAgregados); StartCoroutine(Coccion());
                    break;
            }
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
            yield return null;
        }
        if (elapsedTime >= 5f)
        {
            lecheCocinada = lecheServida;
            arrozCocinado = arrozServido;
            if (lecheCocinada) { PasoActual?.Invoke(Arroz_Coco.ArrozState.LecheHervida); }
            else if (arrozCocinado) { PasoActual?.Invoke(Arroz_Coco.ArrozState.ArrozCocido); }
            else { PasoActual?.Invoke(Arroz_Coco.ArrozState.ArrozListo); }
        }
    }
}
