using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Minijuego3 : MonoBehaviour
{
    public GameObject personaje1, personaje2, personaje3;
    public GameObject simbolo1, simbolo2, simbolo3;
    public GameObject btnCirculo, btnTriangulo, btnCuadrado, btnMano, btnReset, btnContinue, btnTermina;
    public GameObject msj_ok, msj_fail, msj_complete;
    public GameObject texto;
    public Text BestScore, Score;
    public Button BtMano, BtCirculo, BtCuadrado, BtTriangulo;
    public GameStatus gs;
    public AudioSource audioSource;
    public AudioClip bgMusic;
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

    public DependencyInjector di;
    public DynamicGameBalance dgb;

    //Texto a mostrar al usuario
    public Text Nivel;
    public Text timing;

    //Manejo de Animaciones
    public AnimationClip speak01, speak02, speak03;
    private Animation animacionSpeak1, animacionSpeak2, animacionSpeak3;

    public Sprite Circulo, Triangulo, Cuadrado;

    private int _x, _y, _z, count = 1;
    private string _sign;

    // Use this for initialization
    void Start()
    {
        GetAndInitializeAllGameObjects();
        InitializeRecordAndScore();
        RandomSpeak();
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
                UnableGameControls();
                audioSource.Stop();
                isGameDone = true;
                di.ResetLevelSuccessTimeByLevel(3);
                gs = new GameStatus();
                gs.PlayerNeedToRepeatGame(audioSource, waitingTime, 1);
            }

        }
    }
    public void RandomSpeak()
    {
        if (count <= 3)
        {
            //Random Animacion
            _x = UnityEngine.Random.Range(1, 4);
            switch (_x)
            {
                case 1:
                    //Habla personaje 1
                    animacionSpeak1.GetComponent<Animation>().Play("Speak01");

                    //Random de Simbolo para el personaje que habla 
                    _y = UnityEngine.Random.Range(1, 4);
                    simbolo1.GetComponent<Image>().enabled = true;
                    switch (_y)
                    {
                        case 1:
                            simbolo1.GetComponent<Image>().sprite = Triangulo;
                            _sign = "triangulo"; //simbolo asignado al personaje que esta hablando
                            break;
                        case 2:
                            simbolo1.GetComponent<Image>().sprite = Circulo;
                            _sign = "circulo";
                            break;
                        case 3:
                            simbolo1.GetComponent<Image>().sprite = Cuadrado;
                            _sign = "cuadrado";
                            break;
                    }
                    break;
                case 2:
                    //Habla personaje 2
                    animacionSpeak2.GetComponent<Animation>().Play("Speak02");

                    //Random de Simbolos 
                    //Random de Simbolos 
                    _y = UnityEngine.Random.Range(1, 4);
                    simbolo2.GetComponent<Image>().enabled = true;
                    switch (_y)
                    {
                        case 1:
                            simbolo2.GetComponent<Image>().sprite = Triangulo;
                            _sign = "triangulo";
                            break;
                        case 2:
                            simbolo2.GetComponent<Image>().sprite = Circulo;
                            _sign = "circulo";
                            break;
                        case 3:
                            simbolo2.GetComponent<Image>().sprite = Cuadrado;
                            _sign = "cuadrado";
                            break;
                    }
                    break;
                case 3:
                    //Habla personaje 3
                    animacionSpeak3.GetComponent<Animation>().Play("Speak03");

                    //Random de Simbolos 
                    _y = UnityEngine.Random.Range(1, 4);
                    simbolo3.GetComponent<Image>().enabled = true;
                    switch (_y)
                    {
                        case 1:
                            simbolo3.GetComponent<Image>().sprite = Triangulo;
                            _sign = "triangulo";
                            break;
                        case 2:
                            simbolo3.GetComponent<Image>().sprite = Circulo;
                            _sign = "circulo";
                            break;
                        case 3:
                            simbolo3.GetComponent<Image>().sprite = Cuadrado;
                            _sign = "cuadrado";
                            break;
                    }
                    break;
            }
            if (simbolo1.GetComponent<Image>().isActiveAndEnabled)
            {
                //Luego de verificar si simbolo1 esta activado, se activa el simbolo2 
                simbolo2.GetComponent<Image>().enabled = true;
                simbolo3.GetComponent<Image>().enabled = true;
                switch (_sign)
                {
                    case "triangulo":
                        _z = UnityEngine.Random.Range(1, 3);
                        switch (_z)
                        {
                            case 1:
                                simbolo2.GetComponent<Image>().sprite = Circulo;
                                simbolo3.GetComponent<Image>().sprite = Cuadrado;
                                break;
                            case 2:
                                simbolo2.GetComponent<Image>().sprite = Cuadrado;
                                simbolo3.GetComponent<Image>().sprite = Circulo;
                                break;
                        }
                        break;
                    case "circulo":
                        _z = UnityEngine.Random.Range(1, 3);
                        switch (_z)
                        {
                            case 1:
                                simbolo2.GetComponent<Image>().sprite = Triangulo;
                                simbolo3.GetComponent<Image>().sprite = Cuadrado;
                                break;
                            case 2:
                                simbolo2.GetComponent<Image>().sprite = Cuadrado;
                                simbolo3.GetComponent<Image>().sprite = Triangulo;
                                break;
                        }
                        break;
                    case "cuadrado":
                        _z = UnityEngine.Random.Range(1, 3);
                        switch (_z)
                        {
                            case 1:
                                simbolo2.GetComponent<Image>().sprite = Circulo;
                                simbolo3.GetComponent<Image>().sprite = Triangulo;
                                break;
                            case 2:
                                simbolo2.GetComponent<Image>().sprite = Triangulo;
                                simbolo3.GetComponent<Image>().sprite = Circulo;
                                break;
                        }
                        break;
                }
            }
            else
            {
                if (simbolo2.GetComponent<Image>().isActiveAndEnabled)
                {
                    simbolo1.GetComponent<Image>().enabled = true;
                    simbolo3.GetComponent<Image>().enabled = true;
                    switch (_sign)
                    {
                        case "triangulo":
                            _z = UnityEngine.Random.Range(1, 3);
                            switch (_z)
                            {
                                case 1:
                                    simbolo1.GetComponent<Image>().sprite = Circulo;
                                    simbolo3.GetComponent<Image>().sprite = Cuadrado;
                                    break;
                                case 2:
                                    simbolo1.GetComponent<Image>().sprite = Cuadrado;
                                    simbolo3.GetComponent<Image>().sprite = Circulo;
                                    break;
                            }
                            break;
                        case "circulo":
                            _z = UnityEngine.Random.Range(1, 3);
                            switch (_z)
                            {
                                case 1:
                                    simbolo1.GetComponent<Image>().sprite = Triangulo;
                                    simbolo3.GetComponent<Image>().sprite = Cuadrado;
                                    break;
                                case 2:
                                    simbolo1.GetComponent<Image>().sprite = Cuadrado;
                                    simbolo3.GetComponent<Image>().sprite = Triangulo;
                                    break;
                            }
                            break;
                        case "cuadrado":
                            _z = UnityEngine.Random.Range(1, 3);
                            switch (_z)
                            {
                                case 1:
                                    simbolo1.GetComponent<Image>().sprite = Circulo;
                                    simbolo3.GetComponent<Image>().sprite = Triangulo;
                                    break;
                                case 2:
                                    simbolo1.GetComponent<Image>().sprite = Triangulo;
                                    simbolo3.GetComponent<Image>().sprite = Circulo;
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    simbolo1.GetComponent<Image>().enabled = true;
                    simbolo2.GetComponent<Image>().enabled = true;
                    switch (_sign)
                    {
                        case "triangulo":
                            _z = UnityEngine.Random.Range(1, 3);
                            switch (_z)
                            {
                                case 1:
                                    simbolo1.GetComponent<Image>().sprite = Circulo;
                                    simbolo2.GetComponent<Image>().sprite = Cuadrado;
                                    break;
                                case 2:
                                    simbolo1.GetComponent<Image>().sprite = Cuadrado;
                                    simbolo2.GetComponent<Image>().sprite = Circulo;
                                    break;
                            }
                            break;
                        case "circulo":
                            _z = UnityEngine.Random.Range(1, 3);
                            switch (_z)
                            {
                                case 1:
                                    simbolo1.GetComponent<Image>().sprite = Triangulo;
                                    simbolo2.GetComponent<Image>().sprite = Cuadrado;
                                    break;
                                case 2:
                                    simbolo1.GetComponent<Image>().sprite = Cuadrado;
                                    simbolo2.GetComponent<Image>().sprite = Triangulo;
                                    break;
                            }
                            break;
                        case "cuadrado":
                            _z = UnityEngine.Random.Range(1, 3);
                            switch (_z)
                            {
                                case 1:
                                    simbolo1.GetComponent<Image>().sprite = Circulo;
                                    simbolo2.GetComponent<Image>().sprite = Triangulo;
                                    break;
                                case 2:
                                    simbolo1.GetComponent<Image>().sprite = Triangulo;
                                    simbolo2.GetComponent<Image>().sprite = Circulo;
                                    break;
                            }
                            break;
                    }
                }

            }

        }
        else
        {
            Complete();
        }
    }
    public void IsCircle()
    {
        if (btnCirculo.name == _sign)
        {
            Ok();
        }
        else
        {
            Fail();
        }

        DisableButton();
    }
    public void IsTriangle()
    {
        if (btnTriangulo.name == _sign)
        {
            Ok();
        }
        else
        {
            Fail();
        }

        DisableButton();
    }
    public void IsSquare()
    {
        if (btnCuadrado.name == _sign)
        {
            Ok();
        }
        else
        {
            Fail();
        }

        DisableButton();
    }
    //Accion oculta mano y activa simbolos
    public void HandAction()
    {

        //Ocultar Botones con simbolos
        btnCirculo.SetActive(true);
        btnCuadrado.SetActive(true);
        btnTriangulo.SetActive(true);

        btnMano.SetActive(false);

    }
    //Oculta simbolos de botones
    public void DisableButton()
    {
        btnCirculo.SetActive(false);
        btnCuadrado.SetActive(false);
        btnTriangulo.SetActive(false);
    }
    //Mensaje de respuesta correcta
    public void Ok()
    {
        totalTimeByGame += dbRoundtime - (int)timeLeft;
        UpdateScore();
        SettingTimeOfGame();
        msj_ok.SetActive(true);
        btnContinue.SetActive(true);
        isRoundDone = true;
    }
    //Mensaje de Respuesta incorrecta
    public void Fail()
    {
        score -= 800;
        msj_fail.SetActive(true);
        btnReset.SetActive(true);
        isRoundDone = true;
        di.ResetLevelSuccessTimeByLevel(3);
    }
    //Mensaje de finalizacion de minijuego
    public void Complete()
    {
        di.UpdateLevelTimesPlayed(3);
        di.SaveSuccesTime(new LevelSuccessTime()
        {
            LevelID = 3,
            SuccessTime = dgb.CalculateAverageRound(totalTimeByGame, 3)
        });

        audioSource.Stop();
        isGameDone = true;
        btnMano.SetActive(false);

        if (bestScore == score)
            di.UpdateBestScoreForLevel(3, score);
        di.UpdateTotalizedScore(score);

        gs = new GameStatus();
        gs.PlayerWinGame(audioSource, waitingTime, 3);
    }
    //Restaura el minijuego para la siguiente iteracion
    public void Iteration()
    {
        isRoundDone = false;
        //Deshabilitar simbolos
        simbolo1.GetComponent<Image>().enabled = false;
        simbolo2.GetComponent<Image>().enabled = false;
        simbolo3.GetComponent<Image>().enabled = false;

        //Deshabilitar botones con simbolo
        DisableButton();

        //Habilitar Mano
        btnMano.SetActive(true);

        //Setando Texto
        Nivel.text = count + "/3";

        //Contador
        count++;

        msj_ok.SetActive(false);
        btnContinue.SetActive(false);

        //Detener todas las animaciones
        animacionSpeak1.GetComponent<Animation>().Stop("Speak01");
        animacionSpeak2.GetComponent<Animation>().Stop("Speak02");
        animacionSpeak3.GetComponent<Animation>().Stop("Speak03");

        RandomSpeak();
    }
    private void UnableGameControls()
    {
        DisableButton();
        btnMano.SetActive(false);
        animacionSpeak1.GetComponent<Animation>().Stop("Speak01");
        animacionSpeak2.GetComponent<Animation>().Stop("Speak02");
        animacionSpeak3.GetComponent<Animation>().Stop("Speak03");
    }
    private void GetAndInitializeAllGameObjects()
    {
        audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>("Sounds/Minigame");
        audioSource.PlayOneShot(bgMusic);
        btnMano = GameObject.Find("btnMano");
        btnCirculo = GameObject.Find("circulo");
        btnCuadrado = GameObject.Find("cuadrado");
        btnTriangulo = GameObject.Find("triangulo");

        BtMano = btnMano.GetComponent<Button>();
        BtCirculo = btnCirculo.GetComponent<Button>();
        BtCuadrado = btnCuadrado.GetComponent<Button>();
        BtTriangulo = btnTriangulo.GetComponent<Button>();

        texto = new GameObject();
        texto = GameObject.Find("Timing");
        timing = texto.GetComponent<Text>();

        timing.text = "Tiempo";

        animacionSpeak1 = personaje1.AddComponent<Animation>();
        animacionSpeak2 = personaje2.AddComponent<Animation>();
        animacionSpeak3 = personaje3.AddComponent<Animation>();
        animacionSpeak1.AddClip(speak01, "Speak01");
        animacionSpeak2.AddClip(speak02, "Speak02");
        animacionSpeak3.AddClip(speak03, "Speak03");

        simbolo1.GetComponent<Image>().enabled = false;
        simbolo2.GetComponent<Image>().enabled = false;
        simbolo3.GetComponent<Image>().enabled = false;

        /*Para control de puntajes.*/
        var objBestScore = GameObject.Find("BestScore");
        var objScore = GameObject.Find("Score");
        BestScore = objBestScore.GetComponent<Text>();
        Score = objScore.GetComponent<Text>();
        di = new DependencyInjector();
        dgb = new DynamicGameBalance();
        timeLeft = di.GetRoundTime(3);

        //Ocultar Botones con simbolos
        btnCirculo.SetActive(false);
        btnCuadrado.SetActive(false);
        btnTriangulo.SetActive(false);
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

}

