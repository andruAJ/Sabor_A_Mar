using UnityEngine;

public class EventManager : MonoBehaviour
{
    public UI_Manager uiManager;
    public Pescado pescadoScript;
    public Patacon pataconScript;
    public ToggleInteractions toggleInteractions;



    void Awake()
    {
        FoodGrabbed.OnFoodGrabbed += OnFoodGrabbed;
        Enable_Preparaciones.OnTablaCollided += PreparacionTabla;
        Enable_Preparaciones.OnSartenCollided += PreparacionSarten;
        Enable_Preparaciones.OnOllaCollided += PreparacionOlla;
        Inventory.OnQuantityChanged +=  ChangeInventoryIcon;
        Inventory.OnInventoryChanged += ToggleInteractions;
        Pescado.OnPreparacionPescado += ToggleInteractions;
        
    }

    public void OnFoodGrabbed(FoodGrabbed.FoodType foodType)
    {
        Debug.Log("Evento recibido: " + foodType);
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
    public void PreparacionOlla(Collider other)
    {         // Implementación de la preparación en la olla
    }
    public void ChangeInventoryIcon(int quantity) 
    {
        uiManager.ChangeInvButtonTexture(quantity);
    }
}
