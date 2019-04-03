using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Minijuego11 : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] Transform sprites;
    [SerializeField] Text elementText;

    public GameObject panelSprites;
    public GameObject[] arrayPrefab = new GameObject[8];
   
    private string referenciaOpcion;

    public Button btnCompare;

    public AudioSource audioAlegria;
    public AudioSource audioTristeza;
    public AudioSource audioEnojo;
    public AudioSource audioMiedo;

    private int nchar;
    private int option;
    private int count = 0;
    private int i = 0;

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

        btnCompare.onClick.AddListener(() => Validate());

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("UPDATE valor de elementText: ------------- " + elementText.text);
        Debug.Log("UPDATE valor de referenciaOPcion: ------------- "+referenciaOpcion);
        if (elementText.text.Length > 10)
        {
            panelSprites.gameObject.SetActive(false); //Desactivar panel de sprites
            
            stateSlots = true; //
            //Debug.Log("Estan Seteados los 2 slots? :" + stateSlots);
            btnCompare.gameObject.SetActive(true); //Activar el boton de comparar
        }
        else
        {
            Debug.Log("Estan Seteados los 2 slots? :" + stateSlots);
        }
  
    }

    public void Validate()
    {
        Debug.Log("VALIDATE Valor de Referencia: " + referenciaOpcion);
        Debug.Log("VALIDATE Valor de ElementText: " + elementText.text);
        

        if (elementText.text == referenciaOpcion)
        {
            Debug.Log("Son iguales, ACERTASTE");
            count++;
            elementText.text = " "; //Para que no siga entrando al if de update
            //referenciaOpcion = " ";
            btnCompare.gameObject.SetActive(false);  //Desactiva boton
            panelSprites.gameObject.SetActive(true); //Activa panel
            //Debug.Log("count: "+count);

            //Borra los sprite cargado en rostro de emoji
            foreach (Transform slotTransform in slots)
            { 
                GameObject itemSprite = slotTransform.GetComponent<SlotContent>().item;
                Destroy(itemSprite);
            }

            //Borra todos los sprite arrastrables
            foreach (Transform spriteTransform in sprites)
            {
                GameObject objSprite = spriteTransform.GetComponent<SlotContent>().item;
                Destroy(objSprite);
            }

            //Vuelve a llenar todo el panel de sprites arrastrables
            foreach (Transform spriteTransform in sprites)
            {
                GameObject objectHijo = Instantiate(arrayPrefab[i]) as GameObject;
                objectHijo.name = arrayPrefab[i].name;
                objectHijo.transform.parent = spriteTransform.transform;
                objectHijo.transform.position = spriteTransform.transform.position;
                i++;
            }
            i = 0; //reiniciar indice de arrayprefab
            
            randomPetition(); //Randomizar nuevamente
            HasChanged();
            Update();
            
            
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
                Debug.Log("Reproducir mp3 Alegria");
               // audioAlegria.Play();
                referenciaOpcion = "o_alegreb_alegre";
                break;
            case 1:
                Debug.Log("Reproducir mp3 Triste");
               // audioTristeza.Play();
                referenciaOpcion = "o_tristezab_tristeza";
                break;
            case 2:
                Debug.Log("Reproducir mp3 Miedo");
               // audioMiedo.Play();
                referenciaOpcion = "o_miedob_miedo";
                break;
            case 3:
                Debug.Log("Reproducir mp3 Enojo");
               // audioEnojo.Play();
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
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}