using UnityEngine;

public class DeteccionInicioPreparaciones : MonoBehaviour
{
    public GameObject canasta;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            for (int i =0; i <= 2; i++) 
            {
                Inventory.Instance.RemoveFromInventory();
            }
            canasta.SetActive(false);
            ConversationManager.Instance.NextConversation(Conversacion.DescamarPescado);
            this.gameObject.SetActive(false);
        }
    }
}
