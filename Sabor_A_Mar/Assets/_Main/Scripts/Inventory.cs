using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public int maxInventorySize = 3;

    public Transform inventorySpawnPoint;

    public Stack<FoodElement> inventory = new Stack<FoodElement>();

    public static event Action<int> OnQuantityChanged;
    public static event Action<bool, GameObject> OnInventoryChanged;

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
        OnInventoryChanged?.Invoke(false, comida.gameObject);
        inventory.Push(comida);
        Debug.LogWarning("Se guardo el elemento " + comida);
        OnQuantityChanged?.Invoke(inventory.Count);
    }
    public void RemoveFromInventory()
    {
        Debug.LogWarning("Entro a RemoveFromInventory");
        if (inventory.Count > 0)
        {
            if (inventory.TryPop(out var popped))
            {
                Debug.LogWarning("hizo el TryPop");
                popped.gameObject.SetActive(true);
                popped.transform.position = inventorySpawnPoint.transform.position;
                OnInventoryChanged?.Invoke(true, popped.gameObject);
                Debug.Log("Elemento salió del inventario");
            }
        }
        else
        {
            Debug.Log("Inventario vacío");
        }
    }  
}
