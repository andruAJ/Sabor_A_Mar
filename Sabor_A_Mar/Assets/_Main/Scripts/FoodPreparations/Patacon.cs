using System;
using System.Collections;
using UnityEngine;

public class Patacon : MonoBehaviour
{
    //MeshesIntercambiables
    public Mesh platano_Pelado;           // usar con el GameObject paso1
    public Mesh platano_Cortado;          // usar con el GameObject paso1
    public Mesh patacon_Frito1;          // usar con el GameObject pataconCrudo1.hijo1 y pataconCrudo2.hijo1
    public Mesh patacon_Frito2;          // usar con el GameObject pataconCrudo1.hijo2 y pataconCrudo2.hijo2

    //Hijos
    public GameObject platano;
    public GameObject cascaras;
    public GameObject platano_Cortado_Dorado;
    public GameObject pataconCrudo;

    //MeshFilter
    public MeshFilter platanoMeshFilter;
    public MeshFilter patacon1Hijo1MeshFilter;
    public MeshFilter patacon1Hijo2MeshFilter;
    public MeshFilter patacon2Hijo1MeshFilter;
    public MeshFilter patacon2Hijo2MeshFilter;

    //Eventos
    public static event Action<bool, GameObject> OnPreparacionPatacon;

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

    private void Start()
    {
        currentPataconState = PataconState.Verde;
        SetPataconState(currentPataconState);
        OnPreparacionPatacon?.Invoke(false, this.gameObject);
    }
    private void OnCollisionEnter(Collision collision) 
    {
        string collisionTag = collision.gameObject.tag;
        switch (collisionTag)
        {
            case "Hand":
                if (currentPataconState == PataconState.Verde)
                {
                    SetPataconState(PataconState.Pelado);
                }
                break;
            case "Cuchillo":
                if(currentPataconState == PataconState.Pelado)
                {
                    SetPataconState(PataconState.Cortado);
                }
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
                ChangeMesh(platanoMeshFilter, platano_Pelado);
                cascaras.SetActive(true);
                break;
            case PataconState.Cortado:
                TurnOffAllHijosExcept(platano);
                ChangeMesh(platanoMeshFilter, platano_Cortado);
                break;
            case PataconState.Dorado:
                TurnOffAllHijosExcept(platano_Cortado_Dorado);
                break;
            case PataconState.Aplastado:
                TurnOffAllHijosExcept(pataconCrudo);
                break;
            case PataconState.Frito:
                TurnOffAllHijosExcept(pataconCrudo);
                ChangeMesh(patacon1Hijo1MeshFilter, patacon_Frito1);
                ChangeMesh(patacon1Hijo2MeshFilter, patacon_Frito2);
                ChangeMesh(patacon2Hijo1MeshFilter, patacon_Frito1);
                ChangeMesh(patacon2Hijo2MeshFilter, patacon_Frito2);
                OnPreparacionPatacon?.Invoke(true, this.gameObject);
                break;
        }
        Debug.LogWarning("Estado del patacón cambiado a: " + currentPataconState);
    }
    public void TurnOffAllHijosExcept(GameObject except) 
    {
        foreach (Transform child in transform.GetChild(0)) 
        {
            if (child.gameObject != except) 
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    public void ChangeMesh(MeshFilter meshFilter, Mesh newMesh)
    {
        if (meshFilter != null && newMesh != null)
        {
            meshFilter.mesh = newMesh;
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
