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
        var rot = other.transform.localEulerAngles;
        float x = rot.x % 360;
        float z = rot.z % 360;

        bool xIsMax = Mathf.Abs(x) > Mathf.Abs(z);
        float maxValue = xIsMax ? x : z;
        float delta = Mathf.DeltaAngle(0, maxValue);
        if (Mathf.Abs(delta) > 90f && Mathf.Abs(delta) < 270f)
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
                DebugInfoGenerator.LogWarning("Leche fue servida en el bowl");
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
