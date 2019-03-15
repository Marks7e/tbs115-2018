using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minijuego10Controller : MonoBehaviour
{
    public Button btnEmoji1,btnEmoji2,btnEmoji3,btnEmoji4,btnCompare;
	private Image imgEmo1,imgEmo2,imgEmo3,imgEmo4; //Imagen sobre boton
	public GameObject panelBotones,panelSecuencia,btnReset, btnContinue;
	public GameObject[] emoji; 
    public GameObject msj_ok, msj_fail;
	private int indice = 0, j = 0, k = 0, p = 0, q = 0;
    public Sprite[] spriteList;
	public Sprite defaultBoton;
    private float elapsedTime = 0;
	private float hideTime = 5;
	//private bool updateOn = true;

	/* ******************* agregados para persistencia */
    private int count = 1; //numero de rondas
	public int bestScore = 0;
    public int score = 0;
    public float timeLeft = 10.00f;
    public int waitingTime = 3;
    public bool isGameDone = false;
    public bool isRoundDone = false;
	private bool isPanelHide = false;
	public GameObject texto;
    public Text BestScore, Score;
	public Text timing;
	public Text Nivel;
	public GameStatus gs;
    public AudioSource audioSource;
    public AudioClip bgMusic;
   
    public PlayerData pd;
    public LevelData ld;
    public DependencyInjector di;
	/************************************* */

    // Start is called before the first frame update
    void Start()
    {
		GetAndInitializeAllGameObjects();
		InitializeRecordAndScore();
		/* Funcion que crea secuencia de emojis de manera aleatoria */
		randomSequence();
    }

    // Update is called once per frame
    void Update()
    {
        /* Evita | Permite la Ejecucion del codigo */
		if (!isGameDone)
		{
			//ocultar imagenes de referencia
			if (!isPanelHide)
			{
				hideTime -= Time.deltaTime; // Tiempo transcurrido
				if(hideTime <= 0){
					//Al alcanzar hideTime, oculta la secuencia de emojis
					isPanelHide = true;
					hideSequence();
				}
			} 
		
			if (!isRoundDone && isPanelHide)
			{			
				if(!panelBotones.GetComponent<Animation>().IsPlaying("Panel2")){
					//Debug.Log("------------------TERMINO LA ANIMACION -------------------");
					timeLeft -= Time.deltaTime; //tiempo de ronda para jugar 
					timing.text = "Tiempo: " + timeLeft.ToString("0"); 
				}
			}

			if (timeLeft <= 0 && !isGameDone)
            {
               	//UnableGameControls();
                audioSource.Stop();
				isGameDone = true;
                gs = new GameStatus();
                gs.PlayerNeedToRepeatGame(audioSource, waitingTime, 1);
            }
			
		}

		

    }
	
	/* randomSequence: Crea la secuencia de emojis de forma random */
    public void randomSequence()
    {
        /* Llena el vector de emojis que se van a mostrar como secuencia de emojis */
        for (int i = 0; i < 4; i++)
        {
            /* Obtiene indice al azar para llamar a la imagen guardada en spriteList[] */
            indice = Random.Range(0, 5);
            emoji[i].GetComponent<Image>().sprite = spriteList[indice];
			Debug.Log("posicion: "+i+" ,nombre: "+spriteList[indice].name);
        }
    }
    
	/* actions: proporciona accion de botones que tienen imagenes de emojis */
	public void actions(int btn)
	{
		//Debug.Log("Nombre del Boton: "+btn);
		switch (btn)
		{
			case 1:
				setEmoji(imgEmo1,j);
				if (j<4){ j++; }else{ j = 0; }
				break;
			case 2:
				setEmoji(imgEmo2,k);
				if (k<4){ k++; }else{ k = 0; }
				break;
			case 3:
				setEmoji(imgEmo3,p);
				if (p<4){ p++; }else{ p = 0; }
				break;
			case 4:
				setEmoji(imgEmo4,q);
				if (q<4){ q++; }else{ q = 0; } 
				break;
		}

	}
    
	/* hideSequence: oculta la secuencia de imagenes y activa la animacion de botones para interactuar */
	public void hideSequence()
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
	public void setEmoji(Image imgEmo, int index)
	{
		imgEmo.sprite = spriteList[index];
	}
	
	/* compareEmojis: compara la secuencia inicial con los valores seteados por el usuario */
	public void compareEmojis()
	{
		/* Comprobacion de secuencia con imagen del panel */
		if (imgEmo1.sprite.name == emoji[0].GetComponent<Image>().sprite.name && 
		imgEmo2.sprite.name == emoji[1].GetComponent<Image>().sprite.name && 
		imgEmo3.sprite.name == emoji[2].GetComponent<Image>().sprite.name && 
		imgEmo4.sprite.name == emoji[3].GetComponent<Image>().sprite.name)
		{
			Debug.Log("Todos Coinciden!!!");
			Ok();
		}else
		{
			Debug.Log("FALLO, Uno o mas no coinciden");
			fail();
		}
		
	}
	
	/* OK: Mensaje de respuesta correcta */
    public void Ok()
    {
        UpdateScore();
        SettingTimeOfGame();
        msj_ok.SetActive(true);
        btnContinue.SetActive(true);
		btnCompare.enabled = false;
        isRoundDone = true;
    }
    
	/* fail: Mensaje de Respuesta incorrecta */
    public void fail()
    {
        score -= 800;
        msj_fail.SetActive(true);
        btnReset.SetActive(true);
        isRoundDone = true;
    }
	
	public void complete()
    {
        audioSource.Stop();
        isGameDone = true;
        

        if (bestScore == score)
            di.UpdateBestScoreForLevel(10, score);
        di.UpdateTotalizedScore(score);

        gs = new GameStatus();
        gs.PlayerWinGame(audioSource, waitingTime, 10);
    }
	
	/* iteracion:  */
	public void iteracion()
    {
        isRoundDone = false;

        //Setando Texto
        Nivel.text = count + "/3";

        //Contador
        count++;

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
		if(count <= 3){
			randomSequence();
			isGameDone = false;
			isPanelHide = false; //Haciendo visible panel de secuencia 
			hideTime = 5; //reiniciando cuenta para ocultar secuencia
			for (int i = 0; i < 4; i++)
			{
				//Visualizar cada elemento de imagen de la secuencia
				emoji[i].GetComponent<Image>().enabled = true;
			}
		}else{
			isGameDone = true;
			Debug.Log("----**********--- JUEGO FINALIZADO ----**********---");
			complete();
		}
    }
	
	private void GetAndInitializeAllGameObjects()
    {
		audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>("Sounds/Minigame");
        audioSource.PlayOneShot(bgMusic);

		btnEmoji1.onClick.AddListener(() => actions(1));
		btnEmoji2.onClick.AddListener(() => actions(2));
		btnEmoji3.onClick.AddListener(() => actions(3));
		btnEmoji4.onClick.AddListener(() => actions(4));
		btnCompare.onClick.AddListener(compareEmojis);

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

		panelBotones.SetActive(false);

	}
	
	private void InitializeRecordAndScore()
    {
        di = new DependencyInjector();
        pd = new PlayerData();
        ld = new LevelData();

        pd = di.GetAllPlayerData();
        ld = di.GetLevelData(10);

        score = 0;
        bestScore = ld.BestScore;

        BestScore.text = "Record: " + ld.BestScore;
        Score.text = "Puntaje: " + score;
        SettingTimeOfGame();

    }
	
	private void SettingTimeOfGame()
    {
        timeLeft = ld.RoundTime;
    }
	
	private void UpdateScore()
    {
		Debug.Log("timeLeft: -----------------------  "+timeLeft+"  ----------------------------");
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
