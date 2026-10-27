using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameManager gameManager;
    public UI_Manager uiManager;
    public Pescado pescadoScript;
    public Patacon pataconScript;
    public Arroz_Coco arrozScript;
    public ToggleInteractions toggleInteractions;
    public Enable_Preparaciones enable_Preparaciones_Sarten;
    public Enable_Preparaciones enable_Preparaciones_Tabla;



    void Awake()
    {
        FoodGrabbed.OnFoodGrabbed += OnFoodGrabbed;
        Enable_Preparaciones.OnTablaCollided += PreparacionTabla;
        Enable_Preparaciones.OnSartenCollided += PreparacionSarten;
        Inventory.OnQuantityChanged +=  ChangeInventoryIcon;
        Inventory.OnInventoryChanged += ToggleInteractions;
        Pescado.OnPreparacionPescado += ToggleInteractions;
        Patacon.OnPreparacionPatacon += ToggleInteractions;
        Olla.PasoActual += PreparacionOlla;
        Olla.cocoEncimaDeOlla += CocoEncimaDeolla;
        Pescado.PescadoListo += PescadoListo;
        Patacon.PataconListo += PataconListo;
        Arroz_Coco.ArrozListo += ArrozListo;
        EmplatadoFinal.ComidaEnPlato += Emplatado;

    }

    public void OnFoodGrabbed(FoodGrabbed.FoodType foodType)
    {
        uiManager.ChangeSlotTexture((int)foodType);
        Debug.Log("Comida agarrada: " + foodType);
    }
    public void ToggleInteractions(bool enable, GameObject targetObject) 
    {
        toggleInteractions.ToggleInteractionComponents(enable, targetObject);
    }
    public void PreparacionTabla(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Pescado":
                pescadoScript = other.gameObject.GetComponent<Pescado>();
                if (pescadoScript != null)
                {
                    if (pescadoScript.enabled == false)
                    {

                        pescadoScript.enabled = true;
                    }
                }
                break;
            case "Platano":
                pataconScript = other.gameObject.GetComponent<Patacon>();
                if (pataconScript != null)
                {
                    if (pataconScript.enabled == false)
                    {
                        pataconScript.enabled = true;
                    }
                }
                break;
            default:
                Debug.LogWarning("Chequear el tag de este objeto:  " + other.gameObject + " y el pirobo que está mandando este mensaje es: " + this.gameObject.name);
                break;
        }
    }
    public void PreparacionSarten(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Pescado":
                pescadoScript = other.gameObject.GetComponent<Pescado>();
                if (pescadoScript != null)
                {
                    if (pescadoScript.enabled == false)
                    {
                        pescadoScript.enabled = true;
                    }
                    StartCoroutine(pescadoScript.Fritura());
                }
                break;
            case "Platano":
                pataconScript = other.gameObject.GetComponent<Patacon>();
                if (pataconScript != null)
                {
                    if (pataconScript.enabled == false)
                    {
                        pataconScript.enabled = true;
                    }
                    StartCoroutine(pataconScript.Fritura());
                }
                break;
            default:
                break;
        }
    }
    public void PreparacionOlla(Arroz_Coco.ArrozState estado)
    {
        arrozScript.IngredienteFueAñadido(estado);
    }
    public void CocoEncimaDeolla(bool encima)
    {
        arrozScript.EncimaDeOlla(encima);
    }
    public void PescadoListo(bool completado) 
    {
        enable_Preparaciones_Sarten.ActivarCollider();
        enable_Preparaciones_Tabla.ActivarCollider();
    }
    public void PataconListo(bool completado)
    {
        enable_Preparaciones_Sarten.ActivarCollider();
        enable_Preparaciones_Tabla.ActivarCollider();
    }
    public void ArrozListo(bool completado)
    {

    }
    public void ChangeInventoryIcon(int quantity) 
    {
        uiManager.ChangeInvButtonTexture(quantity);
    }
    public void Emplatado(string comidaAEmplatar)
    {
        switch(comidaAEmplatar)
        {
            case "Pescado":
                gameManager.PescadoEmplatado = true;
                //audio diciendo que se puso el pescado
                gameManager.FinalExperiencia();
                break;
            case "Patacones":
                gameManager.PataconesEmplatados = true;
                //audio diciendo que se pusieron los patacones
                gameManager.FinalExperiencia();
                break;
            case "Arroz":
                gameManager.ArrozEmplatado = true;
                //audio diciendo que se puso el arroz
                gameManager.FinalExperiencia();
                break;
            default:
                Debug.LogWarning("Algo que no es comida colisionó con el plato");
                break;
        }
    }
}
