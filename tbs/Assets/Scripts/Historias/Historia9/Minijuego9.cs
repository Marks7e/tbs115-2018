using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minijuego9 : MonoBehaviour
{

    public Button[] btnEmoji;
    public Image light;
    public Button btnCompare;
    public Sprite[] spriteList;
    public Sprite[] lights;
    private int _indice = 0, _j = 0;
    private int _red = 0, _green = 0, _yellow = 0, _red2 = 0, _green2 = 0, _yellow2 = 0;
    private string _angry = "Enojo", _neutral = "Tristeza", _happy = "Alegria";
    public string color="";

    // Start is called before the first frame update
    void Start()
    {
        GetAndInitializeAllGameObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetAndInitializeAllGameObjects()
    {
		GetAndInitializeRed();
    }

    private void GetAndInitializeRed()
    {
        SetLights(0);
        RandomSequence(1);
		btnEmoji[0].onClick.AddListener(() => ActionsRed(0));
		btnEmoji[1].onClick.AddListener(() => ActionsRed(1));
		btnEmoji[2].onClick.AddListener(() => ActionsRed(2));
		btnEmoji[3].onClick.AddListener(() => ActionsRed(3));
        btnCompare.onClick.AddListener(CompareEmojis);
    }

    private void GetAndInitializeYellow()
    {
        EnableButtons();
        RandomSequence(2);
        SetLights(1);
		btnEmoji[0].onClick.AddListener(() => ActionsYellow(0));
		btnEmoji[1].onClick.AddListener(() => ActionsYellow(1));
		btnEmoji[2].onClick.AddListener(() => ActionsYellow(2));
		btnEmoji[3].onClick.AddListener(() => ActionsYellow(3));
        btnCompare.onClick.AddListener(CompareEmojis);
    }

    private void GetAndInitializeGreen()
    {
        SetLights(2);
        EnableButtons();
        RandomSequence(3);
		btnEmoji[0].onClick.AddListener(() => ActionsGreen(0));
		btnEmoji[1].onClick.AddListener(() => ActionsGreen(1));
		btnEmoji[2].onClick.AddListener(() => ActionsGreen(2));
		btnEmoji[3].onClick.AddListener(() => ActionsGreen(3));
        btnCompare.onClick.AddListener(CompareEmojis);
    }

    /* RandomSequence: Crea la secuencia de emojis */
    public void RandomSequence(int index)
    {
        switch(index)
        {
            case 1:
                for (int i = 0; i < 4; i++)
                {
                    /* Obtiene indice al azar para llamar a la imagen guardada en spriteList[] */
                    _indice = Random.Range(0, 4);
                    btnEmoji[i].GetComponent<Image>().sprite = spriteList[_indice];
                    //Contabiliza los emojis enojo
                    if(btnEmoji[i].GetComponent<Image>().sprite.name == _angry)
                        _red += 1;
                    Debug.Log("posicion: "+ i +" ,nombre: "+ spriteList[_indice].name + " Enojo: " + _red );
                }
            break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    _indice = Random.Range(1, 5);
                    btnEmoji[i].GetComponent<Image>().sprite = spriteList[_indice];
                    //Contabiliza los emojis neutrales
                    if(btnEmoji[i].GetComponent<Image>().sprite.name == _neutral)
                        _yellow += 1;
                    Debug.Log("posicion: "+ i +" ,nombre: "+ spriteList[_indice].name + " Neutral: " + _yellow);
                }
            break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    _indice = Random.Range(2, 6);
                    btnEmoji[i].GetComponent<Image>().sprite = spriteList[_indice];
                    //Contabiliza los emojis alegres
                    if(btnEmoji[i].GetComponent<Image>().sprite.name == _happy)
                        _green += 1;
                    Debug.Log("posicion: "+ i +" ,nombre: "+ spriteList[_indice].name + " Alegre: " + _green);
                }
            break;
            default:
                _red = 0; _yellow = 0; _green = 0;
            break;
        }
        
    }

    /* actions: proporciona accion de botones que tienen imagenes de emojis */
	public void ActionsRed(int btn)
	{
        if(color == "red")
        {
            switch (btn)
            {
                case 0:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _angry)
                        _red2 += 1;
                    else
                        _red2 -= 1;
                    Debug.Log("Eliminadas -> Enojo: " + _red2 );
                    break;
                case 1:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _angry)
                        _red2 += 1;
                    else
                        _red2 -= 1;
                    Debug.Log("Eliminadas -> Enojo: " + _red2 );
                    break;
                case 2:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _angry)
                        _red2 += 1;
                    else
                        _red2 -= 1;
                    Debug.Log("Eliminadas -> Enojo: " + _red2 );
                    break;
                case 3:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _angry)
                        _red2 += 1;
                    else
                        _red2 -= 1;
                    Debug.Log("Eliminadas -> Enojo: " + _red2 );
                    break;
            }
        }
    }

    public void ActionsYellow(int btn)
	{
        if(color == "yellow")
        {
            switch (btn)
            {
                case 0:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _neutral)
                        _yellow2 += 1;
                    else
                        _yellow2 -= 1;
                    Debug.Log("Eliminadas -> Neutral: " + _yellow2 );
                    break;
                case 1:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _neutral)
                        _yellow2 += 1;
                    else
                        _yellow2 -= 1;
                    Debug.Log("Eliminadas -> Neutral: " + _yellow2 );
                    break;
                case 2:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _neutral)
                        _yellow2 += 1;
                    else
                        _yellow2 -= 1;
                    Debug.Log("Eliminadas -> Neutral: " + _yellow2 );
                    break;
                case 3:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _neutral)
                        _yellow2 += 1;
                    else
                        _yellow2 -= 1;
                    Debug.Log("Eliminadas -> Neutral: " + _yellow2 );
                    break;
            }
        }
	}

    public void ActionsGreen(int btn)
	{
        if(color == "green")
        {
            _j = 1;
            switch (btn)
            {
                case 0:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _happy)
                        _green2 += 1;
                    else
                        _green2 -= 1;
                    Debug.Log("Eliminadas -> Alegre: " + _green2 );
                    break;
                case 1:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _happy)
                        _green2 += 1;
                    else
                        _green2 -= 1;
                    Debug.Log("Eliminadas -> Alegre: " + _green2 );
                    break;
                case 2:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _happy)
                        _green2 += 1;
                    else
                        _green2 -= 1;
                    Debug.Log("Eliminadas -> Alegre: " + _green2 );
                    break;
                case 3:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _happy)
                        _green2 += 1;
                    else
                        _green2 -= 1;
                    Debug.Log("Eliminadas -> Alegre: " + _green2 );
                    break;
            }
        }
	}

    public void CompareEmojis()
	{
		/* Comprobacion de caritas enojadas */
        if(color == "red")
        {
            if (_red == _red2)
            {
                Debug.Log("Todos Coinciden!!!");
                GetAndInitializeYellow();
            }
            else
            {
                Debug.Log("FALLO, Uno o mas no coinciden");
            }
        }
        else if(color == "yellow")
        {
            /* Comprobacion de caritas neutrales */
            if (_yellow == _yellow2)
            {
                Debug.Log("Todos Coinciden!!! Neutrales");
                GetAndInitializeGreen();
            }
            else
            {
                Debug.Log("FALLO, Uno o mas no coinciden neutrales");
            }
        }
        else if(color == "green" && _j == 1)
        {
            /* Comprobacion de caritas alegres */
            if (_green == _green2)
            {
                Debug.Log("Todos Coinciden!!! Alegres");
            }
            else
            {
                Debug.Log("FALLO, Uno o mas no coinciden alegres " + _green + " " + _green2);
            }
        }
	}

    public void SetLights(int _light)
    {
        switch (_light)
		{
            case 0:
                color = "red";
                light.GetComponent<Image>().sprite = lights[_light];
            break;
            case 1:
                color = "yellow";
                light.GetComponent<Image>().sprite = lights[_light];
            break;
            case 2:
                color = "green";
                light.GetComponent<Image>().sprite = lights[_light];
            break;
            default:
                color = "";
            break;
        }
    }

    public void EnableButtons()
    {
        for (int i = 0; i < 4; i++)
        {
				btnEmoji[i].GetComponent<Image>().enabled = true;
		}
    }
}
