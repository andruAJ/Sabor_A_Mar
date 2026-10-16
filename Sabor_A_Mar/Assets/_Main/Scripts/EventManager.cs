using UnityEngine;

public class EventManager : MonoBehaviour
{
    public UI_Manager uiManager;

    public Pescado pescadoScript;



    void Awake()
    {
        FoodGrabbed.OnFoodGrabbed += OnFoodGrabbed;
        Enable_Preparaciones.OnTablaCollided += PreparacionTabla;
        Enable_Preparaciones.OnSartenCollided += PreparacionSarten;
        Inventory.OnQuantityChanged +=  ChangeInventoryIcon;
    }

    public void OnFoodGrabbed(FoodGrabbed.FoodType foodType)
    {
        Debug.Log("Evento recibido: " + foodType);
        uiManager.ChangeSlotTexture((int)foodType);
        
        Debug.Log("Comida agarrada: " + foodType);
    }
    public void PreparacionTabla(Collider other)
    {
        Debug.LogWarning("PreparacionTabla: " + other.gameObject.name);
        switch (other.gameObject.tag)
        {
            case "Pescado":
                pescadoScript = other.gameObject.GetComponent<Pescado>();
                Debug.LogWarning("PreparacionTabla -> Case Pescado");
                if (pescadoScript != null)
                {
                    Debug.LogWarning("PreparacionTabla -> Case Pescado -> Script encontrado");
                    if (pescadoScript != enabled)
                    {
                        //Aquí no está entrando
                        Debug.LogWarning("PreparacionTabla -> Case Pescado -> Script encontrado -> El script está Desactivado");
                        pescadoScript.enabled = true;
                    }
                }
                break;
            case "Platano":
                //aqui activar el script del platano
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
                    if (pescadoScript != enabled)
                    {
                        pescadoScript.enabled = true;
                    }
                    StartCoroutine(pescadoScript.Fritura());
                }
                break;
            case "Platano":
                //aqui activar el script del platano
                break;
            default:
                break;
        }
    }
    public void ChangeInventoryIcon(int quantity) 
    {
        uiManager.ChangeInvButtonTexture(quantity);
    }
}
