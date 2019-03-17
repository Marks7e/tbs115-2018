using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoControl : MonoBehaviour
{
    public EnableSkip enableSkip;
    public GameObject skipButton;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public GameObject btnPlay, btnPause;
    public string sceneToChange;
    public float duration;
    private bool videostate;

    // Use this for initialization
    void Start()
    {
        enableSkip = new EnableSkip();
        skipButton = GameObject.Find("Saltar");
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            videostate = true;
            btnPlay.SetActive(false);
            btnPause.SetActive(true);
            enableSkip.EnableSkipButton(getLevelNumberFromSceneToLoad(sceneToChange), 5, skipButton);
            //Debug.Log("Esta reproduciendo"+", "+"frame: "+videoPlayer.frame+", "+"tiempo: "+videoPlayer.time);
        }
        else
        {
            videostate = false;
            btnPause.SetActive(false);
            btnPlay.SetActive(true);
            enableSkip.EnableSkipButton(getLevelNumberFromSceneToLoad(sceneToChange), 5, skipButton);
            //Debug.Log("Esta Pausado"+" "+"frame: "+videoPlayer.frame+", "+"tiempo: "+videoPlayer.time);
        }

        if (videoPlayer.time >= duration)
        {
            //Debug.Log("cambio de escena");
            SceneManager.LoadScene(sceneToChange);
        }

    }

    public void actionPause()
    {
        videoPlayer.Pause();
        audioSource.Pause();
    }

    public void actionPlay()
    {
        videoPlayer.Play();
        audioSource.Play();
    }

    private int getLevelNumberFromSceneToLoad(string sceneToChange)
    {
        return int.Parse(sceneToChange.Split(' ')[1]);
    }
}
