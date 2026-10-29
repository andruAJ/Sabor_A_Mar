using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RetrieveFoodFromInventory : MonoBehaviour
{
    
    public void SacarElemento(PointerEvent pointerEvent)
    {
        if (pointerEvent.Data is not HandRef handData)
        {
            Debug.LogWarning("No se recibió información de la mano.");
            return;
        }
        if (handData.GetJointPose(
            HandJointId.HandWristRoot, out Pose wristPose))
        {
            Vector3 handPosition = wristPose.position;
            Inventory.Instance.RemoveFromInventory(handPosition);
        }
    }
}
