using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bar : MonoBehaviour {
    
	public static int [] slots;
	public float currentTime = 0;
	float maxTime = 0.1f;
	private int k=0, l=0, m=0, n=0, p=0, q=1; 

	//Texto a mostrar al usuario
    public Text Nivel;
    public Text timing;
    public Text BestScore;
    public Text Score;
    public GameObject texto;
    public int bestScore = 0;
    public int score = 0;
    public int count = 0;
    public float timeLeft = 5.00f;
    public int waitingTime = 3;
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
	private int index = 0;

	// Use this for initialization
	void Start () {
		GetAndInitializeAllGameObjects();
		GetInitializeMusic();
		InitializeRecordAndScore();
		CompleteRound();
		slots = new int[5];
	}
	
	// Update is called once per frame
	void Update () {

		VerifiedSlot();

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

	void GetInitializeMusic()
    {
        audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>(musicName);
        audioSource.clip = bgMusic;
        audioSource.Play(0);
    }

	void WinGame()
    {
		if (bestScore == score)
            di.UpdateBestScoreForLevel(4, score);
        di.UpdateTotalizedScore(score);

		if(index == 1)
		{
            di.UpdateLevelTimesPlayed(3);
            audioSource.Stop();
        	gs = new GameStatus();
        	gs.PlayerWinGame(audioSource, waitingTime, 4);
			index++;
		}
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
        ld = di.GetLevelData(4);

        score = 0;
        bestScore = ld.BestScore;

        BestScore.text = "Record: " + ld.BestScore;
        Score.text = "Puntaje: " + score;
        SettingTimeOfGame();

    }

	private void VerifiedSlot()
	{
		currentTime += Time.deltaTime;
		
		if(currentTime >= maxTime){
			currentTime = 0;
			if(slots[0] == 1)
			{
				FullSlot(0, k++);
				if(slots[1] == 1)
				{
					FullSlot(1, l++);
					if(slots[2] == 1)
					{
						FullSlot(2, m++);
						if(slots[3] == 1)
						{
							FullSlot(3, n++);
							if(slots[4] == 1)
							{
								FullSlot(4, p++);
								index++;
								WinGame();
							}
							else
							{
								isRoundDone = false;
							}
						}
						else
						{
							isRoundDone = false;
						}
					}
					else
					{
						isRoundDone = false;
					}
				}
				else
				{
					isRoundDone = false;
				}
			}
			else
			{
				isRoundDone = false;
			}
		}
	}

	private void UpdateScore()
    {
		//Contador
        count++;
		CompleteRound();

		isRoundDone = true;

        double res = 100 * (timeLeft * ld.PointMultiplier);
        score += (int)res;
        Score.text = "Puntaje: " + score;

        if (bestScore < score)
        {
            bestScore = score;
            BestScore.text = "Record: " + score;
        }
    }

	private void GetAndInitializeAllGameObjects()
    {
        var objBestScore = GameObject.Find("BestScore");
        var objScore = GameObject.Find("Score");
        BestScore = objBestScore.GetComponent<Text>();
        Score = objScore.GetComponent<Text>();
        texto = GameObject.Find("Timing");
        timing = texto.GetComponent<Text>();
    }

	private void FullSlot(int i, int j)
    {
		switch(i)
		{
			case 0:
				if(q==j)
				{
					CompleteRound();
					UpdateScore();
					timeLeft = 5.00f;
				}
			break;
			case 1:
				if(q==j)
				{
					CompleteRound();
					UpdateScore();
					timeLeft = 5.00f;
				}
			break;
			case 2:
				if(q==j)
				{
					CompleteRound();
					UpdateScore();
					timeLeft = 5.00f;
				}
			break;
			case 3:
				if(q==j)
				{
					CompleteRound();
					UpdateScore();
					timeLeft = 5.00f;
				}
			break;
			case 4:
				if(q==j)
				{
					CompleteRound();
					UpdateScore();
					timeLeft = 5.00f;
				}
			break;
		}
	}

	void LoseGame()
    {
        audioSource.Stop();
        gs = new GameStatus();
        gs.PlayerNeedToRepeatGame(audioSource, waitingTime, 4);
    }

	void CompleteRound()
	{
        //Setando Texto
        var Nivel = GameObject.Find("Nivel").GetComponent<Text>();
        Nivel.text = count + "/5";
	}
}
