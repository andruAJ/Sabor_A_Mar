using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

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

    public float effect_timer = 1.0f;

    void Start()
    {
        teleport_volume = tp_volumeParent.GetComponent<Volume>();
        teleport_volume.profile.TryGet<Vignette>(out teleport_vignette);
        teleport_volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
        if (teleport_vignette != null)
        {
            Debug.Log("Vignette found in volume profile.");
        }
    }

    public void TeleportToPoint(int point)
    {
        if (teleport_volume == null || teleport_vignette == null) return;
        StartCoroutine(FadeOut());
        if (teleporterPoints.Length > point && point >= 0)
        {
            Transform targetPoint = teleporterPoints[point];
            player.transform.position = targetPoint.position;
        }
        else
        {
            Debug.LogWarning("Teleporter point not set for: " + point);
        }
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < effect_timer)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / effect_timer;
            teleport_vignette.intensity.value = Mathf.Lerp(tp_effect_initialIntensity, 1, t);
            colorAdjustments.colorFilter.value = Color.Lerp(Color.white, Color.black, t);
            yield return null;
        }
    }
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < effect_timer)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / effect_timer;
            teleport_vignette.intensity.value = Mathf.Lerp(1f, tp_effect_initialIntensity, t);
            colorAdjustments.colorFilter.value = Color.Lerp(Color.black, Color.white, t);
            yield return null;
        }
    }
}
