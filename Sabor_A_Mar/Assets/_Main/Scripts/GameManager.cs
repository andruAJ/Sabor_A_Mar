using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool pescadoEmplatado;
    public bool pataconesEmplatados;
    public bool arrozEmplatado;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
