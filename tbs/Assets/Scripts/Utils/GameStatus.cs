using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Global;

public class GameStatus : ScriptableObject
{
    public AudioClip win;
    public AudioClip repeat;
    public AudioSource source;
    private RandomizeTest _randomizeTestModel;
    private DependencyInjector _dependencyInjector;

    public int LevelId { get; set; }

    public enum GameState
    {
        Win,
        TryAgain
    }

    public void SettingSounds()
    {
        win = Resources.Load<AudioClip>("Sounds/Win");
        repeat = Resources.Load<AudioClip>("Sounds/TryAgain");
        GetGeneralVolume();
    }
    public void PlayerWinGame(AudioSource audioSource, int waitSeconds, int levelId)
    {
        LevelId = levelId;
        source = new AudioSource();
        source = audioSource;
        SettingSounds();
        CreateMessageWindow(GameState.Win);
        source.PlayOneShot(win);
    }
    public void PlayerNeedToRepeatGame(AudioSource audioSource, int waitSeconds, int levelId)
    {
        source = new AudioSource();
        source = audioSource;
        SettingSounds();
        CreateMessageWindow(GameState.TryAgain);
        source.PlayOneShot(repeat);
        new WaitForSeconds(waitSeconds);
    }

    private void CreateMessageWindow(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Win:
                {
                    if (!GameObject.Find("MsgPanel"))
                        CreatingMessageWindowHierarchy(GameState.Win);
                    break;
                }

            case GameState.TryAgain:
                {
                    if (!GameObject.Find("MsgPanel"))
                        CreatingMessageWindowHierarchy(GameState.TryAgain);
                    break;
                }
            default:
                throw new System.Exception("Error en tipo de mensaje a renderizar. Solamente existe GANAR o INTENTAR DE NUEVO!");
        }
    }
    private void CreatingMessageWindowHierarchy(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Win:
                SettingSizePositionAndHierarchyWhenWinGame();
                break;
            case GameState.TryAgain:
                SettingSizePositionAndHierarchyWhenLoseGame();
                break;
        }
    }
    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void ContinueWithGame()
    {
        _randomizeTestModel = new RandomizeTest();
        _randomizeTestModel.RandomizeForTest();
    }
    private void RepeatGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void SettingSizePositionAndHierarchyWhenWinGame()
    {
        GameObject mainCanvas = new GameObject();
        GameObject msgPanel = new GameObject("MsgPanel");
        GameObject panel = new GameObject("panel");

        mainCanvas = GameObject.Find("Canvas");

        Canvas canvas = msgPanel.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.pixelPerfect = true;
        msgPanel.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        panel.AddComponent<CanvasRenderer>();
        Image msg = panel.AddComponent<Image>();

        Button btnContinue = Instantiate(Resources.Load<Button>("Prefabs/BtnCountinue"));
        Button btnRepeatLevel = Instantiate(Resources.Load<Button>("Prefabs/BtnRepeat"));
        Button btnGoToMainMenu = Instantiate(Resources.Load<Button>("Prefabs/BtnGoToMainMenu"));

        btnContinue.onClick.AddListener(ContinueWithGame);
        btnRepeatLevel.onClick.AddListener(RepeatGame);
        btnGoToMainMenu.onClick.AddListener(ReturnToMainMenu);

        msg.sprite = Resources.Load<Sprite>("Images/Win");
        msgPanel.transform.localScale = new Vector3(3, 2, 1);
        btnContinue.transform.localScale = new Vector3(1, 1, 1);
        btnContinue.transform.localPosition = new Vector3(0, -80f, 5);
        btnRepeatLevel.transform.localScale = new Vector3(1, 1, 1);
        btnRepeatLevel.transform.localPosition = new Vector3(0, -110f, 5);
        btnGoToMainMenu.transform.localScale = new Vector3(1, 1, 1);
        btnGoToMainMenu.transform.localPosition = new Vector3(0, -140f, 5);
        msgPanel.transform.localScale = new Vector3(1, 1, 1);
        canvas.transform.localScale = new Vector3(3, 2, 1);
        panel.transform.localScale = new Vector3(3, 2, 1);
        panel.transform.SetParent(canvas.transform, false);
        canvas.transform.SetParent(msgPanel.transform, false);
        btnContinue.transform.SetParent(mainCanvas.transform, false);
        btnRepeatLevel.transform.SetParent(mainCanvas.transform, false);
        btnGoToMainMenu.transform.SetParent(mainCanvas.transform, false);
    }
    private void SettingSizePositionAndHierarchyWhenLoseGame()
    {
        GameObject mainCanvas = new GameObject();
        GameObject msgPanel = new GameObject("MsgPanel");
        GameObject panel = new GameObject("panel");

        mainCanvas = GameObject.Find("Canvas");

        Canvas canvas = msgPanel.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.pixelPerfect = true;
        msgPanel.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        panel.AddComponent<CanvasRenderer>();
        Image msg = panel.AddComponent<Image>();

        Button btnContinue = Instantiate(Resources.Load<Button>("Prefabs/BtnCountinue"));
        Button btnRepeatLevel = Instantiate(Resources.Load<Button>("Prefabs/BtnRepeat"));
        Button btnGoToMainMenu = Instantiate(Resources.Load<Button>("Prefabs/BtnGoToMainMenu"));

        btnContinue.onClick.AddListener(ContinueWithGame);
        btnRepeatLevel.onClick.AddListener(RepeatGame);
        btnGoToMainMenu.onClick.AddListener(ReturnToMainMenu);

        msg.sprite = Resources.Load<Sprite>("Images/TryAgain");
        msgPanel.transform.localScale = new Vector3(4, 2, 1);
        btnRepeatLevel.transform.localScale = new Vector3(1, 1, 1);
        btnRepeatLevel.transform.localPosition = new Vector3(0, -110f, 5);
        btnGoToMainMenu.transform.localScale = new Vector3(1, 1, 1);
        btnGoToMainMenu.transform.localPosition = new Vector3(0, -140f, 5);
        msgPanel.transform.localScale = new Vector3(1, 1, 1);
        canvas.transform.localScale = new Vector3(3, 2, 1);
        panel.transform.localScale = new Vector3(3, 2, 1);
        panel.transform.SetParent(canvas.transform, false);
        canvas.transform.SetParent(msgPanel.transform, false);
        btnRepeatLevel.transform.SetParent(mainCanvas.transform, false);
        btnGoToMainMenu.transform.SetParent(mainCanvas.transform, false);
    }

    private void GetGeneralVolume()
    {
        float generalVolume = GlobalVariables.GeneralVolume;
        source.volume = generalVolume;
    }
}
