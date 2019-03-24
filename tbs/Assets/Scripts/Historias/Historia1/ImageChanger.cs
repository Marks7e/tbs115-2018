using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using Assets.Scripts.Utils;
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
    public DynamicGameBalance dgb = null;
    public float timeLeft = 0f;

    //Texto a mostrar al usuario
    public Text Nivel;
    public Text timing;
    public Text BestScore;
    public Text Score;
    public GameObject texto;
    public int bestScore = 0;
    public int score = 0;
    public int count = 0;
    public int waitingTime = 3;
    public int dbRoundtime = 0;
    public int totalTimeByGame = 0;
    public bool isGameDone = false;
    public bool isRoundDone = false;
    public PlayerData pd;
    public LevelData ld;

    public DependencyInjector di;

    //Ganar/Perder
    public GameStatus gs;
    public AudioSource audioSource;
    public AudioClip bgMusic;
    private string musicName = "Sounds/Minigame";

    // Start is called before the first frame update
    void Start()
    {
        GetAndInitializeAllGameObjects();
        GetInitializeMusic();
        ButtonInit();
        SleepersInactive();
        RandomPosition();
        ImageChangerBtn();
        InitializeRecordAndScore();
        btnSend.onClick.AddListener(ImageSendBtn);
    }
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
                audioSource.Stop();
                isGameDone = true;
                LoseGame();
            }
        }
    }
    public void NextSprite()
    {
        ButtonActive();
        imgBtn.sprite = imageList[i];
        imgName = imageList[i].name;

        if (i < 4)
        {
            i++;
        }
        else
        {
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

        switch (index)
        {
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
        if (imgNameArg == iName)
        {

            switch (j)
            {
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
        else
        {
            LoseGame();
        }
    }
    public void ImageSendBtn()
    {
        isRoundDone = true;

        if (j < 5)
        {
            ImageSend(imgName);
        }
    }
    void SleepersInactive()
    {
        for (int k = 0; k < sleepers.Length; k++)
        {
            sleepers[k].SetActive(false);
        }
    }
    void SleepersActive()
    {
        //Contador
        count++;
        //Setando Texto
        var Nivel = GameObject.Find("Nivel").GetComponent<Text>();
        Nivel.text = count + "/5";

        sleepers[j].SetActive(true);
        animator = sleepers[j].GetComponent<Animator>();
        animator.SetTrigger("SleepActivate");
        totalTimeByGame += dbRoundtime - (int)timeLeft;
        UpdateScore();
    }
    void ButtonInit()
    {
        timeLeft = di.GetLevelData(1).RoundTime;
        btnSend.enabled = false;
        btnSend.GetComponent<Image>().enabled = false;
        btn.GetComponent<Image>().enabled = false;
        ImageChangerBtn();
        isRoundDone = false;
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
        di.UpdateLevelTimesPlayed(1);

        if (bestScore == score)
            di.UpdateBestScoreForLevel(1, score);
        di.UpdateTotalizedScore(score);

        di.SaveSuccesTime(new LevelSuccessTime()
        {
            LevelID = 1,
            SuccessTime = dgb.CalculateAverageRound(totalTimeByGame, 1)
        });

        audioSource.Stop();
        gs = new GameStatus();
        gs.PlayerWinGame(audioSource, waitingTime = 3, 1);
        imageSelector.SetActive(false);
    }
    void LoseGame()
    {
        di.UpdateLevelTimesPlayed(1);
        di.ResetLevelSuccessTimeByLevel(1);
        audioSource.Stop();
        gs = new GameStatus();
        gs.PlayerNeedToRepeatGame(audioSource, waitingTime, 1);
        imageSelector.SetActive(false);
    }

    private void GetAndInitializeAllGameObjects()
    {
        /*Para control de puntajes.*/
        //Nivel = GetComponent("Nivel").GetComponent<Text>();
        di = new DependencyInjector();
        var objBestScore = GameObject.Find("BestScore");
        var objScore = GameObject.Find("Score");
        BestScore = objBestScore.GetComponent<Text>();
        Score = objScore.GetComponent<Text>();
        texto = GameObject.Find("Timing");
        timing = texto.GetComponent<Text>();
        dgb = new DynamicGameBalance();
        timeLeft = di.GetRoundTime(1);
    }
    private void SettingTimeOfGame()
    {
        timeLeft = ld.RoundTime;
    }
    private void InitializeRecordAndScore()
    {
        pd = new PlayerData();
        ld = new LevelData();

        pd = di.GetAllPlayerData();
        ld = di.GetLevelData(1);
        dbRoundtime = di.GetRoundTime(1);

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
}
