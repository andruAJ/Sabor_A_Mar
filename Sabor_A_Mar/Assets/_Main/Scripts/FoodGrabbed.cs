using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System;
using UnityEngine;

public class FoodGrabbed : MonoBehaviour
{
    public void Seleccionarobjeto()
    {

        Inventory.Instance.AddToInventory(GetComponent<FoodElement>());

        try 
        {
            DistanceGrabInteractable interactable = GetComponentInChildren<DistanceGrabInteractable>();        //tengo que estar pendiente que esta función sí busque en todos los hijos o si toca nombrar en cuál hijo debe buscar
            HandGrabInteractable handInteractable = GetComponentInChildren<HandGrabInteractable>();
            GrabInteractable grabInteractable = GetComponentInChildren<GrabInteractable>();
            DistanceHandGrabInteractable distanceHandGrab = GetComponentInChildren<DistanceHandGrabInteractable>();
            Grabbable grabbable = GetComponent<Grabbable>();

            interactable.enabled = false;
            handInteractable.enabled = false;
            grabInteractable.enabled = false;
            distanceHandGrab.enabled = false;
            grabbable.enabled = false;
        } 
        catch (ArgumentException e)
        {
            throw new ArgumentException("Error al desactivar los componentes de interacción: " + e.Message);
        }
        gameObject.SetActive(false);
    }
}
