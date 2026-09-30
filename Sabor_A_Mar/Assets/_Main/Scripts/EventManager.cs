using UnityEngine;

public class EventManager : MonoBehaviour
{
    
    void Awake ()
    {
        FoodGrabbed.OnFoodGrabbed += OnFoodGrabbed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnFoodGrabbed(FoodGrabbed.FoodType foodType)
    {
        // cambiar UI
        Debug.Log("Comida agarrada: " + foodType);
    }
}
