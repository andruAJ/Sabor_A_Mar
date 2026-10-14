using UnityEngine;
using UnityEngine.Animations;

public class TablaPicar : MonoBehaviour
{
    public Pescado pescadoScript;
    private PositionConstraint positionConstraint;

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        switch (collision.gameObject.tag)
        {
            case "Pescado":
                pescadoScript = collision.gameObject.GetComponent<Pescado>();
                if (pescadoScript != null)
                {
                    collision.gameObject.GetComponent<PositionConstraint>().enabled = true;
                    Debug.Log("Constraint deberia ser activado");
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
    public void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision stay detected with: " + collision.gameObject.name);
    }
}
