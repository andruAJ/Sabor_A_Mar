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
    Aceite
}
[ExecuteInEditMode]
public class VFX_Manager : MonoBehaviour
{
    public VFXList[] vfxObjects;

    public void PrenderEfecto(VFXType efecto) 
    {
        GameObject effect = vfxObjects[(int)efecto].Effect;
        effect.SetActive(true);
    }
    public IEnumerator ApagarEfecto(VFXType efecto)
    {
        GameObject effect = vfxObjects[(int)efecto].Effect;
        ParticleSystem childrenPS = effect.GetComponentInChildren<ParticleSystem>();

        if(childrenPS == null)
            yield break;

        var mainModule = childrenPS.main;
        Color startColor = mainModule.startColor.color;
        float startAlpha = startColor.a;

        float elapsedTime = 0f;
        while (elapsedTime < 3f)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime/ 3f);
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
