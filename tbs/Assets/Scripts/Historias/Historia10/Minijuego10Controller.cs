using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Global;
using Assets.Scripts.DataPersistence.Models;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Minijuego10Controller : MonoBehaviour
{
    public Button btnEmoji1,btnEmoji2,btnEmoji3,btnEmoji4,btnCompare;
	private Image imgEmo1,imgEmo2,imgEmo3,imgEmo4; //Imagen sobre boton
	public GameObject panelBotones,panelSecuencia,btnContinue;
	public GameObject[] emoji; 
    public GameObject msj_ok;
	private int _indice = 0, _j = 0, _k = 0, _p = 0, _q = 0;
    public Sprite[] spriteList;
	public Sprite defaultBoton;
    public int dbRoundTime = 0;
    public int totalTimeByGame = 0;
    private float _elapsedTime = 0;
	private float _hideTime = 5;

	/* ******************* agregados para persistencia */
    private int _count = 1; //numero de rondas
	public int bestScore = 0;
    public int score = 0;
    public float timeLeft = 10.00f;
    public int waitingTime = 3;
    public bool isGameDone = false;
    public bool isRoundDone = false;
	private bool _isPanelHide = false;
	public GameObject texto;
    public Text BestScore, Score;
	public Text timing;
	public Text Nivel;
	public GameStatus gameStatusModel;
    public AudioSource audioSource;
    public AudioClip bgMusic;
    public PlayerData playerDataModel;
    public LevelData levelDataModel;
    public DependencyInjector dependecyInjector;
    public DynamicGameBalance dynamicGameBalance;
	/************************************* */

    // Start is called before the first frame update
    void Start()
    {
		GetAndInitializeAllGameObjects();
		InitializeRecordAndScore();
		/* Funcion que crea secuencia de emojis de manera aleatoria */
		RandomSequence();
    }

    // Update is called once per frame
    void Update()
    {
        /* Evita | Permite la Ejecucion del codigo */
		if (!isGameDone)
		{
			//ocultar imagenes de referencia
			if (!_isPanelHide)
			{
				_hideTime -= Time.deltaTime; // Tiempo transcurrido
				if(_hideTime <= 0){
					//Al alcanzar hideTime, oculta la secuencia de emojis
					_isPanelHide = true;
					HideSequence();
				}
			} 
		
			if (!isRoundDone && _isPanelHide)
			{			
				if(!panelBotones.GetComponent<Animation>().IsPlaying("Panel2")){
					//Debug.Log("------------------TERMINO LA ANIMACION -------------------");
					timeLeft -= Time.deltaTime; //tiempo de ronda para jugar 
					timing.text = "Tiempo: " + timeLeft.ToString("0"); 
				}
			}

			if (timeLeft <= 0 && !isGameDone)
            {
				LoseGame();
            }
		}
    }
	
	private void LoseGame(){
		dependecyInjector.UpdateLevelTimesPlayed(10);
        dependecyInjector.ResetLevelSuccessTimeByLevel(10);
        audioSource.Stop();
		isGameDone = true;
        gameStatusModel = new GameStatus();
        gameStatusModel.PlayerNeedToRepeatGame(audioSource, waitingTime, 1);
	}
	/* RandomSequence: Crea la secuencia de emojis de forma random */
    public void RandomSequence()
    {
        /* Llena el vector de emojis que se van a mostrar como secuencia de emojis */
        for (int i = 0; i < 4; i++)
        {
            /* Obtiene indice al azar para llamar a la imagen guardada en spriteList[] */
            _indice = Random.Range(0, 5);
            emoji[i].GetComponent<Image>().sprite = spriteList[_indice];
            //Debug.Log("posicion: "+i+" ,nombre: "+spriteList[_indice].name);
        }
    }
    
	/* actions: proporciona accion de botones que tienen imagenes de emojis */
	public void Actions(int btn)
	{
		//Debug.Log("Nombre del Boton: "+btn);
		switch (btn)
		{
			case 1:
				SetEmoji(imgEmo1,_j);
				if (_j<4){ _j++; }else{ _j = 0; }
				break;
			case 2:
				SetEmoji(imgEmo2,_k);
				if (_k<4){ _k++; }else{ _k = 0; }
				break;
			case 3:
				SetEmoji(imgEmo3,_p);
				if (_p<4){ _p++; }else{ _p = 0; }
				break;
			case 4:
				SetEmoji(imgEmo4,_q);
				if (_q<4){ _q++; }else{ _q = 0; } 
				break;
		}

	}
    
	/* hideSequence: oculta la secuencia de imagenes y activa la animacion de botones para interactuar */
	public void HideSequence()
	{
		for (int i = 0; i < 4; i++)
		{
			//Oculta cada elemento de imagen de la secuencia
			emoji[i].GetComponent<Image>().enabled = false;
		}

		//Activa panel y animacion con botones
		panelBotones.SetActive(true);
		panelBotones.GetComponent<Animation>().Play("Panel2");
		
	}
    
	/* setEmoji: cambia la imagen de emoji en los botones donde interactua el usuario */
	public void SetEmoji(Image imgEmo, int index)
	{
		imgEmo.sprite = spriteList[index];
	}
	
	/* compareEmojis: compara la secuencia inicial con los valores seteados por el usuario */
	public void CompareEmojis()
	{
		/* Comprobacion de secuencia con imagen del panel */
		if (imgEmo1.sprite.name == emoji[0].GetComponent<Image>().sprite.name && 
		imgEmo2.sprite.name == emoji[1].GetComponent<Image>().sprite.name && 
		imgEmo3.sprite.name == emoji[2].GetComponent<Image>().sprite.name && 
		imgEmo4.sprite.name == emoji[3].GetComponent<Image>().sprite.name)
		{
            //Debug.Log("Todos Coinciden!!!");
            Ok();
		}else
		{
            //Debug.Log("FALLO, Uno o mas no coinciden");
            Fail();
		}
		
	}
	
	/* OK: Mensaje de respuesta correcta */
    public void Ok()
    {
        totalTimeByGame += dbRoundTime - (int)timeLeft;
        UpdateScore();
        SettingTimeOfGame();
        msj_ok.SetActive(true);
        btnContinue.SetActive(true);
		btnCompare.enabled = false;
        isRoundDone = true;
    }
    
	/* fail: Mensaje de Respuesta incorrecta */
    public void Fail()
    {
        score -= 800;
        LoseGame();
        isRoundDone = true;
    }
	
	public void Complete()
    {
        audioSource.Stop();
        isGameDone = true;

        dependecyInjector.UpdateLevelTimesPlayed(10);
        dependecyInjector.SaveSuccesTime(new LevelSuccessTime(){
            LevelID = 10,
            SuccessTime = dynamicGameBalance.CalculateAverageRound(totalTimeByGame,10)
        });

        if (bestScore == score)
            dependecyInjector.UpdateBestScoreForLevel(10, score);
        dependecyInjector.UpdateTotalizedScore(score);

        gameStatusModel = new GameStatus();
        gameStatusModel.PlayerWinGame(audioSource, waitingTime, 10);
    }
	
	/* iteracion:  */
	public void Iteration()
    {
        isRoundDone = false;

        //Setando Texto
        Nivel.text = _count + "/3";

        //Contador
        _count++;

        msj_ok.SetActive(false);
        btnContinue.SetActive(false);
		btnCompare.enabled = true;

		//Coloca imagen base en boton
		imgEmo1.sprite = defaultBoton;
		imgEmo2.sprite = defaultBoton;
		imgEmo3.sprite = defaultBoton;
		imgEmo4.sprite = defaultBoton;

		panelBotones.SetActive(false);
		
        //Generar una nueva secuencia
		if(_count <= 3){
			RandomSequence();
			isGameDone = false;
			_isPanelHide = false; //Haciendo visible panel de secuencia 
			_hideTime = 5; //reiniciando cuenta para ocultar secuencia
			for (int i = 0; i < 4; i++)
			{
				//Visualizar cada elemento de imagen de la secuencia
				emoji[i].GetComponent<Image>().enabled = true;
			}
		}else{
			isGameDone = true;
			//Debug.Log("----**********--- JUEGO FINALIZADO ----**********---");
			Complete();
		}
    }
	
	private void GetAndInitializeAllGameObjects()
    {
        dependecyInjector = new DependencyInjector();
        dynamicGameBalance = new DynamicGameBalance();
		audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>("Sounds/Minigame");
        audioSource.clip = bgMusic;
        audioSource.Play(0);

		btnEmoji1.onClick.AddListener(() => Actions(1));
		btnEmoji2.onClick.AddListener(() => Actions(2));
		btnEmoji3.onClick.AddListener(() => Actions(3));
		btnEmoji4.onClick.AddListener(() => Actions(4));
		btnCompare.onClick.AddListener(CompareEmojis);

		imgEmo1 = GameObject.Find("imgEmo1").GetComponent<Image>();
		imgEmo2 = GameObject.Find("imgEmo2").GetComponent<Image>();
		imgEmo3 = GameObject.Find("imgEmo3").GetComponent<Image>();
		imgEmo4 = GameObject.Find("imgEmo4").GetComponent<Image>();

		texto = new GameObject();
        texto = GameObject.Find("Timing");
        timing = texto.GetComponent<Text>();

        timing.text = "Tiempo";

		/*Para control de puntajes.*/
        var objBestScore = GameObject.Find("BestScore");
        var objScore = GameObject.Find("Score");
        BestScore = objBestScore.GetComponent<Text>();
        Score = objScore.GetComponent<Text>();

        timeLeft = dependecyInjector.GetRoundTime(10);

		panelBotones.SetActive(false);

	}
	
	private void InitializeRecordAndScore()
    {
        dependecyInjector = new DependencyInjector();
        dynamicGameBalance = new DynamicGameBalance();
        playerDataModel = new PlayerData();
        levelDataModel = new LevelData();

        playerDataModel = dependecyInjector.GetAllPlayerData();
        levelDataModel = dependecyInjector.GetLevelData(10);
        dbRoundTime = dependecyInjector.GetRoundTime(10);

        score = 0;
        bestScore = levelDataModel.BestScore;

        BestScore.text = "Record: " + levelDataModel.BestScore;
        Score.text = "Puntaje: " + score;
        SettingTimeOfGame();

    }
	
	private void SettingTimeOfGame()
    {
        timeLeft = dependecyInjector.GetRoundTime(10);
    }
	
	private void UpdateScore()
    {
        //Debug.Log("timeLeft: -----------------------  "+timeLeft+"  ----------------------------");
        double res = 100 * (timeLeft * levelDataModel.PointMultiplier);
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
