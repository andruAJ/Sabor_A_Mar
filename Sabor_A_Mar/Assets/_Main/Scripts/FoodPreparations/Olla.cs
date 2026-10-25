using System;
using System.Collections;
using UnityEngine;

public class Olla : MonoBehaviour
{
    private Arroz_Coco.ArrozState currentState;
    public Arroz_Coco arroz_Coco;

    //bool para cocciones e ingredientes agregados
    public bool ralladuraServida = false;
    public bool lecheServida = false;
    public bool lecheCocinada = false;
    public bool arrozCocinado = false;

    //evento
    public static event Action<Arroz_Coco.ArrozState> PasoActual;
    private string collisionTag;

    private void OnTriggerEnter(Collider other)
    {
        collisionTag = other.gameObject.tag;
        currentState = arroz_Coco.CurrentState;
        
    }
    private void OnTriggerStay(Collider other) 
    {

        
        
    }
    private void EfectoLecheEnOlla() 
    {
        //Poner VFX
    }
    public bool RalladuraServida
    {
        get => ralladuraServida;
        private set
        {
            ralladuraServida = value;
        }
    }
    public IEnumerator Coccion(bool comidaAHervir)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (elapsedTime >= 5f)
        {
            if (!comidaAHervir) 
            {
                comidaAHervir = true;  
            }
        }
    }
}
