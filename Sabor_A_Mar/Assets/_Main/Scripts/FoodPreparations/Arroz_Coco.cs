using System.Collections;
using UnityEngine;

public class Arroz_Coco : MonoBehaviour
{
    //GameObjects
    public GameObject[] pasos;

    //bool de comprobación
    private bool encimaDeOlla;
    private bool encimaDeBowl;

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
                        //Aquí deja el coco rallado pero puede que falta que se agregue la leche al bowl
                        //efectos visuales y cambia de estado
                        SetArrozState(ArrozState.CocoRallado);
                    }
                    else 
                    {
                        //efectos visuales pero no cambia de estado
                        Debug.LogWarning("Coco rallado pero no encima de la olla");
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
                PourDetector pourDetector = GetComponent<PourDetector>();
                pourDetector.enabled = true;
                break;
            case ArrozState.CocoRallado:
                pasos[2].SetActive(true);
                break;
            case ArrozState.LecheHervida:
                //el apagado de la leche lo hice desde la olla pero podría ser desde aquí directamente
                break;
            case ArrozState.ArrozAgregado:
                TurnOffAllHijosExcept(pasos[4]);
                pasos[1].SetActive(true);
                break;
            case ArrozState.Agua_SalAgregados:
                
                break;
            case ArrozState.ArrozCocido:
                TurnOffAllHijosExcept(pasos[6]);
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
        if (currentState == estadoNuevo-1) { SetArrozState(estadoNuevo);} //preciento que si se hecha la ralladura y la leche en desorden, esto creara problemas
    }
}
