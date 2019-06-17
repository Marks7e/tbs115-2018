using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Global;
using Assets.Scripts.DataPersistence.Models;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minijuego9 : MonoBehaviour
{

    //Mecanica de juego 9
    public Button[] btnEmoji;
    public Image light;
    public Button btnCompare;
    public Sprite[] spriteList;
    public Sprite[] lights;
    private int _indice = 0, _j = 0, _k = 0;
    private int _red = 0, _green = 0, _yellow = 0, _red2 = 0, _green2 = 0, _yellow2 = 0;
    private int _iWin0 = 0, _iWin1 = 0, _iWin2 = 0, _iLose = 0;
    private string _angry = "Enojo", _neutral = "Neutro", _happy = "Alegria";
    public string color="";

    //Mensaje Ganar/Perder
    public GameStatus gs;
    public AudioSource audioSource;
    public AudioClip bgMusic;
    private string _musicName = "Sounds/Minigame";

    public DynamicGameBalance dgb = null;
    public float timeLeft = 0f;
    private int _nivel = 9;

    //Texto a mostrar al usuario
    public Text Nivel;
    public Text timing;
    public Text BestScore;
    public Text Score;
    public GameObject texto;
    public int bestScore = 0;
    public int score = 0;
    public int waitingTime = 3;
    public int dbRoundtime = 0;
    public int totalTimeByGame = 0;
    public bool isGameDone = false;
    public bool isRoundDone = false;
    public PlayerData pd;
    public LevelData ld;

    public DependencyInjector di;

    // Start is called before the first frame update
    void Start()
    {
        GetAndInitializeAllGameObjects();
        GetInitializeMusic();
        GetGeneralVolume();
        InitializeRecordAndScore();
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
                isGameDone = true;
                LoseGame();
            }
        }
    }
    private void GetAndInitializeAllGameObjects()
    {
        /*Para control de puntajes.*/
        di = new DependencyInjector();
        var objBestScore = GameObject.Find("BestScore");
        var objScore = GameObject.Find("Score");
        BestScore = objBestScore.GetComponent<Text>();
        Score = objScore.GetComponent<Text>();
        texto = GameObject.Find("Timing");
        timing = texto.GetComponent<Text>();
        dgb = new DynamicGameBalance();

		GetAndInitializeRed();
    }
    private void GetAndInitializeRed()
    {
        timeLeft = di.GetRoundTime(_nivel);
        SetLights(0);
        RandomSequence(1);
		btnEmoji[0].onClick.AddListener(() => ActionsRed(0));
		btnEmoji[1].onClick.AddListener(() => ActionsRed(1));
		btnEmoji[2].onClick.AddListener(() => ActionsRed(2));
		btnEmoji[3].onClick.AddListener(() => ActionsRed(3));
        btnCompare.onClick.AddListener(ActionsCompare);
    }
    private void GetAndInitializeYellow()
    {
        timeLeft = di.GetRoundTime(_nivel);
        isRoundDone = false;
        EnableButtons();
        RandomSequence(2);
        SetLights(1);
		btnEmoji[0].onClick.AddListener(() => ActionsYellow(0));
		btnEmoji[1].onClick.AddListener(() => ActionsYellow(1));
		btnEmoji[2].onClick.AddListener(() => ActionsYellow(2));
		btnEmoji[3].onClick.AddListener(() => ActionsYellow(3));
        btnCompare.onClick.AddListener(ActionsCompare);
    }
    private void GetAndInitializeGreen()
    {
        timeLeft = di.GetRoundTime(_nivel);
        isRoundDone = false;
        SetLights(2);
        EnableButtons();
        RandomSequence(3);
		btnEmoji[0].onClick.AddListener(() => ActionsGreen(0));
		btnEmoji[1].onClick.AddListener(() => ActionsGreen(1));
		btnEmoji[2].onClick.AddListener(() => ActionsGreen(2));
		btnEmoji[3].onClick.AddListener(() => ActionsGreen(3));
        btnCompare.onClick.AddListener(ActionsCompare);
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
                    //Debug.Log("posicion: "+ i +" ,nombre: "+ spriteList[_indice].name + " Enojo: " + _red );
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
                    //Debug.Log("posicion: "+ i +" ,nombre: "+ spriteList[_indice].name + " Neutral: " + _yellow);
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
                    //Debug.Log("posicion: "+ i +" ,nombre: "+ spriteList[_indice].name + " Alegre: " + _green);
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
            _j = 1;
            switch (btn)
            {
                case 0:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _angry)
                        _red2 += 1;
                    else
                        _red2 -= 1;
                    //Debug.Log("Eliminadas -> Enojo: " + _red2 );
                    break;
                case 1:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _angry)
                        _red2 += 1;
                    else
                        _red2 -= 1;
                    //Debug.Log("Eliminadas -> Enojo: " + _red2 );
                    break;
                case 2:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _angry)
                        _red2 += 1;
                    else
                        _red2 -= 1;
                    //Debug.Log("Eliminadas -> Enojo: " + _red2 );
                    break;
                case 3:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _angry)
                        _red2 += 1;
                    else
                        _red2 -= 1;
                    //Debug.Log("Eliminadas -> Enojo: " + _red2 );
                    break;
            }
        }
    }
    public void ActionsYellow(int btn)
	{
        if(color == "yellow")
        {
            _j = 2;
            switch (btn)
            {
                case 0:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _neutral)
                        _yellow2 += 1;
                    else
                        _yellow2 -= 1;
                    //Debug.Log("Eliminadas -> Neutral: " + _yellow2 );
                    break;
                case 1:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _neutral)
                        _yellow2 += 1;
                    else
                        _yellow2 -= 1;
                    //Debug.Log("Eliminadas -> Neutral: " + _yellow2 );
                    break;
                case 2:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _neutral)
                        _yellow2 += 1;
                    else
                        _yellow2 -= 1;
                    //Debug.Log("Eliminadas -> Neutral: " + _yellow2 );
                    break;
                case 3:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _neutral)
                        _yellow2 += 1;
                    else
                        _yellow2 -= 1;
                    //Debug.Log("Eliminadas -> Neutral: " + _yellow2 );
                    break;
            }
        }
	}
    public void ActionsGreen(int btn)
	{
        if(color == "green")
        {
            _j = 3;
            switch (btn)
            {
                case 0:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _happy)
                        _green2 += 1;
                    else
                        _green2 -= 1;
                    //Debug.Log("Eliminadas -> Alegre: " + _green2 );
                    break;
                case 1:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _happy)
                        _green2 += 1;
                    else
                        _green2 -= 1;
                    //Debug.Log("Eliminadas -> Alegre: " + _green2 );
                    break;
                case 2:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _happy)
                        _green2 += 1;
                    else
                        _green2 -= 1;
                    //Debug.Log("Eliminadas -> Alegre: " + _green2 );
                    break;
                case 3:
                    btnEmoji[btn].GetComponent<Image>().enabled = false;
                    if(btnEmoji[btn].GetComponent<Image>().sprite.name == _happy)
                        _green2 += 1;
                    else
                        _green2 -= 1;
                    //Debug.Log("Eliminadas -> Alegre: " + _green2 );
                    break;
            }
        }
	}
    public void ActionsCompare(){
        _k += 1;
        CompareEmojis();
    }
    public void CompareEmojis()
	{
        if(color == "red" && _red == 0 && _k == 1)
            _j = 1;
        else if(color == "yellow" && _yellow == 0 && _k == 2)
            _j = 2;
        else if(color == "green" && _green == 0 && _k >= 4)
            _j = 3;
		/* Comprobacion de caritas enojadas */
        if(color == "red" && _j == 1)
        {
            if (_red == _red2 && _iWin0 == 0)
            {
                _iWin0 += 1;
                //Debug.Log("Todos Coinciden!!!");
                isRoundDone = true;
                totalTimeByGame += dbRoundtime - (int)timeLeft;
                UpdateScore();
                CompleteRound(1);
                GetAndInitializeYellow();
            }
            else if(_red != _red2)
            {
                //Debug.Log("FALLO, Uno o mas no coinciden");
                isGameDone = true;
                LoseGame();
            }
        }
        else if(color == "yellow" && _j == 2)
        {
            /* Comprobacion de caritas neutrales */
            if (_yellow == _yellow2 && _iWin1 == 0)
            {
                _iWin1 += 1;
                //Debug.Log("Todos Coinciden!!! Neutrales");
                isRoundDone = true;
                totalTimeByGame += dbRoundtime - (int)timeLeft;
                UpdateScore();
                CompleteRound(2);
                GetAndInitializeGreen();
            }
            else if(_yellow != _yellow2)
            {
                //Debug.Log("FALLO, Uno o mas no coinciden neutrales");
                isGameDone = true;
                LoseGame();
            }
        }
        else if(color == "green" && _j == 3)
        {
            /* Comprobacion de caritas alegres */
            if (_green == _green2 && _iWin2 == 0)
            {
                _iWin2 += 1;
                //Debug.Log("Todos Coinciden!!! Alegres " + _k);
                isRoundDone = true;
                totalTimeByGame += dbRoundtime - (int)timeLeft;
                UpdateScore();
                CompleteRound(3);
                isGameDone = true;
                WinGame();
            }
            else if(_green != _green2 && _iLose == 0)
            {
                _iLose += 1;
                //Debug.Log("FALLO, Uno o mas no coinciden alegres " + _green + " " + _green2);
                isGameDone = true;
                LoseGame();
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
    void GetInitializeMusic()
    {
        audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>(_musicName);
        audioSource.clip = bgMusic;
        audioSource.Play(0);
    }
    void WinGame()
    {
        di.UpdateLevelTimesPlayed(_nivel);

        if (bestScore == score)
            di.UpdateBestScoreForLevel(_nivel, score);
        di.UpdateTotalizedScore(score);

        di.SaveSuccesTime(new LevelSuccessTime()
        {
            LevelID = _nivel,
            SuccessTime = dgb.CalculateAverageRound(totalTimeByGame, 9)
        });

        audioSource.Stop();
        gs = new GameStatus();
        gs.PlayerWinGame(audioSource, waitingTime = 3, _nivel);
    }
    void LoseGame()
    {
        di.UpdateLevelTimesPlayed(_nivel);
        di.ResetLevelSuccessTimeByLevel(_nivel);
        audioSource.Stop();
        gs = new GameStatus();
        gs.PlayerNeedToRepeatGame(audioSource, waitingTime, _nivel);
    }
    void CompleteRound(int count)
	{
        //Setando Texto
        var Nivel = GameObject.Find("Nivel").GetComponent<Text>();
        Nivel.text = count + "/3";
	}
    private void SettingTimeOfGame()
    {
        timeLeft = di.GetRoundTime(_nivel);
    }
    private void InitializeRecordAndScore()
    {
        di = new DependencyInjector();
        dgb = new DynamicGameBalance();
        pd = new PlayerData();
        ld = new LevelData();

        pd = di.GetAllPlayerData();
        ld = di.GetLevelData(_nivel);
        dbRoundtime = di.GetRoundTime(_nivel);
            
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

    private void GetGeneralVolume()
    {
        float generalVolume = 0.0f;
        generalVolume = GlobalVariables.GeneralVolume;
        audioSource.volume = generalVolume;
    }

}
