using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Minijuego8 : MonoBehaviour, IHasChanged
{
    /* primera parte */
    [SerializeField] Transform slotFriend;
    [SerializeField] Transform slot;
    [SerializeField] Text elementText;

    /* segunda parte */
    //[SerializeField] Transform slots;
    [SerializeField] Transform personajes;
    [SerializeField] Transform slot2;
    [SerializeField] Text elementText2;

    public GameObject[] arrayPrefab = new GameObject[4];

    
    /* variables generales del minijuego */
    public GameObject canvas1; //primera parte 
    public GameObject canvas2; //segunda parte
    public GameObject canvas3; //tercera parte
    public GameObject canvas4; //cuarta parte

    public GameObject panelWin;
    public GameObject panelLose;
    public GameObject btnReset, btnContinue;

    private bool hideCanvas1 = false;
    private int opcion = 0;
    private int sillaVacia;
    private int _i = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        HasChanged();
        
        btnContinue.GetComponent<Button>().onClick.AddListener(() => OkRound());
        btnReset.GetComponent<Button>().onClick.AddListener(() => ReloadGame());
    }

    // Update is called once per frame
    void Update()
    {

        if (elementText.text == "Gift" && hideCanvas1 == false)
        {
            //Debug.Log("Parte 1(ronda 1) Completada, ocultar canvas, llamar siguiente fase"); //Funcion que desactiva panel de sprites y activa boton
            //canvas1.SetActive(false);
            panelWin.SetActive(true);
            hideCanvas1 = true;
        }
    }

    // Accion del boton 'continue' del panel emergente
    public void OkRound()
    {
        switch (opcion)
        {
            case 0:

                // activar animacion sonriendo

               

                // desactivar canvas 1
                canvas1.SetActive(false);

                // activar canvas 2
                canvas2.SetActive(true);

                MinigamePartTwo();

                break;
            case 1:

                // activar animacion sonriendo

                // desactivar canvas 2
                canvas2.SetActive(false);

                // activar canvas 3
                canvas3.SetActive(true);

                break;
            case 2:

                // desactivar canvas 3
                canvas3.SetActive(false);

                // activar canvas 4
                canvas4.SetActive(true);

                break;
        }

        // sumar para que cambie a la siguiente parte de minijuego
        opcion++;
        //Debug.Log("Valor de Opcion: "+opcion);

        //desactivar ventana emergente
        panelWin.SetActive(false);

    }

    // Accion del Reinicia el juego al fallar
    public void ReloadGame()
    {
        Debug.Log("Reiniciar Juego"); //Funcion que desactiva panel de sprites y activa boton

    }

    public void MinigamePartTwo()
    {
        sillaVacia = Random.Range(0,4);

        Debug.Log("DENTRO DE FUNCION MINIJUEGOPARTTWO, silla vacia: "+sillaVacia);

        //Llenando las sillas a excepcion de una
        //Llenar todo el panel de sprites arrastrables
        foreach (Transform spriteTransform in personajes)
        {
            if (sillaVacia != _i) {
                GameObject objectHijo = Instantiate(arrayPrefab[_i]) as GameObject;
                objectHijo.name = arrayPrefab[_i].name;
                objectHijo.transform.SetParent(spriteTransform.transform);
                objectHijo.transform.position = spriteTransform.transform.position;
            }
            _i++;
        }


    }

    //Obtiene el valor del sprite 
    public void HasChanged()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();

        foreach (Transform slotTransform in slotFriend)
        {
            GameObject item = slotTransform.GetComponent<SlotContent>().item;
            if (item)
            {
                builder.Append(item.name);
            }
        }
        elementText.text = builder.ToString();
        
    }

    
}

