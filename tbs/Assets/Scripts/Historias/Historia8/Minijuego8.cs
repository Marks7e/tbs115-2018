﻿using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using Assets.Scripts.Utils;
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
    public GameObject smugie;

    /* segunda parte */
    [SerializeField] Transform personajes;
    [SerializeField] Transform slot2;
    [SerializeField] Transform slot3;
   
    public GameObject[] arrayPrefab = new GameObject[4];

    public GameObject btnValidar;

    /* variables generales del minijuego */
    public GameObject panel1; //primera parte 
    public GameObject panel2; //segunda parte
    public GameObject panel3; //tercera parte
    public GameObject panel4; //cuarta parte

    public GameObject panelWin;
    public GameObject panelLose;
    public GameObject btnReset, btnContinue;


    /* variables de persistenca y audio minijuego */
    public AudioSource audioSource;
    public AudioClip bgMusic;

    //Texto a mostrar al usuario
    public Text BestScore, Score;
    public Text Nivel;
    public Text timing;
    public float timeLeft = 0f;

    public GameObject texto;
    public int bestScore = 0;
    public int score = 0;
    public int waitingTime = 3;
    public int totalTimeByGame = 0;
    public int dbRoundtime = 0;

    //Dependencias
    public DependencyInjector di;
    public DynamicGameBalance dgb;
    public PlayerData pd;
    public LevelData ld;

    public GameStatus gs;

    public bool isGameDone = false;
    public bool isRoundDone = false;
    public bool isWalk = true;

    private bool hideCanvas1 = false;
    private bool hideCanvas2 = true;
    private bool hideCanvas3 = true;

    private int opcion = 0;
    private int sillaVacia;
    private int _i = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
        GetAndInitializeAllGameObjects();

        InitializeRecordAndScore();

        /*
        MinigamePartOne();

        btnContinue.GetComponent<Button>().onClick.AddListener(() => NextPart());
        btnReset.GetComponent<Button>().onClick.AddListener(() => ReloadGame());
        btnValidar.GetComponent<Button>().onClick.AddListener(() => ValidatePartTwo());
        */
    }

    // Update is called once per frame
    void Update()
    {
        /* Verifica si smugie ha llegado a la posicion marcada --para parte 1 de juegoo -- */
        if (smugie.GetComponent<Transform>().localPosition.x == -60)
        {
            isWalk = false;
        }
        
        if (!isGameDone) //Si isGameDone es falso, entra
        {
            //Debug.Log("Juego no ha terminado");
            
            if (!isRoundDone && !isWalk)
            {
                timeLeft -= Time.deltaTime;
                timing.text = "Tiempo: " + timeLeft.ToString("0");
            }

            if (timeLeft <= 0 && !isGameDone)
            {
                isGameDone = true;
                di.UpdateLevelTimesPlayed(8);
                audioSource.Stop();
                di.ResetLevelSuccessTimeByLevel(8);
                gs = new GameStatus();
                gs.PlayerNeedToRepeatGame(audioSource, waitingTime, 8);
            }

            if (hideCanvas1 == false) //ronda 1
            {
                MinigamePartOne();
            }
           
        }
       
    }

    // Accion del boton 'continue' del panel emergente para pasar a siguiente parte de juego
    public void NextPart()
    {
        switch (opcion)
        {
            case 0:

                // activar animacion sonriendo

                // desactivar canvas 1
                panel1.SetActive(false);

                // activar canvas 2
                panel2.SetActive(true);

                MinigamePartTwo();
                
                break;
            case 1:

                // desactivar canvas 2
                panel2.SetActive(false);

                // activar canvas 3
                panel3.SetActive(true);

                MinigamePartThree();

                break;
            case 2:

                // desactivar canvas 3
                panel3.SetActive(false);

                // activar canvas 4
                panel4.SetActive(true);

                break;
        }

        // sumar para que cambie a la siguiente parte de minijuego
        opcion++;
        //Debug.Log("Valor de Opcion: "+opcion);

        //desactivar ventana emergente
        panelWin.SetActive(false);

        OkRound();

    }

    /* funcion lleva logica de parte 1 de minijuego */
    public void MinigamePartOne()
    {
        //Debug.Log("********************* Dentro de MinigamePartOne *************************");

        HasChanged();

        // Validar 
        if (elementText.text == "Gift" && hideCanvas1 == false)
        {
            Nivel.text = "1/4"; //Marca de avance de minijuego 8
            isRoundDone = true; 
            panelWin.SetActive(true);
            hideCanvas1 = true; //canvas 1 oculto
            hideCanvas2 = false; //canvas 2 visible

        }
       
    }

    /* funcion lleva logica de parte 2 de minijuego */
    public void MinigamePartTwo()
    {
        sillaVacia = Random.Range(0,4);
   
        //Debug.Log("DENTRO DE FUNCION MINIJUEGOPARTTWO, silla vacia: "+sillaVacia);
        Debug.Log("******************** Dentro de MinigamePartTwo *************************");

        //Debug.Log("Valor de slot2: "+slot2.childCount);

        //Llenando las sillas a excepcion de una
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

    /* funcion lleva logica de parte 3 de minijuego */
    public void MinigamePartThree()
    {
        Debug.Log("******************* Dentro de MinigamePartThree ************************");
        //desactivar ventana emergente
        panelWin.SetActive(false);
        GameComplete();
        
    }

    /* Funcion para boton "VALIDAR" que permite verificar luego de posicionar smugie en sillas */
    public void ValidatePartTwo()
    {
        
        if (slot3.childCount == 0)
        {
            Debug.Log(" GANASTES!!, Pasa a tercera parte");
            Nivel.text = "2/4";
            panelWin.SetActive(true);
            isRoundDone = true;
            hideCanvas2 = true; //canvas 2 oculto
            hideCanvas3 = false; //canvas 3 visible
            
        }
        
    }

    public void GameComplete()
    {
        Debug.Log("********************** JUEGO TERMINADO **************************************");
        /*Desactivando botones*/
        
        isGameDone = true;

        audioSource.Stop();

        if (bestScore == score)
            di.UpdateBestScoreForLevel(8, score);
        di.UpdateTotalizedScore(score);

        gs = new GameStatus();
        gs.PlayerWinGame(audioSource, waitingTime, 8);
    }

    // Sirve para saber si se ha movido el regalo
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

    /* Funciones de inicializacion */ 
    public void GetAndInitializeAllGameObjects()
    {
        
        audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>("Sounds/Minigame");
        audioSource.PlayOneShot(bgMusic);

        texto = new GameObject();
        texto = GameObject.Find("Timing");
        timing = texto.GetComponent<Text>();

        timing.text = "Tiempo";

        /*Para control de puntajes.*/
        var objBestScore = GameObject.Find("BestScore");
        var objScore = GameObject.Find("Score");
        BestScore = objBestScore.GetComponent<Text>();
        Score = objScore.GetComponent<Text>();
        di = new DependencyInjector();
        dgb = new DynamicGameBalance();
        timeLeft = di.GetRoundTime(8);

        MinigamePartOne();

        btnContinue.GetComponent<Button>().onClick.AddListener(() => NextPart());
        btnReset.GetComponent<Button>().onClick.AddListener(() => ReloadGame());
        btnValidar.GetComponent<Button>().onClick.AddListener(() => ValidatePartTwo());

    }

    private void SettingTimeOfGame()
    {
        timeLeft = di.GetRoundTime(8);
    }

    private void InitializeRecordAndScore()
    {
        di = new DependencyInjector();
        pd = new PlayerData();
        ld = new LevelData();

        pd = di.GetAllPlayerData();
        ld = di.GetLevelData(8);
        dbRoundtime = di.GetRoundTime(8);
        score = 0;
        bestScore = ld.BestScore;

        BestScore.text = "Record: " + ld.BestScore;
        Score.text = "Puntaje: " + score;
        SettingTimeOfGame();

    }

    private void UpdateScore()
    {
        double res = 100 * (timeLeft * ld.PointMultiplier);
        score += (int)res;
        Score.text = "Puntaje: " + score;

        if (bestScore < score)
        {
            bestScore = score;
            BestScore.text = "Record: " + score;
        }

    }

    //Mensaje de respuesta correcta
    public void OkRound()
    {
        totalTimeByGame += dbRoundtime - (int)timeLeft;
        UpdateScore();
        SettingTimeOfGame();        //Reinicia el tiempo
        isRoundDone = false;     

    }

    // Accion del Reinicia el juego al fallar
    public void ReloadGame()
    {
        Debug.Log("Reiniciar Juego"); //Funcion que desactiva panel de sprites y activa boton
        UnityEngine.SceneManagement.SceneManager.LoadScene("Minijuego 8");

    }

}
