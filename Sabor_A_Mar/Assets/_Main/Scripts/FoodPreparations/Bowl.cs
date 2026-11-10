using System.Collections;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    //estado actual del coco
    private Arroz_Coco.ArrozState currentState;

    public Arroz_Coco arroz_Coco;

    public GameObject lecheEnElPlato;
    public GameObject arrozListo;

    //pa saber si la leche fue servida
    public bool lecheServida = false;
    public bool arrozServido = false;
    private string collisionTag;
    private void OnTriggerEnter(Collider other)
    {
        collisionTag = other.gameObject.tag;
        currentState = arroz_Coco.CurrentState;
        if (collisionTag == "Coco") { LecheSiendoServida(); }
        else if (collisionTag == "Olla" && currentState == Arroz_Coco.ArrozState.ArrozListo) {arrozListo.SetActive(true); arrozServido = true; }
        else if (collisionTag == "Plato" && arrozServido) { }
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

        if (currentState == Arroz_Coco.ArrozState.CocoPartido || currentState == Arroz_Coco.ArrozState.CocoRallado)
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
    public bool ArrozServido
    {
        get => arrozServido;
        private set
        {
            arrozServido = value;
        }
    }
}
