using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AreaDetection : MonoBehaviour
{
    public enum AreaType
    {
        Platano,
        Pescado,
        Coco
    }
    public AreaType areaType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VFX_Manager.Instance.StartCoroutine(VFX_Manager.Instance.ApagarEfecto(VFXType.Interactable, 1f));
            switch (areaType)
            {
                case AreaType.Platano:
                    GameManager.Instance.SeAcercoAlPlatano = true;
                    break;
                case AreaType.Pescado:
                    GameManager.Instance.SeAcercoAlPescado = true;
                    break;
                case AreaType.Coco:
                    GameManager.Instance.SeAcercoAlCoco = true;
                    break;
            }
            GameManager.Instance.EstablecerEfectoInteractables();
        }
    }
}
