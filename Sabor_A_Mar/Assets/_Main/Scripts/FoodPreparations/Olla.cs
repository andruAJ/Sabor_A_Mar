using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;

public class Olla : MonoBehaviour
{
    private Arroz_Coco.ArrozState currentState;
    public Arroz_Coco arroz_Coco;
    public bool ralladuraServida = false;
    private string collisionTag;

    private void OnTriggerEnter(Collider other)
    {
        collisionTag = other.gameObject.tag;
        currentState = arroz_Coco.CurrentState;
        
    }
    private void OnTriggerStay(Collider other) 
    {

        RalladuraServida = true;
            EfectoLecheEnOlla();
        
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
}
