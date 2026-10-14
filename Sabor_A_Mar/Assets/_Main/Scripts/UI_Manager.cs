using UnityEditorInternal;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public Animator inv_animatorController;
    public Animator tp_animatorController;
    public GameObject[] slots;

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
}
