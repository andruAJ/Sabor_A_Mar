using UnityEngine;

public class DeteccionInicioPreparaciones : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            ConversationManager.Instance.NextConversation(Conversacion.DescamarPescado);
            this.gameObject.SetActive(false);
        }
    }
}
