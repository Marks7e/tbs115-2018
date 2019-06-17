using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Global;
using Assets.Scripts.DataPersistence.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minijuego6 : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioClip bgMusic;

    public AudioSource sourceBtn1;
    public AudioSource sourceBtn2;
    public AudioSource sourceBtn3;
    public AudioSource audioSource;

    public Button btnAudio1;
    public Button btnAudio2;
    public Button btnAudio3;
    public Button btnEmoji1;
    public Button btnEmoji2;
    public Button btnEmoji3;
    public Button btnCompare;

    public GameObject btnContinue;
    public GameObject msj_ok;

    public GameObject panelWin;

    public Image icon1;
    public Image icon2;
    public Image icon3;

    public Sprite[] spriteList;
    public Sprite defaultBoton;

    public int bestScore = 0;
    public int score = 0;
    public int totalTimeByGame = 0;
    public int dbRoundtime = 0;
    public Text BestScore, Score;
    public float timeLeft = 5.00f;
    public int waitingTime = 3;

    public GameStatus gs;
    public PlayerData pd;
    public LevelData ld;

    public DependencyInjector di;

    //Imagen sobre boton
    private Image _imgEmo1;
    private Image _imgEmo2;
    private Image _imgEmo3;

    private int _iClip;

    private int _j = 0;
    private int _k = 0;
    private int _p = 0;
    private int _count = 0;


    public bool isGameDone = false;
    public bool isRoundDone = false;

    //Texto a mostrar al usuario
    public Text Nivel;
    public Text timing;
    public GameObject texto;

    // Start is called before the first frame update
    void Start()
    {
        GetAndInitializeAllGameObjects();
        InitializeRecordAndScore();
        GetGeneralVolume();
        btnContinue.GetComponent<Button>().onClick.AddListener(() => OkRound());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameDone)
        {
            if (!isRoundDone)
            {
                timeLeft -= Time.deltaTime;
                timing.text = "Tiempo: " + timeLeft.ToString("0");
            }

            if (timeLeft <= 0 && !isGameDone)
            {
                LoseGame();
            }
        }
    }

    private void LoseGame()
    {
        isGameDone = true;
        di.UpdateLevelTimesPlayed(6);
        audioSource.Stop();
        di.ResetLevelSuccessTimeByLevel(6);
        gs = new GameStatus();
        gs.PlayerNeedToRepeatGame(audioSource, waitingTime, 1);
    }
    /* RandomAudio: cambia el audio de cada boton, representado por sprite de smugie */
    public void RandomAudio()
    {
        for (int i = 0; i < 3; i++)
        {
            _iClip = Random.Range(0, 6);

            //Set audio en boton 1,2,3
            SetAudioButton(i, _iClip);
        }

    }

    /* SetAudioButton: asigna al audiosource el audio seleccionado por la funcion RandomAudio */
    public void SetAudioButton(int btnAudio, int indice)
    {
        switch (btnAudio)
        {
            case 0:
                sourceBtn1.clip = clips[indice];
                break;
            case 1:
                sourceBtn2.clip = clips[indice];
                break;
            case 2:
                sourceBtn3.clip = clips[indice];
                break;
        }
    }

    /* Play: Activa el icono de bocina, sobre el emoji que reproduce el cumplido */
    public void Play(int option)
    {
        switch (option)
        {
            case 1:
                sourceBtn2.Stop();
                sourceBtn3.Stop();
                icon2.enabled = false;
                icon3.enabled = false;

                icon1.enabled = true;
                sourceBtn1.Play();
                break;
            case 2:
                sourceBtn1.Stop();
                sourceBtn3.Stop();
                icon1.enabled = false;
                icon3.enabled = false;

                icon2.enabled = true;
                sourceBtn2.Play();
                break;
            case 3:
                sourceBtn1.Stop();
                sourceBtn2.Stop();
                icon1.enabled = false;
                icon2.enabled = false;

                icon3.enabled = true;
                sourceBtn3.Play();
                break;
        }
    }

    /* actions: proporciona accion de botones que tienen imagenes de emojis */
    public void Actions(int btn)
    {
        //Debug.Log("Nombre del Boton: "+btn);
        switch (btn)
        {
            case 1:
                SetEmoji(_imgEmo1, _j);
                if (_j < 1) { _j++; } else { _j = 0; }
                break;
            case 2:
                SetEmoji(_imgEmo2, _k);
                if (_k < 1) { _k++; } else { _k = 0; }
                break;
            case 3:
                SetEmoji(_imgEmo3, _p);
                if (_p < 1) { _p++; } else { _p = 0; }
                break;
        }

    }

    /* setEmoji: cambia la imagen de emoji en los botones donde interactua el usuario */
    public void SetEmoji(Image imgEmo, int index)
    {
        //Debug.Log("Valor de index: "+index);
        imgEmo.sprite = spriteList[index];
    }

    public void Compare()
    {

        if (btnAudio1.GetComponent<AudioSource>().clip.name.Split(' ')[1] == _imgEmo1.sprite.name &&
            btnAudio2.GetComponent<AudioSource>().clip.name.Split(' ')[1] == _imgEmo2.sprite.name &&
            btnAudio3.GetComponent<AudioSource>().clip.name.Split(' ')[1] == _imgEmo3.sprite.name)
        {
            //Debug.Log("/********** HAS ACERTADO *********/");
            isRoundDone = true;
            panelWin.SetActive(true);
        }
        else
        {
            //Debug.Log("/********** FALLO, Uno o MAS NO SON CORRECTOS **************/");
            isRoundDone = true;
            LoseGame();
        }


    }

    /* Reestablece escenario para proxima iteracion */
    public void Iteration()
    {

        //Coloca imagen base en boton
        _imgEmo1.sprite = defaultBoton;
        _imgEmo2.sprite = defaultBoton;
        _imgEmo3.sprite = defaultBoton;

        //incremento contador 
        _count++;

        //Setando Texto
        Nivel.text = _count + "/2";

        if (_count >= 2)
        {
            Complete();
        }
        else
        {
            // cambiando el orden de los audios en sprites
            RandomAudio();
        }

    }

    public void Complete()
    {
        /*Desactivando botones*/
        btnAudio1.enabled = false;
        btnAudio2.enabled = false;
        btnAudio3.enabled = false;
        btnEmoji1.enabled = false;
        btnEmoji2.enabled = false;
        btnEmoji3.enabled = false;
        btnCompare.enabled = false;

        isGameDone = true;

        audioSource.Stop();

        if (bestScore == score)
            di.UpdateBestScoreForLevel(6, score);
        di.UpdateTotalizedScore(score);

        gs = new GameStatus();
        gs.PlayerWinGame(audioSource, waitingTime, 6);

    }

    private void GetAndInitializeAllGameObjects()
    {
        audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>("Sounds/Minigame");
        audioSource.clip = bgMusic;
        audioSource.Play(0);

        /* inicializacion*/
        btnEmoji1.onClick.AddListener(() => Actions(1));
        btnEmoji2.onClick.AddListener(() => Actions(2));
        btnEmoji3.onClick.AddListener(() => Actions(3));

        _imgEmo1 = GameObject.Find("imgEmo1").GetComponent<Image>();
        _imgEmo2 = GameObject.Find("imgEmo2").GetComponent<Image>();
        _imgEmo3 = GameObject.Find("imgEmo3").GetComponent<Image>();


        RandomAudio();

        btnAudio1.onClick.AddListener(() => Play(1));
        btnAudio2.onClick.AddListener(() => Play(2));
        btnAudio3.onClick.AddListener(() => Play(3));
        btnCompare.onClick.AddListener(Compare);


        texto = new GameObject();
        texto = GameObject.Find("Timing");
        timing = texto.GetComponent<Text>();

        timing.text = "Tiempo";

        /*Para control de puntajes.*/
        var objBestScore = GameObject.Find("BestScore");
        var objScore = GameObject.Find("Score");
        BestScore = objBestScore.GetComponent<Text>();
        Score = objScore.GetComponent<Text>();

    }

    private void SettingTimeOfGame()
    {
        timeLeft = ld.RoundTime;
    }

    private void InitializeRecordAndScore()
    {
        di = new DependencyInjector();
        pd = new PlayerData();
        ld = new LevelData();

        pd = di.GetAllPlayerData();
        ld = di.GetLevelData(6);

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
        panelWin.SetActive(false);  //Desactiva panel de mensaje de ganador de ronda
        isRoundDone = false;
        Iteration();                //Reinicia el escenario

    }

    private void GetGeneralVolume()
    {
        float generalVolume = 0.0f;
        generalVolume = GlobalVariables.GeneralVolume;
        audioSource.volume = generalVolume;
    }
}
