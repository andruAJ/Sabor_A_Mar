using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public Animator inv_animatorController;
    public Animator tp_animatorController;
    public GameObject[] slots;

    public Texture2D invIcon0, invIcon1, invIcon2, invIcon3;

    public Material invIconMaterial;

    private bool isInventoryVisible = false;
    private bool isTeleportVisible = false;

    public void ShowOrHidePanel(string panelName)
    {
        if (panelName == "Inventory")
            inv_animatorController.SetTrigger(isInventoryVisible ? "Hide" : "Show");
        else if (panelName == "Teleport")
            tp_animatorController.SetTrigger(isTeleportVisible ? "Hide" : "Show");
        else Debug.LogWarning("Panel name not recognized: " + panelName);
    }
    
    public void ChangeSlotTexture(int slotIndex)
    {
       slots[slotIndex].GetComponent<TextureChanger>().SetUnblocked(true);
    }
    public void TogglePanel(string panelName)
    {
        if (panelName == "Inventory")
        {
            isInventoryVisible = !isInventoryVisible;
            ShowOrHidePanel("Inventory");
        }
        else if (panelName == "Teleport")
        {
            isTeleportVisible = !isTeleportVisible;
            ShowOrHidePanel("Teleport");
        }
    }
    public void ChangeInvButtonTexture(int inv_quantity) 
    {
        switch (inv_quantity) 
        {
            case 0:
                invIconMaterial.mainTexture = invIcon0;
                break;
            case 1:
                invIconMaterial.mainTexture = invIcon1;
                break;
            case 2:
                invIconMaterial.mainTexture = invIcon2;
                break;
            case 3:
                invIconMaterial.mainTexture = invIcon3;
                break;
            default:
                Debug.LogWarning("cantidad en inventario incorrecta");
                break;
        }
    }
}
