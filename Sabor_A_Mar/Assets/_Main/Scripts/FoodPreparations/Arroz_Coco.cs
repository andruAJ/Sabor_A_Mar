using System;
using System.Collections;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Animations;

public class Arroz_Coco : MonoBehaviour
{
    //GameObjects
    public GameObject[] pasos;

    //bool de comprobación
    private bool encimaDeOlla;
    private bool encimaDeBowl;

    //eventos
    public static event Action<bool> ArrozListo;
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
                    if (encimaDeOlla) 
                    {
                        //efectos visuales y cambia de estado
                        SetArrozState(ArrozState.CocoRallado);
                    }
                    else 
                    {
                        //efectos visuales pero no cambia de estado
                    } 
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
                pasos[2].SetActive(true);
                break;
            case ArrozState.LecheHervida:
                
                break;
            case ArrozState.ArrozAgregado:
                pasos[4].SetActive(true);
                break;
            case ArrozState.Agua_SalAgregados:
                
                break;
            case ArrozState.ArrozCocido:
                TurnOffAllHijosExcept(pasos[7]);
                pasos[1].SetActive(true);
                break;
            case ArrozState.ArrozListo:
                
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
    public void EncimaDeOlla(bool encima) 
    {
        encimaDeOlla = encima;
    }
    public void EncimaDeBowl(bool encima)
    {
        encimaDeBowl = encima;
    }
    public void IngredienteFueAñadido(ArrozState estadoNuevo) 
    {
        if (currentState == estadoNuevo-1) { currentState = estadoNuevo;}
    }
}
