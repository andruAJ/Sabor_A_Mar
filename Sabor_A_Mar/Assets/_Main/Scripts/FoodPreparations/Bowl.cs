using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;

public class Bowl : MonoBehaviour
{
    private Arroz_Coco.ArrozState currentState;
    public Arroz_Coco arroz_Coco;
    public bool lecheServida = false;
    private string collisionTag;

    private bool anguloCorrecto = false;
    private void OnTriggerEnter(Collider other)
    {
        collisionTag = other.gameObject.tag;
        currentState = arroz_Coco.CurrentState;
        if (90 <= other.transform.rotation.x && other.transform.rotation.x >= 270) 
        {
            anguloCorrecto = true;
        }
        else if (-90 >= other.transform.rotation.x && other.transform.rotation.x <= -270) 
        {
            anguloCorrecto = true;
        }
    }
    private void OnTriggerStay(Collider other) 
    {   
        if(collisionTag == "Coco" && anguloCorrecto && currentState == Arroz_Coco.ArrozState.CocoPartido)
        {
            LecheServida = true;
            EfectoLecheEnBowl();
        }
    }
    private void EfectoLecheEnBowl() 
    {
        //Poner VFX
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
