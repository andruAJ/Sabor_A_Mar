using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameManager gameManager;
    public UI_Manager uiManager;
    public Pescado pescadoScript;
    public Patacon pataconScript;
    public Arroz_Coco arrozScript;  // estoy poniendo esta referencia desde el interactable del coco pero si hay varios cocos, habrá problema porque solo hace referencia a uno específico manualmente
    public ToggleInteractions toggleInteractions;

    public Enable_Preparaciones enablePreparaciones_Sarten;
    public Enable_Preparaciones enablePreparaciones_Tabla;

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
                    ToggleInteractions(false, other.gameObject);
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
    public void ChangeInventoryIcon(int quantity) 
    {
        uiManager.ChangeInvButtonTexture(quantity);
    }
    //CREO QUE ESTA SIGUIENTE FUNCIÓN NO SE NECESITA
    public void ActivarCollidersElementosCocina()
    {
        enablePreparaciones_Sarten.ActivarCollider();
        enablePreparaciones_Tabla.ActivarCollider();
    }
    public void Emplatado(GameObject comida, string comidaAEmplatar)
    {
        ToggleInteractions(false, comida);
        switch (comidaAEmplatar)
        {
            case "Pescado":
                Debug.LogWarning("Pescado emplatado");
                gameManager.PescadoEmplatado = true;
                //audio diciendo que se puso el pescado
                gameManager.FinalExperiencia();
                break;
            case "Platano":
                Debug.LogWarning("Patacones emplatados");
                gameManager.PataconesEmplatados = true;
                //audio diciendo que se pusieron los patacones
                gameManager.FinalExperiencia();
                break;
            case "Coco":
                Debug.LogWarning("Arroz emplatado");
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
