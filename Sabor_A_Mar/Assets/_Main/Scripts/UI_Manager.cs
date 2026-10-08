using UnityEditorInternal;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public Animator inv_animatorController;
    public Animator tp_animatorController;
    public GameObject[] slots;

    public void ShowPanel(string panelName)
    {
        if (panelName == "Inventory")
            inv_animatorController.SetTrigger("Show");
        else if (panelName == "Teleport")
            tp_animatorController.SetTrigger("Show");
        else Debug.LogWarning("Panel name not recognized: " + panelName);
    }

    public void HidePanel(string panelName)
    {
        if (panelName == "Inventory")
            inv_animatorController.SetTrigger("Hide");
        else if (panelName == "Teleport")
            tp_animatorController.SetTrigger("Hide");
        else Debug.LogWarning("Panel name not recognized: " + panelName);
    }
    
    public void ChangeSlotTexture(int slotIndex)
    {
       slots[slotIndex].GetComponent<TextureChanger>().SetUnblocked(true);
    }
}
