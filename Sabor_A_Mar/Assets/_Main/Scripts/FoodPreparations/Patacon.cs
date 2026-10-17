using UnityEngine;
using System.Collections;

public class Patacon : MonoBehaviour
{
    //MeshesIntercambiables
    public Mesh platano_Pelado;
    public Mesh platano_Cortado; 
    public Mesh patacon_Frito1;
    public Mesh patacon_Frito2;

    //Hijos
    public GameObject platano;
    public GameObject cascaras;
    public GameObject platano_Cortado_Dorado;
    public GameObject pataconCrudo;

    //MeshFilter
    public MeshFilter platanoMeshFilter;
    public MeshFilter cascarasMeshFilter;
    public MeshFilter pataconMeshFilter;

    public enum PataconState
    {
        Verde,
        Pelado,
        Cortado,
        Dorado,
        Aplastado,
        Frito
    }
    private PataconState currentPataconState;
    private void OnCollisionEnter(Collision collision) 
    {
        string collisionTag = collision.gameObject.tag;
        switch (collisionTag) 
        { 
            case "Hand":
                
                break;
            case "Cuchillo":
                
                break;
            case "Pataconera":
                
                break;
            default:
                break;
        }
    }
    public void SetPataconState(PataconState state) 
    {
        currentPataconState = state;
        switch (currentPataconState) 
        {
            case PataconState.Verde:
                TurnOffAllHijosExcept(platano);
                break;
            case PataconState.Pelado:
                TurnOffAllHijosExcept(platano);
                cascaras.SetActive(true);
                break;
            case PataconState.Cortado:
                
                break;
            case PataconState.Dorado:
                
                break;
            case PataconState.Aplastado:
                
                break;
            case PataconState.Frito:
                
                break;
        }
        Debug.LogWarning("Estado del patacón cambiado a: " + currentPataconState);
    }
    public void TurnOffAllHijosExcept(GameObject except) 
    {
        foreach (Transform child in transform) 
        {
            if (child.gameObject != except) 
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    public IEnumerator Fritura()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 3f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if(elapsedTime >= 3f)
        {
            SetPataconState(PataconState.Frito);
        }
    }
}
