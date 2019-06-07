using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Minijuego4 : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] Transform toys; //Personajes
   
    public GameObject[] arrayPrefab;
    private int _i = 0, _dec = 0, _inc = 0, _iSlot = 0,
                _k = 0, _l = 0, _m = 0, _n  = 0, _p = 0, 
                _q = 1, _index = 0, _iLose = 0;
    private int _kS = 0, _lS = 0, _mS = 0, _nS  = 0, _pS = 0;
    private int[] _slots; 
    public float currentTime = 0;
	float maxTime = 0.1f;

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
    public int _nivel = 4;

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
        RandomPosition();
        _slots = new int[5];
    }

    // Update is called once per frame
    void Update()
    {
        HasChanged();
        VerifiedSlot();
        LookEmptySlot();

        if (!isGameDone)
        {
            if (!isRoundDone)
            {
                timeLeft -= Time.deltaTime;
                timing.text = "Tiempo: " + timeLeft.ToString("0");
            }
            if (timeLeft <= 0 && !isGameDone)
            {
                if(_iLose == 0){
                    LoseGame();
                    _iLose ++;
                }
            }
        }
    }
    public void HasChanged()
    {
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<SlotContent>().item;
            string nameToy = "";
            if (item)
            {
                _iSlot = slotTransform.GetComponent<Slot>().id;
                nameToy = item.name;
                CompareSlotSmugie(nameToy, _iSlot);
            }
        }
    }
    private void RandomPosition()
    {
        int index = Random.Range(0, arrayPrefab.Length);
        GameObject toy = null;
        switch (index)
        {
            case 0:
                Debug.Log("indice 0,sumar" + index);
                toy = arrayPrefab[index];
                arrayPrefab[index] = arrayPrefab[index + 1];
                arrayPrefab[index + 1] = toy;
                break;
            case 2:
                Debug.Log("Indice 2, restar" + index);
                toy = arrayPrefab[index];
                arrayPrefab[index] = arrayPrefab[index - 1];
                arrayPrefab[index - 1] = toy;
                break;
            default:
                Debug.Log("Indice 1, sumar o restar" + index);
                toy = arrayPrefab[index];
                arrayPrefab[index] = arrayPrefab[index - 1];
                arrayPrefab[index - 1] = toy;
                break;
        }
        SetToysSlots();
    }
    private void SetToysSlots(){
        foreach (Transform spriteTransform in toys)
        {
            int _j = GetIndex();
            GameObject objectHijo = Instantiate(arrayPrefab[_j]) as GameObject;
            objectHijo.name = arrayPrefab[_j].name;
            objectHijo.transform.SetParent(spriteTransform.transform);
            objectHijo.transform.position = spriteTransform.transform.position;
            if(objectHijo.name == "smugie_m" || objectHijo.name == "smugie_f")
                objectHijo.transform.localScale = spriteTransform.transform.localScale;
            else
                objectHijo.transform.localScale = new Vector3(0.7f, 0.7f, 1);
            Debug.Log("Indice: " + _j);
        }
    }
    private int GetIndex(){
        if(_i == 0 && _inc == 1){
            _inc = 0;
            _i ++;
        }
        else if(_i >= 0 && _i < arrayPrefab.Length && _inc == 0 && _dec == 0){
            if(_i == (arrayPrefab.Length - 1)){
                _i --;
                _inc = 1;
                _dec = 1;
            }else
                _i ++;
        }
        else if(_i > 0 && _i < arrayPrefab.Length && _dec == 1 && _inc == 1){
            _i --;
            if(_i == 0){
                _dec = 0;
            }
        }
        else
            _i = 0;
        return _i;
    }
    private void CompareSlotSmugie(string nameToy, int _iSlot){
        if((nameToy == "smugie_m" || nameToy == "smugie_f") 
            && _iSlot == 0){
                _slots[0] = 1;
                _kS = 1;
        }
        else if((nameToy == "smugie_m" || nameToy == "smugie_f") 
            && _iSlot == 1){
                _slots[1] = 1;
                _lS = 1;
        }
        else if((nameToy == "smugie_m" || nameToy == "smugie_f") 
            && _iSlot == 2){
                _slots[2] = 1;
                _mS = 1;
        }
        else if((nameToy == "smugie_m" || nameToy == "smugie_f") 
            && _iSlot == 3){
                _slots[3] = 1;
                _nS = 1;
        }
        else if((nameToy == "smugie_m" || nameToy == "smugie_f") 
            && _iSlot == 4){
                _slots[4] = 1;
                _pS = 1;
        }
        else if(nameToy == "conejo_m" || nameToy == "conejo_f")
        {
            if(_iLose == 0){
                LoseGame();
                _iLose ++;
            }
        }
    }
    private void VerifiedSlot()
	{
		currentTime += Time.deltaTime;
		
		if(currentTime >= maxTime){
			currentTime = 0;
			if(_slots[0] == 1)
			{
				FullSlot(0, _k++);
				if(_slots[1] == 1)
				{
					FullSlot(1, _l++);
					if(_slots[2] == 1)
					{
						FullSlot(2, _m++);
						if(_slots[3] == 1)
						{
							FullSlot(3, _n++);
							if(_slots[4] == 1)
							{
								FullSlot(4, _p++);
								_index++;
							}
							else isRoundDone = false;
						}
						else isRoundDone = false;
					}
					else isRoundDone = false;
				}
				else isRoundDone = false;
			}
			else isRoundDone = false;
		}
	}
    private void FullSlot(int i, int j)
    {
		switch(i)
		{
			case 0:
				if(_q==j)
				{
					ReInitRound();
				}
			break;
			case 1:
				if(_q==j)
				{
					ReInitRound();
				}
			break;
			case 2:
				if(_q==j)
				{
                    ReInitRound();
				}
			break;
			case 3:
				if(_q==j)
				{
					ReInitRound();
				}
			break;
			case 4:
				if(_q==j)
				{
					NivelText();
                    WinGame();
				}
			break;
		}
	}
    private void LookEmptySlot(){
        if(_slots[0] == 0){
            for(int _id = 1; _id < _slots.Length; _id ++){
                if(_slots[_id] == 1){
                    if(_iLose == 0){
                        _iLose ++;
                        LoseGame();
                    }
                }
            }
        }
        else if(_slots[1] == 0){
            for(int _id = 2; _id < _slots.Length; _id ++){
                if(_slots[_id] == 1){
                    if(_iLose == 0){
                        _iLose ++;
                        LoseGame();
                    }
                }
            }
        }
        else if(_slots[2] == 0){
            for(int _id = 3; _id < _slots.Length; _id ++){
                if(_slots[_id] == 1){
                    if(_iLose == 0){
                        _iLose ++;
                        LoseGame();
                    }
                }
            }
        }else if(_slots[3] == 0){
            for(int _id = 4; _id < _slots.Length; _id ++){
                if(_slots[_id] == 1){
                    if(_iLose == 0){
                        _iLose ++;
                        LoseGame();
                    }
                }
            }
        }
    }
    private void ReInitRound(){
        NivelText();
        SettingTimeOfGame();
    }
    private void NivelText(){
        count ++;
        var Nivel = GameObject.Find("Nivel").GetComponent<Text>();
        Nivel.text = count + "/5";
        totalTimeByGame += dbRoundtime - (int)timeLeft;
        UpdateScore();
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
        isGameDone = true;
		if (bestScore == score)
            di.UpdateBestScoreForLevel(4, score);
        di.UpdateTotalizedScore(score);

		if(_index == 1)
		{
            di.SaveSuccesTime(new LevelSuccessTime()
            {
                LevelID = _nivel,
                SuccessTime = dgb.CalculateAverageRound(totalTimeByGame, 5)
            });

            di.UpdateLevelTimesPlayed(_nivel);
            audioSource.Stop();
        	gs = new GameStatus();
        	gs.PlayerWinGame(audioSource, waitingTime, _nivel);
			_index++;
		}
	}
    void LoseGame()
    {
        isGameDone = true;
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
