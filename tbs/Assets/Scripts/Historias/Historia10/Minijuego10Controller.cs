using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minijuego10Controller : MonoBehaviour
{
    
    public Button btnEmoji1,btnEmoji2,btnEmoji3,btnEmoji4,btnCompare;
	private Image imgEmo1,imgEmo2,imgEmo3,imgEmo4; //Imagen sobre boton
	public GameObject panel;
	public GameObject[] emoji; 
	private int indice = 0, j = 0, k = 0, p = 0, q = 0;
    public Sprite[] spriteList;
    private float elapsedTime = 0;
	private float limitTime = 5;
	private bool updateOn = true;
    
    // Start is called before the first frame update
    void Start()
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

		panel.SetActive(false);

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
			//Debug.Log("Tiempo: "+elapsedTime);
			if(elapsedTime >= limitTime){
				//Debug.Log("Se Acabo el tiempo, desaparece secuencia");
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
		panel.SetActive(true);

		for (int i = 0; i < 4; i++)
		{
			emoji[i].GetComponent<Image>().enabled = false;
		}

		panel.GetComponent<Animation>().Play("Panel2");

	}
    public void setEmoji(Image imgEmo, int index)
	{
		imgEmo.sprite = spriteList[index];
	}
	public void compareEmojis()
	{
		/* Comprobacion de secuencia con imagen del panel */
		if (imgEmo1.sprite.name == emoji[0].GetComponent<Image>().sprite.name)
		{	Debug.Log("Primer par son iguales");	}
		else{	Debug.Log("Fallo, Termina el Juego");	}

		if (imgEmo2.sprite.name == emoji[1].GetComponent<Image>().sprite.name)
		{	Debug.Log("Segundo par son iguales");	}
		else{	Debug.Log("Fallo, Termina el Juego");	}

		if (imgEmo3.sprite.name == emoji[2].GetComponent<Image>().sprite.name)
		{	Debug.Log("Tercer par son iguales");	}
		else{	Debug.Log("Fallo, Termina el Juego");	}

		if (imgEmo4.sprite.name == emoji[3].GetComponent<Image>().sprite.name)
		{	Debug.Log("Cuarto par son iguales, GANASTE!!!!");	}
		else{	Debug.Log("Fallo, Termina el Juego");	}
		
	}

}
