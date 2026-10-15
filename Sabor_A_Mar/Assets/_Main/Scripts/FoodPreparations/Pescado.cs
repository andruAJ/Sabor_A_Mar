using Oculus.Interaction;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering.Universal;

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

    public enum PescadoState
    {
        Escamas,
        SinEscamas,
        Cortado,
        Frito
    }
    private PescadoState currentState;
    private void Start()
    {
        pescadoMaterial = pescadoMeshRenderer.material;
        currentState = PescadoState.Escamas;
    }
    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.gameObject.tag;
        switch (collisionTag)
        {
            case "Cuchillo":
                Debug.LogWarning("Golpe con cuchillo");
                if (currentState == PescadoState.SinEscamas)
                {
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
                BlendTextures(0f);
                break;
            case PescadoState.SinEscamas:
                UpdateTextures(pescado_Sin_Escamas, pescado_Cortado);
                BlendTextures(0f);
                golpes = 0;
                break;
            case PescadoState.Cortado:
                UpdateTextures(pescado_Cortado, pescado_Frito);
                BlendTextures(0f);
                this.gameObject.GetComponent<PositionConstraint>().enabled = false;
                break;
            case PescadoState.Frito:
                UpdateTextures(pescado_Frito, pescado_Frito);
                BlendTextures(1f);
                break;
        }
    }
    public void UpdateTextures(Texture2D texture1, Texture2D texture2) 
    {
        pescadoMaterial.SetTexture("Texture1", texture1);
        pescadoMaterial.SetTexture("Texture2", texture2);
    }

    public void BlendTextures(float blendValue) 
    {
        pescadoMaterial.SetFloat("BlendValue", blendValue);
    }
    public IEnumerator Fritura() 
    {
        float elapsedTime = 0f;
        while (elapsedTime < 10f)
        {
            BlendTextures(elapsedTime / 10f);
            yield return null;
        }
    }
}
