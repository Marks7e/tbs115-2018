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

    public AudioSource audioPetition;
    public AudioClip[] audioClipArray; 

    private int nchar;
    private int option;
    private int count = 1;
    private int i = 0;

    private void Awake()
    {
        audioPetition = GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
        GetAndInitializeAllGameObjects();

        HasChanged();

        btnCompare.onClick.AddListener(() => Validate());

    }

    // Update is called once per frame
    void Update()
    {
       
            if (elementText.text.Length > 10)
            {
                //Debug.Log(" palabra tiene mas de 10 letras");
                DisablePanelSprites(); //Funcion que desactiva panel de sprites y activa boton
            }
     
         
    }

    public void GetAndInitializeAllGameObjects()
    {
        RandomPetition();
    }

    public void DisablePanelSprites()
    {
        panelSprites.gameObject.SetActive(false);
        btnCompare.gameObject.SetActive(true);
    }

    public void EnablePanelSprite()
    {
        btnCompare.gameObject.SetActive(false);  //Desactiva boton
        panelSprites.gameObject.SetActive(true); //Activa panel
    }

    public void resetStage()
    {
        elementText.text = " ";

        referenciaOpcion = " ";

        EnablePanelSprite();
        
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
            //objectHijo.transform.parent = spriteTransform.transform;
            objectHijo.transform.SetParent(spriteTransform.transform);
            objectHijo.transform.position = spriteTransform.transform.position;
            i++;
        }

        i = 0;
        
    }



    public void Validate()
    {
        Debug.Log("intento: " + count);

        if (count < 3)
        {
            if (elementText.text == referenciaOpcion)
            {
                Debug.Log("Son iguales, ACERTASTE");

                count++;

                resetStage();
                RandomPetition();
            }
            else
            {
                Debug.Log("Son diferentes, FALLASTE");
                count = 0; //reinicia los intentos
            }
        }
        else
        {
            btnCompare.gameObject.SetActive(false);
            GameObject.Find("Base_Rostro").SetActive(false); //disable Panel: Base_Rostro
            //Borra los sprite cargado en rostro de emoji
            foreach (Transform slotTransform in slots)
            {
                GameObject itemSprite = slotTransform.GetComponent<SlotContent>().item;
                Destroy(itemSprite);
            }

            Debug.Log("--*-*-*-*-*-*-*-*-*-*- Juego Terminado -*-*-*-*-*-*-**-*-*-*-*-*--*");

        }

    }

    public void RandomPetition()
    {
        int op = UnityEngine.Random.Range(0, audioClipArray.Length);

        switch (op)
        {
            case 0:
                audioPetition.clip = audioClipArray[0];
                referenciaOpcion = "o_alegreb_alegre";
                break;
            case 1:
                 audioPetition.clip = audioClipArray[1];
                referenciaOpcion = "o_tristezab_tristeza";
                break;
            case 2:
                audioPetition.clip = audioClipArray[2];
                referenciaOpcion = "o_miedob_miedo";
                break;
            case 3:
                 audioPetition.clip = audioClipArray[3];
                referenciaOpcion = "o_enojob_enojo";
                break;
        }

        audioPetition.PlayOneShot(audioPetition.clip);


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