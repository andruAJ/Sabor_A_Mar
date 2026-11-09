using System.Collections;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    //estado actual del coco
    private Arroz_Coco.ArrozState currentState;

    public Arroz_Coco arroz_Coco;

    public GameObject lecheEnElPlato;

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
    private IEnumerator EfectoLecheEnBowl() 
    { 
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            lecheEnElPlato.SetActive(true);
            elapsedTime += Time.deltaTime;
            lecheEnElPlato.transform.Translate(Vector3.up * Time.deltaTime * 0.02f);
            yield return null;
        }
    }
    private void LecheSiendoServida() 
    {
        if (LecheServida)
            return;

        if (collisionTag == "Coco" &&
        (currentState == Arroz_Coco.ArrozState.CocoPartido ||
         currentState == Arroz_Coco.ArrozState.CocoRallado))
        {
            LecheServida = true;
            arroz_Coco.gameObject.GetComponent<PourDetector>().enabled = false;
            gameObject.tag = "BowlConLeche";

            StartCoroutine(EfectoLecheEnBowl());
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
