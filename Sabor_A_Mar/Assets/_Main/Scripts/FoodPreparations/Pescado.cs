using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class Pescado : MonoBehaviour
{
    //Textures
    public Texture2D pescado_Escamas;
    public Texture2D pescado_Sin_Escamas;
    public Texture2D pescado_Cortado;
    public Texture2D pescado_Frito;

    //MeshRenderer
    public MeshRenderer pescadoMeshRenderer;

    //Material
    public Material pescadoMaterial;

    //Golpes con otros objetos
    private int golpes = 0;

    //Valor del blend
    private float blendValue = 0f;

    //Eventos
    public static event Action <bool, GameObject> OnPreparacionPescado;
    public static event Action<bool> PescadoListo;

    public enum PescadoState
    {
        Escamas,
        SinEscamas,
        Cortado,
        Frito
    }
    private PescadoState currentState;
    private void OnEnable()
    {
        pescadoMaterial = pescadoMeshRenderer.material;
        currentState = PescadoState.Escamas;
        OnPreparacionPescado?.Invoke(false, this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)        //debería corregir esta función para que detecte colisiones trigger, pero sólo hacer de último para más profesionalismo, ya que funcione bien cómo está
    {
        string collisionTag = collision.gameObject.tag;
        switch (collisionTag)
        {
            case "Cuchillo":
                Debug.LogWarning("Golpe con cuchillo");
                if (currentState == PescadoState.SinEscamas)
                {
                    Debug.LogWarning("Golpe con cuchillo y estado SinEscamas");
                    golpes++;
                    blendValue += 0.35f;
                    BlendTextures(blendValue);
                    if (golpes >= 3)
                    {
                        SetPescadoState(PescadoState.Cortado);
                    }
                }   
                break;
            case "Descamador":
                Debug.LogWarning("Golpe con descamador");
                if (currentState == PescadoState.Escamas)
                {
                    Debug.LogWarning("Golpe con descamador y estado Escamas");
                    golpes++;
                    blendValue += 0.35f;
                    BlendTextures(blendValue);
                    if (golpes >= 3)
                    {
                        SetPescadoState(PescadoState.SinEscamas);

                    }
                }
                break;
            default:
                break;
        }
    }
    public void SetPescadoState(PescadoState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case PescadoState.Escamas:
                UpdateTextures(pescado_Escamas, pescado_Sin_Escamas);
                blendValue = 0f;
                BlendTextures(0f);
                break;
            case PescadoState.SinEscamas:
                UpdateTextures(pescado_Sin_Escamas, pescado_Cortado);
                blendValue = 0f;
                BlendTextures(0f);
                golpes = 0;
                break;
            case PescadoState.Cortado:
                UpdateTextures(pescado_Cortado, pescado_Frito);
                blendValue = 0f;
                BlendTextures(0f);
                this.gameObject.GetComponent<PositionConstraint>().enabled = false;
                OnPreparacionPescado?.Invoke(true, this.gameObject);
                break;
            case PescadoState.Frito:
                UpdateTextures(pescado_Frito, pescado_Frito);
                this.gameObject.GetComponent<PositionConstraint>().enabled = false;
                BlendTextures(1f);
                break;
        }
        Debug.LogWarning("Estado del pescado cambiado a: " + currentState.ToString());
    }
    public void UpdateTextures(Texture2D texture1, Texture2D texture2) 
    {
        pescadoMaterial.SetTexture("_Texture1", texture1);
        pescadoMaterial.SetTexture("_Texture2", texture2);
    }

    public void BlendTextures(float blendValue) 
    {
        pescadoMaterial.SetFloat("_CombiningValue", blendValue);
    }
    public IEnumerator Fritura() 
    {
        float elapsedTime = 0f;
        while (elapsedTime < 10f)
        {
            elapsedTime += Time.deltaTime;
            BlendTextures(elapsedTime / 10f);
            yield return null;
        }
    }
    public PescadoState CurrentState 
    {
        get => currentState;
    }
}
