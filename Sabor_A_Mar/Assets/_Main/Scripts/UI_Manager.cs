using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject[] slots;
    public void ShowInventory()
    {
        //Inventory.Instance.inventorySpawnPoint.gameObject.SetActive(true);
    }

    public void HideInventory()
    {
        //Inventory.Instance.inventorySpawnPoint.gameObject.SetActive(false);
    }
    public void ChangeSlotTexture(int slotIndex)
    {
       slots[slotIndex].GetComponent<TextureChanger>().SetUnblocked(true);
    }
}
