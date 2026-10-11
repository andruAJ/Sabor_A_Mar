using UnityEngine;

public class Pescado : MonoBehaviour
{
    public Texture2D pescado_Escamas;
    public Texture2D pescado_Sin_Escamas;
    public Texture2D pescado_Cortado;
    public Texture2D pescado_Frito;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            Debug.Log("Hand entro en el área");

        }
    }
}
