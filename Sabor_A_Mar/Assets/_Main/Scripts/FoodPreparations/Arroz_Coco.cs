using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class Arroz_Coco : MonoBehaviour
{
    //GameObjects
    public GameObjects pescado_Escamas;
    public GameObjects pescado_Sin_Escamas;
    public GameObjects pescado_Cortado;
    public GameObjects pescado_Frito;

    //MeshFilter
    public MeshFilter arrozMeshFilter;

    //Eventos
    public static event Action <bool, GameObject> OnPreparacionArroz;

    public enum ArrozState
    {
        CocoEntero,
        CocoVacio,
        CocoRallado,
        AguaHervida,
        CocoCocinado,
        ArrozListo
    }
    private ArrozState currentState;
    private void Start()
    {
        currentState = ArrozState.CocoEntero;
        OnPreparacionArroz?.Invoke(false, this.gameObject);
    }
    private void OnTriggerEnter(Collider other) 
    {
        string collisionTag = other.gameObject.tag;
        switch (collisionTag)
        {
            case "n":
                if (currentState == ArrozState.CocoEntero)
                {
                    Debug.LogWarning("Golpe con cuchillo");
                    SetArrozState(ArrozState.CocoVacio);       
                }   
                break;
            case "Rallador":
                if (currentState == ArrozState.CocoEntero)
                {
                    Debug.LogWarning("Golpe con rallador");
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
                
                break;
            case ArrozState.CocoVacio:
                
                break;
            case ArrozState.CocoRallado:
                
                break;
            case ArrozState.AguaHervida:
                
                break;
            case ArrozState.CocoCocinado:
                
                break;
            case ArrozState.ArozListo:
                
                break;
        }
        Debug.LogWarning("Estado del arroz cambiado a: " + currentState.ToString());
    }
    public void TurnOffAllHijosExcept(GameObject except) 
    {
        foreach (Transform child in transform.Find("HijoPrincipales")) 
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
    
    public IEnumerator Fritura() 
    {
        float elapsedTime = 0f;
        while (elapsedTime < 10f)
        {
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
    }
}
