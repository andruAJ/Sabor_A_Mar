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
                    SetArrozState(ArrozState.CocoPartido);       
                }   
                break;
            case "Rallador":
                if (currentState == ArrozState.CocoPartido)
                {
                    VFX_Manager.Instance.PrenderEfecto(VFXType.Ralladura, true, this.transform);
                    if (encimaDeOlla) 
                    {
                        SetArrozState(ArrozState.CocoRallado);
                    } 
                    VFX_Manager.Instance.StartCoroutine(VFX_Manager.Instance.ApagarEfecto(VFXType.Ralladura, 2f));
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
                VFX_Manager.Instance.PrenderEfecto(VFXType.Agua, false, this.transform);
                break;
            case ArrozState.ArrozCocido:
                TurnOffAllHijosExcept(pasos[6]);
                pasos[1].SetActive(true);
                break;
            case ArrozState.ArrozListo:
                VFX_Manager.Instance.PrenderEfecto(VFXType.ComidaLista, true, pasos[6].transform);
                VFX_Manager.Instance.StartCoroutine(VFX_Manager.Instance.ApagarEfecto(VFXType.ComidaLista, 1f));
                break;
        }
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
