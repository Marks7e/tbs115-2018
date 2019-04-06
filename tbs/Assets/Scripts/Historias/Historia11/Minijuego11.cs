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
    public GameObject panelLose;
    public GameObject btnReset, btnContinue, btnTermina;

    public GameObject[] arrayPrefab = new GameObject[8];
   
    private string referenciaOpcion;

    public Button btnCompare;
   
    public AudioSource audioSource;
    public AudioClip bgMusic;
    public AudioSource audioPetition;
    public AudioClip[] audioClipArray;

    public GameObject msj_ok, msj_fail;
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

    private int nchar;
    private int option;
    private int count = 0;
    private int i = 0;

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
        btnContinue.GetComponent<Button>().onClick.AddListener(() => Ok());
        btnReset.GetComponent<Button>().onClick.AddListener(() => Fail());

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
                di.UpdateLevelTimesPlayed(3);
                //UnableGameControls();
                audioSource.Stop();
                isGameDone = true;
                di.ResetLevelSuccessTimeByLevel(3);
                gs = new GameStatus();
                gs.PlayerNeedToRepeatGame(audioSource, waitingTime, 1);
            }

        }

        if (elementText.text.Length > 10)
            {
                
                Nivel.text = count + "/3";
                //Debug.Log(" palabra tiene mas de 10 letras");
                DisablePanelSprites(); //Funcion que desactiva panel de sprites y activa boton
            }
     
         
    }

    public void DisablePanelSprites()
    {
        panelSprites.gameObject.SetActive(false);
        btnCompare.gameObject.SetActive(true);
    }

    public void EnablePanelSprite()
    {
        btnCompare.gameObject.SetActive(false);  //Desactiva boton
        panelSprites.gameObject.SetActive(true); //Activa panel
    }

    public void ResetStage()
    {
        elementText.text = " ";

        referenciaOpcion = " ";

        //Nivel.text = count + "/3";

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

    public void RestockPanelSprite()
    {
        //Vuelve a llenar todo el panel de sprites arrastrables
        foreach (Transform spriteTransform in sprites)
        {
            GameObject objectHijo = Instantiate(arrayPrefab[i]) as GameObject;
            objectHijo.name = arrayPrefab[i].name;
            //objectHijo.transform.parent = spriteTransform.transform;
            objectHijo.transform.SetParent(spriteTransform.transform);
            objectHijo.transform.position = spriteTransform.transform.position;
            i++;
        }

        i = 0;
    }

    public void Validate()
    {
        Debug.Log("intento: " + count);
        //Nivel.text = count + "/3";
        if (count < 3)
        {
            if (elementText.text == referenciaOpcion)
            {
                //Debug.Log("Son iguales, ACERTASTE");
                isRoundDone = true;
                panelSprites.SetActive(false);
                panelWin.SetActive(true);
                count++;
                //Nivel.text = count + "/3";
                //Ok();
                //count++;

                //ResetStage();
                //RandomPetition();
            }
            else
            {
                //Debug.Log("Son diferentes, FALLASTE");
                isRoundDone = true;
                panelSprites.SetActive(false);
                panelLose.SetActive(true);
                //fail();
                //count = 0; //reinicia los intentos
                count = 1;
                //Nivel.text = count + "/3";
            }
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
            complete();
        }

    }

    public void RandomPetition()
    {
        int op = UnityEngine.Random.Range(0, audioClipArray.Length);

        switch (op)
        {
            case 0:
                audioPetition.clip = audioClipArray[0];
                referenciaOpcion = "o_alegreb_alegre";
                break;
            case 1:
                 audioPetition.clip = audioClipArray[1];
                referenciaOpcion = "o_tristezab_tristeza";
                break;
            case 2:
                audioPetition.clip = audioClipArray[2];
                referenciaOpcion = "o_miedob_miedo";
                break;
            case 3:
                 audioPetition.clip = audioClipArray[3];
                referenciaOpcion = "o_enojob_enojo";
                break;
        }

        audioPetition.PlayOneShot(audioPetition.clip);


    }

    public void HasChanged()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        //builder.Append(" - ");
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<SlotContent>().item;
            if (item)
            {
                builder.Append(item.name);
                //builder.Append(" - ");
            }
        }
        elementText.text = builder.ToString();
    }

    public void GetAndInitializeAllGameObjects()
    {
        RandomPetition();

        audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>("Sounds/Minigame");
        audioSource.PlayOneShot(bgMusic);

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
        timeLeft = di.GetRoundTime(3);
        

    }

    private void SettingTimeOfGame()
    {
        timeLeft = di.GetRoundTime(3);
    }

    private void InitializeRecordAndScore()
    {
        di = new DependencyInjector();
        pd = new PlayerData();
        ld = new LevelData();

        pd = di.GetAllPlayerData();
        ld = di.GetLevelData(3);
        dbRoundtime = di.GetRoundTime(3);
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
    public void Ok()
    {
        totalTimeByGame += dbRoundtime - (int)timeLeft;
        UpdateScore();
        SettingTimeOfGame();        //Reinicia el tiempo
        //count++;                    //Aumenta el contador para etiqueta de ronda
        //Nivel.text = count + "/3";
        panelWin.SetActive(false);  //Desactiva panel de mensaje de ganador de ronda
        ResetStage();               //Reinicia el escenario
        RandomPetition();           //Realiza peticion de nuevo rostro a formar
        isRoundDone = false;
    }

    //Mensaje de Respuesta incorrecta
    public void Fail()
    {
        score -= 800;
        UpdateScore();
        SettingTimeOfGame();
        //count = 1;
        //Nivel.text = count + "/3";
        panelLose.SetActive(false);
        ResetStage();               //Reinicia el escenario
        RandomPetition();
        isRoundDone = false;
        
       // di.ResetLevelSuccessTimeByLevel(3);
    }

    //Mensaje de finalizacion de minijuego
    public void complete()
    {
        di.UpdateLevelTimesPlayed(3);
        di.SaveSuccesTime(new LevelSuccessTime()
        {
            LevelID = 3,
            SuccessTime = dgb.CalculateAverageRound(totalTimeByGame, 3)
        });

        audioSource.Stop();
        isGameDone = true;
        
        if (bestScore == score)
            di.UpdateBestScoreForLevel(3, score);
        di.UpdateTotalizedScore(score);

        gs = new GameStatus();
        gs.PlayerWinGame(audioSource, waitingTime, 3);
    }


}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}