using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class GameStatus : ScriptableObject
{
    
    public enum GameState
    {
        Win = 1,
        TryAgain = 2
    }

    public AudioClip win;
    public AudioClip repeat;
    public AudioSource source;

    public void SettingSounds()
    {
        win = Resources.Load<AudioClip>("Sounds/Win");
        repeat = Resources.Load<AudioClip>("Sounds/TryAgain");
    }
    public void PlayerWinGame(AudioSource audioSource, int waitSeconds)
    {
        source = new AudioSource();
        source = audioSource;
        SettingSounds();
        CreateMessageWindow(GameState.Win);
        source.PlayOneShot(win);
    }
    public void PlayerNeedToRepeatGame(AudioSource audioSource, int waitSeconds)
    {
        source = new AudioSource();
        source = audioSource;
        SettingSounds();
        CreateMessageWindow(GameState.TryAgain);
        source.PlayOneShot(repeat);
        new WaitForSeconds(waitSeconds);
    }

    private void CreateMessageWindow(GameState state)
    {
        switch (state)
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
    private void CreatingMessageWindowHierarchy(GameState state)
    {
        switch (state)
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
        string actualLvlName = SceneManager.GetActiveScene().name;
        RandomizeTest rt = new RandomizeTest();
        rt.RandomizeForTest(actualLvlName);
    }
    private void RepeatGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void SettingSizePositionAndHierarchyWhenWinGame()
    {
        GameObject MainCanvas = new GameObject();
        GameObject MsgPanel = new GameObject("MsgPanel");
        GameObject panel = new GameObject("panel");

        MainCanvas = GameObject.Find("Canvas");

        Canvas canvas = MsgPanel.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.pixelPerfect = true;
        MsgPanel.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        panel.AddComponent<CanvasRenderer>();
        Image msg = panel.AddComponent<Image>();

        Button BtnContinue = Instantiate(Resources.Load<Button>("Prefabs/BtnCountinue"));
        Button BtnRepeatLevel = Instantiate(Resources.Load<Button>("Prefabs/BtnRepeat"));
        Button BtnGoToMainMenu = Instantiate(Resources.Load<Button>("Prefabs/BtnGoToMainMenu"));

        BtnContinue.onClick.AddListener(ContinueWithGame);
        BtnRepeatLevel.onClick.AddListener(RepeatGame);
        BtnGoToMainMenu.onClick.AddListener(ReturnToMainMenu);

        msg.sprite = Resources.Load<Sprite>("Images/Win");
        MsgPanel.transform.localScale = new Vector3(3, 2, 1);
        BtnContinue.transform.localScale = new Vector3(1, 1, 1);
        BtnContinue.transform.localPosition = new Vector3(0, -80f, 5);
        BtnRepeatLevel.transform.localScale = new Vector3(1, 1, 1);
        BtnRepeatLevel.transform.localPosition = new Vector3(0, -110f, 5);
        BtnGoToMainMenu.transform.localScale = new Vector3(1, 1, 1);
        BtnGoToMainMenu.transform.localPosition = new Vector3(0, -140f, 5);
        MsgPanel.transform.localScale = new Vector3(1, 1, 1);
        canvas.transform.localScale = new Vector3(3, 2, 1);
        panel.transform.localScale = new Vector3(3, 2, 1);
        panel.transform.SetParent(canvas.transform, false);
        canvas.transform.SetParent(MsgPanel.transform, false);
        BtnContinue.transform.SetParent(MainCanvas.transform, false);
        BtnRepeatLevel.transform.SetParent(MainCanvas.transform, false);
        BtnGoToMainMenu.transform.SetParent(MainCanvas.transform, false);
    }
    private void SettingSizePositionAndHierarchyWhenLoseGame()
    {
        GameObject MainCanvas = new GameObject();
        GameObject MsgPanel = new GameObject("MsgPanel");
        GameObject panel = new GameObject("panel");

        MainCanvas = GameObject.Find("Canvas");

        Canvas canvas = MsgPanel.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.pixelPerfect = true;
        MsgPanel.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        panel.AddComponent<CanvasRenderer>();
        Image msg = panel.AddComponent<Image>();

        Button BtnContinue = Instantiate(Resources.Load<Button>("Prefabs/BtnCountinue"));
        Button BtnRepeatLevel = Instantiate(Resources.Load<Button>("Prefabs/BtnRepeat"));
        Button BtnGoToMainMenu = Instantiate(Resources.Load<Button>("Prefabs/BtnGoToMainMenu"));

        BtnContinue.onClick.AddListener(ContinueWithGame);
        BtnRepeatLevel.onClick.AddListener(RepeatGame);
        BtnGoToMainMenu.onClick.AddListener(ReturnToMainMenu);

        msg.sprite = Resources.Load<Sprite>("Images/TryAgain");
        MsgPanel.transform.localScale = new Vector3(4, 2, 1);
        BtnRepeatLevel.transform.localScale = new Vector3(1, 1, 1);
        BtnRepeatLevel.transform.localPosition = new Vector3(0, -110f, 5);
        BtnGoToMainMenu.transform.localScale = new Vector3(1, 1, 1);
        BtnGoToMainMenu.transform.localPosition = new Vector3(0, -140f, 5);
        MsgPanel.transform.localScale = new Vector3(1, 1, 1);
        canvas.transform.localScale = new Vector3(3, 2, 1);
        panel.transform.localScale = new Vector3(3, 2, 1);
        panel.transform.SetParent(canvas.transform, false);
        canvas.transform.SetParent(MsgPanel.transform, false);
        BtnRepeatLevel.transform.SetParent(MainCanvas.transform, false);
        BtnGoToMainMenu.transform.SetParent(MainCanvas.transform, false);
    }

}
