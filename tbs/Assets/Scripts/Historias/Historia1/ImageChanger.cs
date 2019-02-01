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
    private float speedX, speedY;
    public GameObject[] sleepers;
    public GameObject[] colliders;





    //Animaciones
    //public AnimationClip sleep1, sleep11;
    //private Animation aniSleep1, aniSleep11;

    public GameObject sleeper;

    
    private Animator animator;







    // Start is called before the first frame update
    void Start()
    {   
        
        
        
        //SendAnimation("sleep0");
        
        
        //GetAnimations();



        sleepers[0].GetComponent<SpriteRenderer>().enabled = false;
        //btnSend.SetActive(false);
        RandomPosition();
        ImageChangerBtn();
        btnSend.onClick.AddListener(ImageSendBtn);
    }

    public void NextSprite()
    {
        //btnSend.SetActive(true);
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

                switch(j){
                    case 0:
                        Debug.Log(imgNameArg + " " + j);
                        speedX = -200f;
                        speedY = 100f;
                        sleepers[j].GetComponent<SpriteRenderer>().enabled = true;
                        sleepers[j].GetComponent<Rigidbody2D>().velocity = new Vector3(speedX, speedY, 0);
                        /* if (OnTriggerEnter2D(colliders[0].GetComponent<Collider2D>())){
                               
                        }*/


                        if(sleepers[j].transform.position.y > 235 || sleepers[j].transform.position.x < -121){
                            sleepers[j].GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
                        }

                        SendAnimation("sleep1");
                        //aniSleep1.GetComponent<Animation>().Play("sleep1");
                        //aniSleep11.GetComponent<Animation>.Play("sleep11");
                        

                    break;
                    case 1:
                        Debug.Log(imgNameArg + " " + j);
                        speedX = 2f;

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
                    default:
                        Debug.Log("El juego ha terminado");
                    break;
                }
            
            j++;
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

    //public void GetAnimations(){
        //aniSleep1 = sleeper.AddComponent<Animation>();
        //aniSleep1.AddClip(sleep1, "Animations/sleep1");
    //}

    /* bool OnTriggerEnter2D(Collider2D other){
        Debug.Log("Encontrado");
        return true;
    }

    */
    public void SendAnimation(string state = null){

        if(state != null)
            animator.Play(state);

    }


}
