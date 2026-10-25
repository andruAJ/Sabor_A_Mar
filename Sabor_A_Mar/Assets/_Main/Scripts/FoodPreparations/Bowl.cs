using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;

public class Bowl : MonoBehaviour
{
    //estado actual del coco
    private Arroz_Coco.ArrozState currentState;

    public Arroz_Coco arroz_Coco;

    //pa saber si la leche fue servida
    public bool lecheServida = false;
    private string collisionTag;
    private void OnTriggerEnter(Collider other)
    {
        collisionTag = other.gameObject.tag;
        currentState = arroz_Coco.CurrentState;      
    }
    private void OnTriggerStay(Collider other) 
    {
        if (90 <= other.transform.rotation.x && other.transform.rotation.x >= 270)
        {
            LecheSiendoServida();
        }
        else if (-90 >= other.transform.rotation.x && other.transform.rotation.x <= -270)
        {
            LecheSiendoServida();
        }
        
    }
    private void EfectoLecheEnBowl() 
    {
        //Poner VFX
    }
    private void LecheSiendoServida() 
    {
        if (collisionTag == "Coco")
        {
            if (currentState == Arroz_Coco.ArrozState.CocoPartido || currentState == Arroz_Coco.ArrozState.CocoRallado) 
            {
                LecheServida = true;
                this.gameObject.tag = "BowlConLeche";
                EfectoLecheEnBowl();
            }  
        }
    }
    public bool LecheServida
    {
        get => lecheServida;
        private set
        {
            lecheServida = value;
        }
    }
}
