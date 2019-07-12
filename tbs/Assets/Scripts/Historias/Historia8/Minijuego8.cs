using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Global;
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
    public GameObject panelGift;

    /* segunda parte */
    [SerializeField] Transform personajes;
    [SerializeField] Transform slot2;
    [SerializeField] Transform slot3;

    public GameObject panelPersonajes;
    public GameObject panel0;
   
    public GameObject[] arrayPrefab = new GameObject[4];

    public GameObject btnValidar;

    /* tercera parte */
    public GameObject[] gitfContainer;
    public Sprite giftFound;
    public Button[] btnDoor;
    int _iPart = 0, _iGift = 5, _iDoor = 5;
    public int _roundCount = 3, _iFound = 0,
                _iLose = 0, _iWin = 0;
    public Text roundText;
    public GameObject doorPanel;

    /* cuarta parte */
    private Animator animator;
    public GameObject piñata, orderPanel, candys;
    public Button stick;
    public int _iKnock = 1;

    /* variables generales del minijuego */
    public GameObject panel1; //primera parte 
    public GameObject panel2; //segunda parte
    public GameObject panel3; //tercera parte
    public GameObject panel4; //cuarta parte

    public GameObject panelWin;
    public GameObject btnContinue;

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
        GetGeneralVolume();
        OpenDoor();
    }
    // Update is called once per frame
    void Update()
    {
        /* Verifica si smugie ha llegado a la posicion marcada --para parte 1 de juegoo -- */
        if (smugie.GetComponent<Transform>().localPosition.x == -60)
        {
            isWalk = false;
        }

        OptionUpdate();

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
                LoseGame();
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

                _iPart = 3;

                MinigamePartThree();

                break;
            case 2:

                // desactivar canvas 3
                panel3.SetActive(false);

                // activar canvas 4
                panel4.SetActive(true);

                _iPart = 4;

                MinigamePartFour();

                break;
        }

        // sumar para que cambie a la siguiente parte de minijuego
        opcion++;

        //desactivar ventana emergente
        panelWin.SetActive(false);

        OkRound();

    }
    /* funcion lleva logica de parte 1 de minijuego */
    public void MinigamePartOne()
    {
        HasChanged();

        // Validar 
        if (elementText.text == "Gift" && hideCanvas1 == false)
        {
            Nivel.text = "1/4"; //Marca de avance de minijuego 8
            isRoundDone = true; 
            panelWin.SetActive(true);
            panelGift.SetActive(false);
            hideCanvas1 = true; //canvas 1 oculto
            hideCanvas2 = false; //canvas 2 visible
        }
    }
    /* funcion lleva logica de parte 2 de minijuego */
    public void MinigamePartTwo()
    {
        sillaVacia = Random.Range(0,4);

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
    /* Funcion para boton "VALIDAR" que permite verificar luego de posicionar smugie en sillas */
    public void ValidatePartTwo()
    { 
        if (slot3.childCount == 0)
        {
            Nivel.text = "2/4";
            panelWin.SetActive(true);
            panel0.SetActive(false);
            isRoundDone = true;
            hideCanvas2 = true; //canvas 2 oculto
            hideCanvas3 = false; //canvas 3 visible   
        }
    }
     /* funcion lleva logica de parte 3 de minijuego */
    public void MinigamePartThree()
    {
        RandomPositionGift();
    }
    /* funcion lleva logica de parte 3 de minijuego */
    private void RandomPositionGift()
    {
        _iGift = Random.Range(0, 5);
        //Debug.Log("Posicion" + _iGift);
        gitfContainer[_iGift].GetComponent<Image>().sprite = giftFound;
    }
    /* funcion lleva logica de parte 3 de minijuego */
    private void OpenDoor()
    {
        btnDoor[0].onClick.AddListener(OpenDoorZero);
        btnDoor[1].onClick.AddListener(OpenDoorOne);
        btnDoor[2].onClick.AddListener(OpenDoorTwo);
        btnDoor[3].onClick.AddListener(OpenDoorThree);
        btnDoor[4].onClick.AddListener(OpenDoorFour);
    }
    /* funcion lleva logica de parte 3 de minijuego */
    private void OpenDoorZero()
    {
        _iDoor = 0;
        _roundCount -= 1;
        if(_iDoor == _iGift)
            _iFound = 10;
        btnDoor[0].enabled = false;
        btnDoor[0].GetComponent<Image>().enabled = false;
    }
    /* funcion lleva logica de parte 3 de minijuego */
    private void OpenDoorOne()
    {
        _iDoor = 1;
        _roundCount -= 1;
        if(_iDoor == _iGift)
            _iFound = 10;
        btnDoor[1].enabled = false;
        btnDoor[1].GetComponent<Image>().enabled = false;
    }
    /* funcion lleva logica de parte 3 de minijuego */
    private void OpenDoorTwo()
    {
        _iDoor = 2;
        _roundCount -= 1;
        if(_iDoor == _iGift)
            _iFound = 10;
        btnDoor[2].enabled = false;
        btnDoor[2].GetComponent<Image>().enabled = false;
    }
    /* funcion lleva logica de parte 3 de minijuego */
    private void OpenDoorThree()
    {
        _iDoor = 3;
        _roundCount -= 1;
        if(_iDoor == _iGift)
            _iFound = 10;
        btnDoor[3].enabled = false;
        btnDoor[3].GetComponent<Image>().enabled = false;
    }
    /* funcion lleva logica de parte 3 de minijuego */
    private void OpenDoorFour()
    {
        _iDoor = 4;
        _roundCount -= 1;
        if(_iDoor == _iGift)
            _iFound = 10;
        btnDoor[4].enabled = false;
        btnDoor[4].GetComponent<Image>().enabled = false;
    }
    /* funcion lleva logica de parte 3 de minijuego */
    private void VerifiedRoundCount(){
        if(_roundCount == 0 && _iFound == 0 && _iLose == 0){
            //PERDISTE
            _iLose += 1;
            LoseGame();
        }
        else if(_iFound == 10 && _roundCount >= 0 && _iWin == 0){
            //GANASTE
            _iWin += 1;
            doorPanel.SetActive(false);
            Nivel.text = "3/4"; //Marca de avance de minijuego 8
            isRoundDone = true;
            panelWin.SetActive(true);
        }
        roundText.text = "Oportunidades: " + _roundCount.ToString("0") + "/3";
    }
    /* funcion lleva logica de parte 4 de minijuego */
    public void MinigamePartFour()
    {
        animator = piñata.GetComponent<Animator>();
        stick.onClick.AddListener(GetSmaller);
    }
    /* funcion lleva logica de parte 4 de minijuego */
    private void GetSmaller(){
        if(_iKnock == 1){
            animator.SetTrigger("knock1");
            _iKnock += 1;
        }
        else if(_iKnock == 2){
            animator.SetTrigger("knock2");
            _iKnock += 1;
        }
        else if(_iKnock == 3){
            animator.SetTrigger("knock3");
            _iKnock += 1;
        }
        else if(_iKnock == 4){
            animator.SetTrigger("knock4");
            _iKnock += 1;
        }
        else if(_iKnock == 5){
            animator.SetTrigger("knock5");
            _iKnock += 1;
        }
        else if(_iKnock == 6){
            animator.SetTrigger("knock6");
            orderPanel.SetActive(false);
            candys.SetActive(true);
            Nivel.text = "4/4"; //Marca de avance de minijuego 8
            isRoundDone = true;
            OkRound();
            WinGame();
        }
    }
    /* funcion para identificar que metodos se ejecutaran en Update */
    private void OptionUpdate(){
        if(_iPart == 3){
            VerifiedRoundCount();
        }
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
        audioSource.clip = bgMusic;
        audioSource.Play(0);

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
    void LoseGame()
    {
        panelGift.SetActive(false);
        panelPersonajes.SetActive(false);
        panel4.SetActive(false);
        di.UpdateLevelTimesPlayed(8);
        di.ResetLevelSuccessTimeByLevel(8);
        audioSource.Stop();
        gs = new GameStatus();
        gs.PlayerNeedToRepeatGame(audioSource, waitingTime, 8);
        if(_iPart == 3)
            doorPanel.SetActive(false);
    }
    void WinGame()
    {
        isGameDone = true;
        di.UpdateLevelTimesPlayed(8);

        if (bestScore == score)
            di.UpdateBestScoreForLevel(8, score);
        di.UpdateTotalizedScore(score);

        di.SaveSuccesTime(new LevelSuccessTime()
        {
            LevelID = 8,
            SuccessTime = dgb.CalculateAverageRound(totalTimeByGame, 8)
        });

        audioSource.Stop();
        gs = new GameStatus();
        gs.PlayerWinGame(audioSource, waitingTime = 3, 8);
    }

    private void GetGeneralVolume()
    {
        float generalVolume = 0.0f;
        generalVolume = GlobalVariables.GeneralVolume;
        audioSource.volume = generalVolume;
    }
}

