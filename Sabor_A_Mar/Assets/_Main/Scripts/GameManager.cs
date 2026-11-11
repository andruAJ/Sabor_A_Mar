using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool seAcercoAlPlatano;
    public bool seAcercoAlPescado;
    public bool seAcercoAlCoco;


    public bool pescadoEmplatado;
    public bool pataconesEmplatados;
    public bool arrozEmplatado;

    public Transform platanoPosition;
    public Transform pescadoPosition;
    public Transform cocoPosition;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        VFX_Manager.Instance.PrenderEfecto(VFXType.Interactable, false, this.transform);
    }

    private void InicioExperiencia() 
    {
        //poner texto de mama clemen diciendo los ingredientes
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
        }
        else if (!seAcercoAlPlatano)
        {
            VFX_Manager.Instance.PrenderEfecto(VFXType.Interactable, true, platanoPosition);
        }
    }
    public void FinalExperiencia()
    {
        bool comidaEmplatada = pescadoEmplatado  == true && pataconesEmplatados  == true && arrozEmplatado == true;
        if(comidaEmplatada)
        {
            //audio de felicitación
            //Fade out a final de la experiencia
        }
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
        set { pescadoEmplatado = value; }
        
    }
    public bool PataconesEmplatados
    {
        get => pataconesEmplatados;
        set {pataconesEmplatados = value; }
    }
    public bool ArrozEmplatado
    {
        get => arrozEmplatado;
        set { arrozEmplatado = value; }
    }
    
}
