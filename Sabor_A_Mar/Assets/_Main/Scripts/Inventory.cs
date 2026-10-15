using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public Transform inventorySpawnPoint;

    public int maxInventorySize = 3;

    public Stack<FoodElement> inventory = new Stack<FoodElement>();

    public static event Action<int> OnQuantityChanged;

    public bool platanoAgarrado = false;
    public bool cocoAgarrado = false;

    public bool pescadoAgarrado = false;

    public bool PlatanoAgarrado
    {
        get => platanoAgarrado;
        set
        {
            platanoAgarrado = value;
        }
    }

    public bool CocoAgarrado
    {
        get => cocoAgarrado;
        set
        {
            cocoAgarrado = value;
        }
    }

    public bool PescadoAgarrado
    {
        get => pescadoAgarrado;
        set
        {
            pescadoAgarrado = value;
        }
    }



    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("hay mas de un singleton");
            Destroy(this);
            return;
        }
        Instance = this;
        OnQuantityChanged?.Invoke(inventory.Count);
    }

    public void AddToInventory(FoodElement comida)
    {
        inventory.Push(comida);
        OnQuantityChanged?.Invoke(inventory.Count);
    }
    public void RemoveFromInventory()
    {
        if (inventory.Count > 0)
        {
            if (inventory.TryPop(out var popped))
            {
                popped.gameObject.SetActive(true);           
                popped.transform.position = inventorySpawnPoint.position;
                DistanceGrabInteractable interactable = popped.GetComponentInChildren<DistanceGrabInteractable>();        //tengo que estar pendiente que esta función sí busque en todos los hijos o si toca nombrar en cuál hijo debe buscar
                HandGrabInteractable handInteractable = popped.GetComponentInChildren<HandGrabInteractable>();
                GrabInteractable grabInteractable = popped.GetComponentInChildren<GrabInteractable>();
                DistanceHandGrabInteractable distanceHandGrab = popped.GetComponentInChildren<DistanceHandGrabInteractable>();
                Grabbable grabbable = popped.GetComponent<Grabbable>();
                interactable.enabled = true;
                handInteractable.enabled = true;
                grabInteractable.enabled = true;
                distanceHandGrab.enabled = true;
                grabbable.enabled = true;
                Debug.Log("Elemento salió del inventario");
            }
        }
        else
        {
            Debug.Log("Inventario vacío");
        }
    }  
}
