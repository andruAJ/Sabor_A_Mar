using System;
using System.Collections;
using UnityEngine;

public enum VFXType
{
    Interactable,
    Agua,
    Leche,
    Arroz,
    Salero,
    Ralladura,
    Aceite,
    Escamas,
    ComidaLista
}
[ExecuteInEditMode]
public class VFX_Manager : MonoBehaviour
{
    public static VFX_Manager Instance { get; private set; }
    public VFXList[] vfxObjects;

    private void Awake()
    {
        Instance = this;
    }

    public void PrenderEfecto(VFXType efecto, bool deseamover, Transform targetTransform)
    {
        GameObject effect = vfxObjects[(int)efecto].Effect;
        if(deseamover)
        {
            MoverEfecto(effect, targetTransform);
        }
        effect.SetActive(true);
        
    }
    public void MoverEfecto(GameObject efecto, Transform targetTransform)
    {
        efecto.transform.position = targetTransform.position;
    }
    public IEnumerator ApagarEfecto(VFXType efecto, float time)
    {
        GameObject effect = vfxObjects[(int)efecto].Effect;
        ParticleSystem childrenPS = effect.GetComponentInChildren<ParticleSystem>();

        if(childrenPS == null)
            yield break;

        var mainModule = childrenPS.main;
        Color startColor = mainModule.startColor.color;
        float startAlpha = startColor.a;

        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime/ time);
            Color currentColor = startColor;
            currentColor.a = Mathf.Lerp(startAlpha, 0f, progress);
            mainModule.startColor = currentColor;
            yield return null;
        }
        effect.SetActive(false);
        mainModule.startColor = startColor; // Verificar si esto sí resetea bien el color
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(VFXType));
        Array.Resize(ref vfxObjects, names.Length);
        for (int i = 0; i < vfxObjects.Length; i++)
        {
            vfxObjects[i].name = names[i];
        }
    }
#endif
}
[Serializable]
public struct VFXList
{
    public GameObject Effect { get => effect; }
    [HideInInspector] public string name;
    [SerializeField] private GameObject effect;
}
