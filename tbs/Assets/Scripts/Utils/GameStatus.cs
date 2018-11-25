using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameStatus : ScriptableObject, IPointerDownHandler
{

    public enum GameState
    {
        Win = 1,
        TryAgain = 2
    }

    public AudioClip Win;
    public AudioClip Repeat;
    public AudioSource source;

    public void SettingSounds()
    {
        addPhysics2DRaycaster();
        Win = Resources.Load<AudioClip>("Sounds/Win");
        Repeat = Resources.Load<AudioClip>("Sounds/TryAgain");
        //  source = new AudioSource();
        //  source = GetComponent<AudioSource>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Evaluando enviar a TEST o a Menú principal");
    }
    public void PlayerWinGame(AudioSource audioSource)
    {
        source = new AudioSource();
        source = audioSource;
        SettingSounds();
        CreateMessageWindow(GameState.Win);
        source.PlayOneShot(Win);
    }
    public void PlayerNeedToRepeatGame(AudioSource audioSource)
    {
        source = new AudioSource();
        source = audioSource;
        SettingSounds();
        CreateMessageWindow(GameState.TryAgain);
        source.PlayOneShot(Repeat);
    }

    private void addPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = GameObject.FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
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
        GameObject MsgPanel = new GameObject("MsgPanel");
        Canvas canvas = MsgPanel.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.pixelPerfect = true;
        MsgPanel.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        GameObject panel = new GameObject("panel");
        panel.AddComponent<CanvasRenderer>();
        Image msg = panel.AddComponent<Image>();

        switch (state)
        {
            case GameState.Win:
                msg.sprite = Resources.Load<Sprite>("Images/Win");
                MsgPanel.transform.localScale = new Vector3(3, 2, 2);
                break;
            case GameState.TryAgain:
                msg.sprite = Resources.Load<Sprite>("Images/TryAgain");
                MsgPanel.transform.localScale = new Vector3(4, 2, 2);
                break;
            default:
                break;
        }


        canvas.transform.localScale = new Vector3(3, 2, 2);
        panel.transform.localScale = new Vector3(3, 2, 2);
        //  btnContinue.transform.localScale = new Vector3(0.5f, 1, 1);

        panel.transform.SetParent(canvas.transform, false);
        canvas.transform.SetParent(MsgPanel.transform, false);
        //  btnContinue.transform.SetParent(panel.transform, false);
    }


}
