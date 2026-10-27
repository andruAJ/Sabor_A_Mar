using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class EmplatadoFinal : MonoBehaviour
{
    //Evento
    public static event Action <String> ComidaEnPlato;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        //ComidaEnPlato?.Invoke(comidaEmplatada);
    }
}
