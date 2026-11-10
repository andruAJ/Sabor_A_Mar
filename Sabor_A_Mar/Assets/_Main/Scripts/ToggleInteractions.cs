using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class ToggleInteractions : MonoBehaviour
{
    public void ToggleInteractionComponents(bool enable, GameObject targetObject)
    {
        try{
            targetObject.gameObject.transform.Find("MetaInteractions").gameObject.SetActive(enable);
            Grabbable grabbable = targetObject.GetComponent<Grabbable>();
            if (grabbable != null) grabbable.enabled = enable;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("No se encontró el objeto MetaInteractions en " + targetObject.name + ". Error: " + e.Message);
        }
        
    }
}
