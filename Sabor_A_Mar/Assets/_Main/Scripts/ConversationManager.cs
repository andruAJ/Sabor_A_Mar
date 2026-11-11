using System;
using System.Collections;
using TMPro;
using UnityEngine;

public enum Conversacion
{
    Bienvenida,
    DescamarPescado, CortarPescado, FritarPescado, LlevarPescadoAPlato,
    CortarPlatano, DorarPlatano, AplastarPlatano, FritarPatacones, LlevarPataconesAPlato,
    AbrirCoco, RallarCoco, ServirLecheBowl, ServirLecheOlla, ServirArrozCrudo, ServirAgua, ServirSal,TaparOlla, CogerArrozConPlatoHondo,LlevarArrozAPlato,
    PescadoEnPlato, ArrozEnPlato, PataconEnPlato,
    Final
}

[ExecuteInEditMode]
public class ConversationManager : MonoBehaviour
{
    public static ConversationManager Instance { get; private set; }

    public TextList[] textList;

    public GameObject parentTextoMama;
    public TextMeshPro cajaTexto;
    public Animator animatorMama;

    [SerializeField] private float tiempoEntrePaginas = 3f;
    private Coroutine corrutinaConversacion;
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void NextConversation(Conversacion siguienteConversacion) 
    {
        int index = (int)siguienteConversacion;

        cajaTexto.text = textList[index].Text;
        PresentarConversation();
    }
    public void PresentarConversation() 
    {
        PrenderConversacion();
        if (corrutinaConversacion != null)
        {
            StopCoroutine(corrutinaConversacion);
        }
        animatorMama.SetTrigger("Hablando");
        corrutinaConversacion = StartCoroutine(MostrarPaginas());
    }
    private IEnumerator MostrarPaginas()
    {
        cajaTexto.ForceMeshUpdate();
        int totalPaginas = cajaTexto.textInfo.pageCount;

        for (int pagina = 1; pagina <= totalPaginas; pagina++)
        {
            cajaTexto.pageToDisplay = pagina;

            yield return new WaitForSeconds(tiempoEntrePaginas);
        }

        corrutinaConversacion = null;
        ApagarConversacion();
    }
    public void PrenderConversacion() 
    {
        parentTextoMama.SetActive(true);
    }
    public void ApagarConversacion() 
    {
        parentTextoMama.SetActive(false);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(Conversacion));
        Array.Resize(ref textList, names.Length);
        for (int i = 0; i < textList.Length; i++)
        {
            textList[i].name = names[i];
        }
    }
#endif
}
[Serializable]
public struct TextList
{
    public string Text { get => texto; }
    [HideInInspector] public string name;

    [TextArea(3, 8)]
    [SerializeField] private string texto;
}

