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
    public void PreparacionTabla(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Pescado":
                pescadoScript = collision.gameObject.GetComponent<Pescado>();
                if (pescadoScript != null)
                {
                    if (pescadoScript != enabled)
                    {
                        pescadoScript.enabled = true;
                    }
                }
                break;
            case "Platano":
                //aqui activar el script del platano
                break;
            default:
                Debug.LogWarning("Chequear el tag de este objeto:  " + collision.gameObject);
                break;
        }
    }
    public void PreparacionSarten(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Pescado":
                pescadoScript = collision.gameObject.GetComponent<Pescado>();
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
