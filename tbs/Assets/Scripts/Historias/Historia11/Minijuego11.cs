using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Minijuego11 : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] Text elementText;

    public GameObject panelSprites;
   
    private string referenciaOpcion = "";

    public Button btnCompare;

    public AudioSource audioAlegria;
    public AudioSource audioTristeza;
    public AudioSource audioEnojo;
    public AudioSource audioMiedo;

    private int nchar;
    private int option;

    private bool stateSlots = false;

    // Start is called before the first frame update
    void Start()
    {
        randomPetition();

        audioAlegria = GetComponent<AudioSource>();
        audioTristeza = GetComponent<AudioSource>();
        audioEnojo = GetComponent<AudioSource>();
        audioMiedo = GetComponent<AudioSource>();

        HasChanged();

        btnCompare.onClick.AddListener(() => Validate(elementText.text));

    }

    // Update is called once per frame
    void Update()
    {
        controlSlot();
        /*
        if (elementText.text.Length > 10)
        {
            panelSprites.gameObject.SetActive(false); //Desactivar panel de sprites
            stateSlots = true; //
            Debug.Log("Estan Seteados los 2 slots? :" + stateSlots);
            btnCompare.gameObject.SetActive(true); //Activar el boton de comparar
        }
        else
        {
            Debug.Log("Estan Seteados los 2 slots? :" + stateSlots);
        }
        */
    }



    public void Validate(string texto)
    {
        //Debug.Log("----------Valor de Texto Final a comparar : "+texto);
        //Debug.Log("----------Valor de Texto referencia a comparar : " + referenciaOpcion);

        if (texto == referenciaOpcion)
        {
            Debug.Log("Son iguales, ACERTASTE");
        }
        else
        {
            Debug.Log("Son diferentes, FALLASTE");
        }

    }

    public void randomPetition()
    {
        switch (Random.Range(0, 5))
        {
            case 0:
                //Debug.Log("Reproducir mp3 Alegria");
                audioAlegria.Play();
                referenciaOpcion = "o_alegreb_alegre";
                break;
            case 1:
                //Debug.Log("Reproducir mp3 Triste");
                audioTristeza.Play();
                referenciaOpcion = "o_tristezab_tristeza";
                break;
            case 2:
                //Debug.Log("Reproducir mp3 Miedo");
                audioMiedo.Play();
                referenciaOpcion = "o_miedob_miedo";
                break;
            case 3:
                //Debug.Log("Reproducir mp3 Enojo");
                audioEnojo.Play();
                referenciaOpcion = "o_enojob_enojo";
                break;
        }

    }
    
    public void HasChanged()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        //builder.Append(" - ");
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<SlotContent>().item;
            if (item)
            {
                builder.Append(item.name);
                //builder.Append(" - ");
            }
        }
        elementText.text = builder.ToString();
    }

    public void controlSlot()
    {
        if (elementText.text.Length > 10)
        {
            panelSprites.gameObject.SetActive(false); //Desactivar panel de sprites
            stateSlots = true; //
            Debug.Log("Estan Seteados los 2 slots? :" + stateSlots);
            btnCompare.gameObject.SetActive(true); //Activar el boton de comparar
        }
        else
        {
            Debug.Log("Estan Seteados los 2 slots? :" + stateSlots);
        }
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}