using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Minijuego5 : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;

    public Button[] emojiArrow;
    public Sprite arrow, uIMask;
    public int _iArrow = 4, _iSlot = 4, _iTurn = 0, 
                _iArrowPrev = 0, _iSlotPrev = 4, _iRound = 1, 
                _roundCount = 0, _iLose = 0;
    public float currentTime = 0;
	float maxTime = 0.1f;
    public Text[] roundText;

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
    public int _nivel = 5;

    public DependencyInjector di;

    //Ganar/Perder
    public GameStatus gs;
    public AudioSource audioSource;
    public AudioClip bgMusic;
    private string _musicName = "Sounds/Minigame";

    // Start is called before the first frame update
    void Start()
    {
        GetAndInitializeAllGameObjects();
        GetInitializeMusic();
        InitializeRecordAndScore();
        HasChanged();
    }
    // Update is called once per frame
    void Update()
    {
        VerifiedSlot();
        HasChanged();
        CheckRound();
        CountControllerRound();

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
    private void NivelText(){
        var Nivel = GameObject.Find("Nivel").GetComponent<Text>();
        Nivel.text = count + "/3";
        totalTimeByGame += dbRoundtime - (int)timeLeft;
        UpdateScore();
    }
    private void CheckRound(){
        if(_iTurn == 6 && _iRound == 1)
        {
            CountControllerRound();
            _iRound += 1;
            _roundCount = 0;
            isRoundDone = false;
            count++;
            NivelText();
            _iTurn = 1;
            SettingTimeOfGame();
        }
        else if(_iTurn == 7 && _iRound == 2)
        {
            CountControllerRound();
            _iRound += 1;
            _roundCount = 0;
            isRoundDone = false;
            count++;
            NivelText();
            _iTurn = 1;
            SettingTimeOfGame();
        }else if(_iTurn == 8 && _iRound == 3)
        {
            isRoundDone = true;
            count = 3;
            NivelText();
            _iTurn += 1;
            WinGame();
        }
    }
    private void RandomPositionArrow()
    {
        _iArrowPrev = _iArrow;
        _iArrow = Random.Range(0, 4);

        if(_iArrowPrev != 4 && _iArrowPrev != 4){
            emojiArrow[_iArrowPrev].GetComponent<Image>().sprite = uIMask;
        }

        if(_iArrowPrev != _iArrow){
            emojiArrow[_iArrow].GetComponent<Image>().sprite = arrow;
        }else if(_iArrow == 3){
            _iArrow -= 1;
            emojiArrow[_iArrow].GetComponent<Image>().sprite = arrow;
        }else{
            _iArrow +=1;
            emojiArrow[_iArrow].GetComponent<Image>().sprite = arrow;
        }
    }
    public void HasChanged()
    {
        if(_iTurn == 0 && _iRound == 1){
            isRoundDone = false;
            _iArrow = 4;
            _iSlot = 4;
            _iTurn += 1;
            RandomPositionArrow();
        }
        else if(_iTurn != 0){
            foreach (Transform slotTransform in slots)
            {
                GameObject item = slotTransform.GetComponent<SlotContent>().item;
                if (item)
                {
                    _iSlot = slotTransform.GetComponent<Slot>().id;
                }
            }
        }
    }
    private void VerifiedSlot()
	{
		currentTime += Time.deltaTime;
		
		if(currentTime >= maxTime){
			currentTime = 0;
			if(_iArrow == _iSlot && _iTurn != 0)
			{
                if(_iTurn == 1){
                    _roundCount += 1;
                    _iSlotPrev = _iSlot;
                    RandomPositionArrow();
                    _iTurn += 1;
                }else if(_iTurn == 2)
                {
                    _roundCount += 1;
                    _iSlotPrev = _iSlot;
                    RandomPositionArrow();
                    _iTurn += 1;
                }else if(_iTurn == 3)
                {
                    _roundCount += 1;
                    _iSlotPrev = _iSlot;
                    RandomPositionArrow();
                    _iTurn += 1;
                }else if(_iTurn == 4)
                {
                    _roundCount += 1;
                    _iSlotPrev = _iSlot;
                    RandomPositionArrow();
                    _iTurn += 1;
                }else if(_iTurn == 5)
                {
                    _roundCount += 1;
                    _iSlotPrev = _iSlot;
                    RandomPositionArrow();
                    _iTurn += 1;
                }else if(_iTurn == 6)
                {
                    _roundCount += 1;
                    _iSlotPrev = _iSlot;
                    RandomPositionArrow();
                    _iTurn += 1;
                }else if(_iTurn == 7)
                {
                    _roundCount += 1;
                    _iTurn += 1;
                }
            }else if(_iSlot != _iSlotPrev && _iLose == 0) 
            {
                LoseGame();
                isRoundDone = true;
                _iLose += 1;
            }
	    }
    }
    private void CountControllerRound()
    {
        if(_iRound == 1){
            roundText[0].text = _roundCount.ToString("0") + "/5";
        }
        if(_iRound == 2){
            roundText[1].text = _roundCount.ToString("0") + "/6";
        }
        if(_iRound == 3){
            roundText[2].text = _roundCount.ToString("0") + "/7";
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
            SuccessTime = dgb.CalculateAverageRound(totalTimeByGame, 5)
        });

        audioSource.Stop();
        gs = new GameStatus();
        gs.PlayerWinGame(audioSource, waitingTime = 3, 1);
    }
    void LoseGame()
    {
        di.UpdateLevelTimesPlayed(_nivel);
        di.ResetLevelSuccessTimeByLevel(_nivel);
        audioSource.Stop();
        gs = new GameStatus();
        gs.PlayerNeedToRepeatGame(audioSource, waitingTime = 3, 1);
    }
    private void GetAndInitializeAllGameObjects()
    {
        di = new DependencyInjector();
        var objBestScore = GameObject.Find("BestScore");
        var objScore = GameObject.Find("Score");
        BestScore = objBestScore.GetComponent<Text>();
        Score = objScore.GetComponent<Text>();
        texto = GameObject.Find("Timing");
        timing = texto.GetComponent<Text>();
        dgb = new DynamicGameBalance();      
    }
    private void SettingTimeOfGame()
    {
        timeLeft = di.GetRoundTime(_nivel);
    }
    private void InitializeRecordAndScore()
    {
        pd = new PlayerData();
        ld = new LevelData();

        pd = di.GetAllPlayerData();
        ld = di.GetLevelData(_nivel);
        //dbRoundtime = di.GetRoundTime(_nivel);

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


