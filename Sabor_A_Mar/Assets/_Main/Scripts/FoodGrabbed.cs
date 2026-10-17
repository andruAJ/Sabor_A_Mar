using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System;
using UnityEngine;

public class FoodGrabbed : MonoBehaviour
{
    public static event Action <FoodType> OnFoodGrabbed;
    public static event Action<bool, GameObject> OnElementGrabbed;

    public enum FoodType
    {
        Platano, Pescado, Coco
    }
    public void Seleccionarobjeto()
    {

        Inventory.Instance.AddToInventory(GetComponent<FoodElement>());

        try 
        {
            OnElementGrabbed?.Invoke(false, this.gameObject);

            Debug.Log("Elemento agarrado");

            switch (GetComponent<FoodElement>().food_Name)
            {
                case "Platano":
                    OnFoodGrabbed?.Invoke(FoodType.Platano);
                    Debug.Log("Platano agarrado");
                    break;
                case "Pescado":
                    OnFoodGrabbed?.Invoke(FoodType.Pescado);
                    Debug.Log("Pescado agarrado");
                    break;
                case "Coco":
                    OnFoodGrabbed?.Invoke(FoodType.Coco);
                    Debug.Log("Coco agarrado");
                    break;
                default:
                    Debug.LogWarning("Tipo de comida desconocido: " + GetComponent<FoodElement>().food_Name);
                    break;
            }
        } 
        catch (ArgumentException e)
        {
            throw new ArgumentException("Error al desactivar los componentes de interacción: " + e.Message);
        }
        GameObject parent = this.gameObject.transform.parent.gameObject;
        this.gameObject.SetActive(false);
    }
}
