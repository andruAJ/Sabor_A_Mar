using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class ToggleInteractions : MonoBehaviour
{
    public void ToggleInteractionComponents(bool enable, GameObject targetObject)
    {
        DistanceGrabInteractable interactable = targetObject.GetComponentInChildren<DistanceGrabInteractable>();
        HandGrabInteractable handInteractable = targetObject.GetComponentInChildren<HandGrabInteractable>();
        GrabInteractable grabInteractable = targetObject.GetComponentInChildren<GrabInteractable>();
        DistanceHandGrabInteractable distanceHandGrab = targetObject.GetComponentInChildren<DistanceHandGrabInteractable>();
        Grabbable grabbable = targetObject.GetComponent<Grabbable>();
        if (interactable != null) interactable.enabled = enable;
        if (handInteractable != null) handInteractable.enabled = enable;
        if (grabInteractable != null) grabInteractable.enabled = enable;
        if (distanceHandGrab != null) distanceHandGrab.enabled = enable;
        if (grabbable != null) grabbable.enabled = enable;
    }
}
