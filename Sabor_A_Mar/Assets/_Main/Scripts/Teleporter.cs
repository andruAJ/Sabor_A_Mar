using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Teleporter : MonoBehaviour
{
    public enum TeleporterPoints { Casa, Palmeras, Playa }

    public Transform[] teleporterPoints;

    public GameObject player;

    public GameObject tp_volumeParent;
    public Volume teleport_volume;
    private Vignette teleport_vignette;
    private ColorAdjustments colorAdjustments;

    private float tp_effect_initialIntensity;

    public void TeleportToPoint(TeleporterPoints point)
    {
        if (teleporterPoints.Length > (int)point)
        {
            Transform targetPoint = teleporterPoints[(int)point];
            transform.position = targetPoint.position;
            transform.rotation = targetPoint.rotation;
        }
        else
        {
            Debug.LogWarning("Teleporter point not set for: " + point);
        }
    }

    void Start()
    {
        teleport_volume = tp_volumeParent.GetComponent<Volume>();
        teleport_volume.profile.TryGet<Vignette>(out teleport_vignette);
        teleport_volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
        if (teleport_vignette != null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
