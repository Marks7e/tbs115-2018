using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{

    public Sprite[] imageList; //Lista para imagenes
    public Button btn, btnSend; //Boton de cambio y enviar smugie dormido
    private int i = 0, j = 0;
    private Image imgBtn; //Imagen sobre boton
    private string imgName = " ";

    // Start is called before the first frame update
    void Start()
    {
        RandomPosition();
        ImageChangerBtn();
        btnSend.onClick.AddListener(ImageSendBtn);
    }

    public void NextSprite()
    {
        imgBtn.sprite = imageList[i];
        imgName = imageList[i].name;

	    if (i < 4){ 
            i ++; 
        }else{ 
            i = 0;
            RandomPosition(); 
        }
    }

    public void ImageChangerBtn()
    {
        btn.onClick.AddListener(NextSprite);
        imgBtn = GameObject.Find("imgChange").GetComponent<Image>();
    }

    public void RandomPosition()
    {
        int index = Random.Range(0, imageList.Length);
        Sprite img = null;

        switch (index){
            case 0:
                Debug.Log("indice 0,sumar" + index);
                img = imageList[index];
                imageList[index] = imageList[index + 1];
                imageList[index + 1] = img;
                break;
            case 2:
                Debug.Log("Indice 2, restar" + index);
                img = imageList[index];
                imageList[index] = imageList[index - 1];
                imageList[index - 1] = img;
                break;
            default:
                Debug.Log("Indice 1, sumar o restar" + index);
                img = imageList[index];
                imageList[index] = imageList[index - 1];
                imageList[index - 1] = img;
                break;
        }

    }

    public void ImageSend(string imgNameArg)
    { 
        if(imgNameArg == "sleep"){

                j++;

                switch(j){
                    case 1:
                        Debug.Log(imgNameArg + " " + j);
                    break;
                    case 2:
                        Debug.Log(imgNameArg + " " + j);
                    break;
                    case 3:
                        Debug.Log(imgNameArg + " " + j);
                    break;
                    case 4:
                        Debug.Log(imgNameArg + " " + j);
                    break;
                    case 5:
                        Debug.Log(imgNameArg + " " + j);
                    break;
                    default:
                        Debug.Log("El juego ha terminado");
                    break;
                }
            
            NextSprite();

        } 
        else{
            Debug.Log("NOOOOO");
        }
    }

    public void ImageSendBtn()
    {
        if(j < 5){
            ImageSend(imgName);
        }
    }


}
