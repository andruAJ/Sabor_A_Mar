using UnityEngine;

[RequireComponent(typeof (Collider))]
public class RetrieveFoodFromInventory : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            Debug.LogWarning("Hand se metió al bolso");
            Inventory.Instance.RemoveFromInventory();
        }
    }
}
