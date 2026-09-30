using UnityEngine;

public class EventManager : MonoBehaviour
{
    public UI_Manager uiManager;

    void Awake ()
    {
        FoodGrabbed.OnFoodGrabbed += OnFoodGrabbed;
    }

    public void OnFoodGrabbed(FoodGrabbed.FoodType foodType)
    {
        Debug.Log("Evento recibido: " + foodType);
        uiManager.ChangeSlotTexture((int)foodType);
        
        Debug.Log("Comida agarrada: " + foodType);
    }
}
