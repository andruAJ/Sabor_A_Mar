using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class Arroz_Coco : MonoBehaviour
{
    //GameObjects
    public GameObject[] pasos;

    //Eventos
    public static event Action <bool, GameObject> OnPreparacionArroz;
    

    public enum ArrozState
    {
        CocoEntero,
        CocoPartido,
        CocoRallado,
        LecheHervida,
        ArrozAgregado,
        Agua_SalAgregados,
        ArrozCocido,
        ArrozListo
    }
    private ArrozState currentState;

    public ArrozState CurrentState
    {
        get => currentState;
    }
    private void Start()
    {
        currentState = ArrozState.CocoEntero;
    }
    private void OnTriggerEnter(Collider other) 
    {
        string collisionTag = other.gameObject.tag;
        switch (collisionTag)
        {
            case "Cuchillo":
                if (currentState == ArrozState.CocoEntero)
                {
                    Debug.LogWarning("Golpe con cuchillo");
                    SetArrozState(ArrozState.CocoPartido);       
                }   
                break;
            case "Rallador":
                if (currentState == ArrozState.CocoPartido)
                {
                    Debug.LogWarning("Golpe con rallador");
                    //Confirmar que se haga encima de la olla
                    SetArrozState(ArrozState.CocoRallado);       
                }   
                break;
            default:
                break;
        }
    }
    public void SetArrozState(ArrozState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case ArrozState.CocoEntero:
                TurnOffAllHijosExcept(pasos[0]);
                break;
            case ArrozState.CocoPartido:
                TurnOffAllHijosExcept(pasos[1]);
                break;
            case ArrozState.CocoRallado:
                TurnOffAllHijosExcept(pasos[2]);
                break;
            case ArrozState.LecheHervida:
                TurnOffAllHijosExcept(pasos[3]);
                break;
            case ArrozState.ArrozAgregado:
                TurnOffAllHijosExcept(pasos[4]);
                break;
            case ArrozState.Agua_SalAgregados:
                TurnOffAllHijosExcept(pasos[5]);
                break;
            case ArrozState.ArrozCocido:
                TurnOffAllHijosExcept(pasos[6]);
                break;
            case ArrozState.ArrozListo:
                TurnOffAllHijosExcept(pasos[7]);
                break;
        }
        Debug.LogWarning("Estado del arroz cambiado a: " + currentState.ToString());
    }
    public void TurnOffAllHijosExcept(GameObject except) 
    {
        foreach (GameObject child in pasos) 
        {
            if (child.gameObject != except) 
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }
    
    public IEnumerator Coccion() 
    {
        float elapsedTime = 0f;
        while (elapsedTime < 10f)
        {
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
    }
}
