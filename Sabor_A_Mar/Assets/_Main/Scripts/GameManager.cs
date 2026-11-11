using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool seAcercoAlPlatano = false;
    public bool seAcercoAlPescado = false;
    public bool seAcercoAlCoco = false;


    public bool pescadoEmplatado;
    public bool pataconesEmplatados;
    public bool arrozEmplatado;

    private bool comidaEmplatada = false;

    public Transform platanoPosition;
    public Transform pescadoPosition;
    public Transform cocoPosition;
    public GameObject deteccionDentroCasa;
    public static GameManager Instance { get; private set; }
    private void Start()
    {
        Instance = this;
        VFX_Manager.Instance.PrenderEfecto(VFXType.Interactable, false, this.transform);
        InicioExperiencia();
    }

    private void InicioExperiencia() 
    {
        ConversationManager.Instance.NextConversation(Conversacion.Bienvenida);
    }
    public void EstablecerEfectoInteractables()
    {
        if (!seAcercoAlCoco)
        {
            VFX_Manager.Instance.PrenderEfecto(VFXType.Interactable, true, cocoPosition);
        }
        else if (!seAcercoAlPescado)
        {
            VFX_Manager.Instance.PrenderEfecto(VFXType.Interactable, true, pescadoPosition);
            deteccionDentroCasa.SetActive(true);
        }
        else if (!seAcercoAlPlatano)
        {
            VFX_Manager.Instance.PrenderEfecto(VFXType.Interactable, true, platanoPosition);
        }
    }
    public void FinalExperiencia()
    {
        if (pescadoEmplatado == true && pataconesEmplatados == true && arrozEmplatado == true) { comidaEmplatada = true; }
        
        if(comidaEmplatada)
        {
            ConversationManager.Instance.NextConversation(Conversacion.Final);
            //Fade out a final de la experiencia

        }
    }
    public IEnumerator EsperarAlSiguienteDialogo() 
    {
        float elapsedTime = 0f;
        while (elapsedTime < 8f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (comidaEmplatada) { SceneManager.LoadScene("LastScene"); }
        if (arrozEmplatado) { ConversationManager.Instance.NextConversation(Conversacion.CortarPlatano); }
        else if (pescadoEmplatado) { ConversationManager.Instance.NextConversation(Conversacion.AbrirCoco);}
        
    }
    public bool SeAcercoAlPlatano
    {
        get => seAcercoAlPlatano;
        set { seAcercoAlPlatano = value; }
    }
    public bool SeAcercoAlPescado
    {
        get => seAcercoAlPescado;
        set { seAcercoAlPescado = value; }
    }
    public bool SeAcercoAlCoco
    {
        get => seAcercoAlCoco;
        set { seAcercoAlCoco = value; }
    }
    public bool PescadoEmplatado
    {
        get => pescadoEmplatado;
        set { pescadoEmplatado = value; ConversationManager.Instance.NextConversation(Conversacion.PescadoEnPlato); StartCoroutine(EsperarAlSiguienteDialogo()); }
        
    }
    public bool PataconesEmplatados
    {
        get => pataconesEmplatados;
        set {pataconesEmplatados = value; ConversationManager.Instance.NextConversation(Conversacion.PataconEnPlato); StartCoroutine(EsperarAlSiguienteDialogo()); }
    }
    public bool ArrozEmplatado
    {
        get => arrozEmplatado;
        set { arrozEmplatado = value; ConversationManager.Instance.NextConversation(Conversacion.ArrozEnPlato); StartCoroutine(EsperarAlSiguienteDialogo()); }
    }
    
}
