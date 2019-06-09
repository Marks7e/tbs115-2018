using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Minijuego11 : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] Transform sprites;
    [SerializeField] Text elementText;

    public GameObject panelSprites;
    public GameObject panelWin;
    public GameObject btnContinue;

    public GameObject[] arrayPrefab = new GameObject[8];
   
    private string _referenciaOpcion;

    public Button btnCompare;
   
    public AudioSource audioSource;
    public AudioClip bgMusic;
    public AudioSource audioPetition;
    public AudioClip[] audioClipArray;

    public GameObject msj_ok;
    public GameObject texto;
    public Text BestScore, Score;

    public GameStatus gs;
  
    public PlayerData pd;
    public LevelData ld;
    public int bestScore = 0;
    public int score = 0;
    public float timeLeft = 0f;
    public int waitingTime = 3;
    public int totalTimeByGame = 0;
    public int dbRoundtime = 0;
    public bool isGameDone = false;
    public bool isRoundDone = false;

    //Dependencias
    public DependencyInjector di;
    public DynamicGameBalance dgb;

    //Texto a mostrar al usuario
    public Text Nivel;
    public Text timing;

    private int _nchar;
    private int _option;
    private int _count = 1;
    private int _i = 0;

    private void Awake()
    {
        audioPetition = GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    void Start()
    {
        GetAndInitializeAllGameObjects();

        InitializeRecordAndScore();

        HasChanged();

        btnCompare.onClick.AddListener(() => Validate());
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

        Nivel.text = _count + "/3";

        if (elementText.text.Length > 10)
        {
           DisablePanelSprites(); //Funcion que desactiva panel de sprites y activa boton
        }
    }

    private void LoseGame(){
        di.UpdateLevelTimesPlayed(11);
        audioSource.Stop();
        isGameDone = true;
        di.ResetLevelSuccessTimeByLevel(11);
        gs = new GameStatus();
        gs.PlayerNeedToRepeatGame(audioSource, waitingTime, 11);
    }
    // Deshabilita panel con sprite con partes de emoji y habilita boton de comparacion
    public void DisablePanelSprites()
    {
        panelSprites.gameObject.SetActive(false);
        btnCompare.gameObject.SetActive(true);
    }

    //Habilita panel con sprite con partes de emoji y deshabilita boton de comparacion
    public void EnablePanelSprite()
    {
        btnCompare.gameObject.SetActive(false);  //Desactiva boton
        panelSprites.gameObject.SetActive(true); //Activa panel
    }

    //Restablece el Escenario luego de cada ronda
    public void ResetStage()
    {
        elementText.text = " ";

        _referenciaOpcion = " ";

        EnablePanelSprite();
        
        //Borra los sprite cargado en rostro de emoji
        foreach (Transform slotTransform in slots)
        {
            GameObject itemSprite = slotTransform.GetComponent<SlotContent>().item;
            Destroy(itemSprite);
        }

        //Borra todos los sprite arrastrables
        foreach (Transform spriteTransform in sprites)
        {
            GameObject objSprite = spriteTransform.GetComponent<SlotContent>().item;
            Destroy(objSprite);
        }

        RestockPanelSprite();
    }

    //Rellena los stocks con partes de emoji
    public void RestockPanelSprite()
    {
        //Vuelve a llenar todo el panel de sprites arrastrables
        foreach (Transform spriteTransform in sprites)
        {
            GameObject objectHijo = Instantiate(arrayPrefab[_i]) as GameObject;
            objectHijo.name = arrayPrefab[_i].name;
            objectHijo.transform.SetParent(spriteTransform.transform);
            objectHijo.transform.position = spriteTransform.transform.position;
            _i++;
        }

        _i = 0;
    }

    //Valida los sprites arrastrados hacia el rostro base de emoji
    public void Validate()
    {
        Debug.Log("intento: " + _count);
        if (elementText.text == _referenciaOpcion)
        {
            if (_count < 3)
            {
                //Debug.Log("Son iguales, ACERTASTE");
                isRoundDone = true;
                panelSprites.SetActive(false);
                panelWin.SetActive(true);
                _count++;
            }
            else
            {
                btnCompare.gameObject.SetActive(false);
                GameObject.Find("Base_Rostro").SetActive(false); //disable Panel: Base_Rostro
                                                                    //Borra los sprite cargado en rostro de emoji
                foreach (Transform slotTransform in slots)
                {
                    GameObject itemSprite = slotTransform.GetComponent<SlotContent>().item;
                    Destroy(itemSprite);
                }

                Debug.Log("--*-*-*-*-*-*-*-*-*-*- Juego Terminado -*-*-*-*-*-*-**-*-*-*-*-*--*");
                CompleteGame();
            }
              
        }
        else
        {
            //Debug.Log("Son diferentes, FALLASTE");
            isRoundDone = true;
            panelSprites.SetActive(false);
            LoseGame();  
        }
       
    }

    //Reproduce al azar audio con la orden del rostro a formar
    public void RandomPetition()
    {
        int op = UnityEngine.Random.Range(0, audioClipArray.Length);

        switch (op)
        {
            case 0:
                audioPetition.clip = audioClipArray[0];
                _referenciaOpcion = "o_alegreb_alegre";
                break;
            case 1:
                 audioPetition.clip = audioClipArray[1];
                _referenciaOpcion = "o_tristezab_tristeza";
                break;
            case 2:
                audioPetition.clip = audioClipArray[2];
                _referenciaOpcion = "o_miedob_miedo";
                break;
            case 3:
                 audioPetition.clip = audioClipArray[3];
                _referenciaOpcion = "o_enojob_enojo";
                break;
        }

        audioPetition.PlayOneShot(audioPetition.clip);


    }

    //Obtiene el valor del sprite posicionado en el rostro de emojis
    public void HasChanged()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
   
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<SlotContent>().item;
            if (item)
            {
                builder.Append(item.name);
            }
        }
        elementText.text = builder.ToString();
    }

    public void GetAndInitializeAllGameObjects()
    {
        RandomPetition();

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
        timeLeft = di.GetRoundTime(11);
        

    }

    private void SettingTimeOfGame()
    {
        timeLeft = di.GetRoundTime(11);
    }

    private void InitializeRecordAndScore()
    {
        di = new DependencyInjector();
        pd = new PlayerData();
        ld = new LevelData();

        pd = di.GetAllPlayerData();
        ld = di.GetLevelData(11);
        dbRoundtime = di.GetRoundTime(11);
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
        ResetStage();               //Reinicia el escenario
        RandomPetition();           //Realiza peticion de nuevo rostro a formar
        isRoundDone = false;
    }

    //Mensaje de Respuesta incorrecta
    public void FailRound()
    {
        score -= 800;
        UpdateScore();
        SettingTimeOfGame();
        LoseGame();
        ResetStage();               //Reinicia el escenario
        RandomPetition();
        isRoundDone = false;
        
       // di.ResetLevelSuccessTimeByLevel(3);
    }

    //Mensaje de finalizacion de minijuego
    public void CompleteGame()
    {
        di.UpdateLevelTimesPlayed(11);
        di.SaveSuccesTime(new LevelSuccessTime()
        {
            LevelID = 11,
            SuccessTime = dgb.CalculateAverageRound(totalTimeByGame, 3)
        });

        audioSource.Stop();
        isGameDone = true;
        
        if (bestScore == score)
            di.UpdateBestScoreForLevel(11, score);
        di.UpdateTotalizedScore(score);

        gs = new GameStatus();
        gs.PlayerWinGame(audioSource, waitingTime, 11);
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}