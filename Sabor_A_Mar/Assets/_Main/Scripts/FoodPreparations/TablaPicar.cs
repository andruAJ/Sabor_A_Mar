using UnityEngine;

public class TablaPicar : MonoBehaviour
{
    public Pescado pescadoScript;

    public void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Pescado":
                pescadoScript = collision.gameObject.GetComponent<Pescado>();
                if (pescadoScript != null)
                {
                    pescadoScript.enabled = true;
                }
                break;
            case "Platano":
                //aqui activar el script del platano
                break;
            default:
                break;
        }

    }
}
