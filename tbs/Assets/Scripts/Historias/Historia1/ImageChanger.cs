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
    public Sprite icono;
    private string iName = "sleep";
    public GameObject imageSelector;

    public GameObject[] sleepers; //Lista animaciones
    private Animator animator; //Animator de las animaciones

    //Ganar/Perder
    public GameStatus gs;
    public AudioSource audioSource;
    public AudioClip bgMusic;
    public int waitingTime = 3;
    private string musicName = "Sounds/Minigame";

    // Start is called before the first frame update
    void Start()
    {   
        GetInitializeMusic();
        ButtonInit();
        SleepersInactive();
        RandomPosition();
        ImageChangerBtn();
        btnSend.onClick.AddListener(ImageSendBtn);
    }

    public void NextSprite()
    {
        ButtonActive();
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
        imgBtn.sprite = icono;
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
        if(imgNameArg == iName){

                switch(j){
                    case 0:
                        SleepersActive();
                    break;
                    case 1:
                        SleepersActive();
                    break;
                    case 2:
                        SleepersActive();
                    break;
                    case 3:
                        SleepersActive();
                    break;
                    case 4:
                        SleepersActive();
                        WinGame();
                    break;
                    default:
                        LoseGame();
                    break;
                }
            
            j++;

            ButtonInit();

        } 
        else{
            LoseGame();
        }
    }

    public void ImageSendBtn()
    {
        if(j < 5){
            ImageSend(imgName);
        }
    }

    void SleepersInactive()
    {
        for(int k=0; k<sleepers.Length; k++)
        {
            sleepers[k].SetActive(false);
        }
    }

    void SleepersActive()
    {
        sleepers[j].SetActive(true);
        animator = sleepers[j].GetComponent<Animator>();
        animator.SetTrigger("SleepActivate");
    }

    void ButtonInit()
    {
        btnSend.enabled = false;
        btnSend.GetComponent<Image>().enabled = false;
        btn.GetComponent<Image>().enabled = false;
        ImageChangerBtn();
    }

    void ButtonActive()
    {
        btnSend.enabled = true;
        btnSend.GetComponent<Image>().enabled = true;
        btn.GetComponent<Image>().enabled = true;
    }

    void GetInitializeMusic()
    {
        audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>(musicName);
        audioSource.clip = bgMusic;
        audioSource.Play(0);
    }

    void WinGame()
    {
        audioSource.Stop();
        gs = new GameStatus();
        gs.PlayerWinGame(audioSource, waitingTime = 3);
        imageSelector.SetActive(false);
    }

    void LoseGame()
    {
        audioSource.Stop();
        gs = new GameStatus();
        gs.PlayerNeedToRepeatGame(audioSource, waitingTime);
        imageSelector.SetActive(false);
    }
}
