using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minijuego10Controller : MonoBehaviour
{
    public Button btnEmoji1,btnEmoji2,btnEmoji3,btnEmoji4,btnCompare;
	private Image imgEmo1,imgEmo2,imgEmo3,imgEmo4; //Imagen sobre boton
	public GameObject panelBotones,panelSecuencia,btnReset, btnContinue;
	public GameObject[] emoji; 
    public GameObject msj_ok, msj_fail;
	private int indice = 0, j = 0, k = 0, p = 0, q = 0;
    public Sprite[] spriteList;
	public Sprite defaultBoton;
    private float elapsedTime = 0;
	private float hideTime = 5;
	private bool updateOn = true;
	public Text Nivel;
    private int count = 1;

    // Start is called before the first frame update
    void Start()
    {
		GetAndInitializeAllGameObjects();

		/* Funcion que crea secuencia de emojis de manera aleatoria */
		randomSequence();
    }

    // Update is called once per frame
    void Update()
    {
        /* Evita | Permite la Ejecucion del codigo */
		if (updateOn == true)
		{
			elapsedTime += Time.deltaTime;
			Debug.Log("Tiempo transcurrido: "+elapsedTime);
			if(elapsedTime >= hideTime){
				//Al alcanzar hideTime, oculta la secuencia de emojis
				elapsedTime = 0;
				updateOn = false;
				hideSequence();
			}
		}

    }
    public void randomSequence()
    {
        /* Llena el vector de emojis que se van a mostrar como secuencia de emojis */
        for (int i = 0; i < 4; i++)
        {
            /* Obtiene indice al azar para llamar a la imagen guardada en spriteList[] */
            indice = Random.Range(0, 5);
            emoji[i].GetComponent<Image>().sprite = spriteList[indice];
			Debug.Log("posicion: "+i+" ,nombre: "+spriteList[indice].name);
        }
    }
    public void actions(int btn)
	{
		//Debug.Log("Nombre del Boton: "+btn);
		switch (btn)
		{
			case 1:
				setEmoji(imgEmo1,j);
				if (j<4){ j++; }else{ j = 0; }
				break;
			case 2:
				setEmoji(imgEmo2,k);
				if (k<4){ k++; }else{ k = 0; }
				break;
			case 3:
				setEmoji(imgEmo3,p);
				if (p<4){ p++; }else{ p = 0; }
				break;
			case 4:
				setEmoji(imgEmo4,q);
				if (q<4){ q++; }else{ q = 0; } 
				break;
		}

	}
    public void hideSequence()
	{
		for (int i = 0; i < 4; i++)
		{
			//Oculta cada elemento de imagen de la secuencia
			emoji[i].GetComponent<Image>().enabled = false;
		}

		//Activa panel y animacion con botones
		panelBotones.SetActive(true);
		panelBotones.GetComponent<Animation>().Play("Panel2");

	}
    public void setEmoji(Image imgEmo, int index)
	{
		imgEmo.sprite = spriteList[index];
	}
	public void compareEmojis()
	{
		/* Comprobacion de secuencia con imagen del panel */
		if (imgEmo1.sprite.name == emoji[0].GetComponent<Image>().sprite.name && 
		imgEmo2.sprite.name == emoji[1].GetComponent<Image>().sprite.name && 
		imgEmo3.sprite.name == emoji[2].GetComponent<Image>().sprite.name && 
		imgEmo4.sprite.name == emoji[3].GetComponent<Image>().sprite.name)
		{
			Debug.Log("Todos Coinciden!!!");
			Ok();
		}else
		{
			Debug.Log("FALLO, Uno o mas no coinciden");
			fail();
		}
		
	}
	
	//Mensaje de respuesta correcta
    public void Ok()
    {
        //UpdateScore();
        //SettingTimeOfGame();
        msj_ok.SetActive(true);
        btnContinue.SetActive(true);
		btnCompare.enabled = false;
        //isRoundDone = true;
    }
    //Mensaje de Respuesta incorrecta
    public void fail()
    {
        //score -= 800;
        msj_fail.SetActive(true);
        btnReset.SetActive(true);
        //isRoundDone = true;
    }
	public void iteracion()
    {
        //isRoundDone = false;

        //Setando Texto
        Nivel.text = count + "/3";

        //Contador
        count++;

        msj_ok.SetActive(false);
        btnContinue.SetActive(false);
		btnCompare.enabled = true;

		//Coloca imagen base en boton
		imgEmo1.sprite = defaultBoton;
		imgEmo2.sprite = defaultBoton;
		imgEmo3.sprite = defaultBoton;
		imgEmo4.sprite = defaultBoton;

		panelBotones.SetActive(false);
		
        //Generar una nueva secuencia
		
		if(count <= 3){
			randomSequence();
			updateOn = true;
			for (int i = 0; i < 4; i++)
			{
				//Visualizar cada elemento de imagen de la secuencia
				emoji[i].GetComponent<Image>().enabled = true;
			}
		}else{
			Debug.Log("----**********--- JUEGO FINALIZADO ----**********---");
		}

    }
	private void GetAndInitializeAllGameObjects()
    {
		btnEmoji1.onClick.AddListener(() => actions(1));
		btnEmoji2.onClick.AddListener(() => actions(2));
		btnEmoji3.onClick.AddListener(() => actions(3));
		btnEmoji4.onClick.AddListener(() => actions(4));
		btnCompare.onClick.AddListener(compareEmojis);

		imgEmo1 = GameObject.Find("imgEmo1").GetComponent<Image>();
		imgEmo2 = GameObject.Find("imgEmo2").GetComponent<Image>();
		imgEmo3 = GameObject.Find("imgEmo3").GetComponent<Image>();
		imgEmo4 = GameObject.Find("imgEmo4").GetComponent<Image>();

		panelBotones.SetActive(false);

	}
}
