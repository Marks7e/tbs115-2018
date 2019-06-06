using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoPlayer video;
    public AudioSource sound;
    public Slider slider;
    public string newScene;
    public float duration; //duracion

    public EnableSkip enableSkip;
    private GameObject _skipButton;
    private GameObject _playButton;
    private GameObject _pauseButton;
    private bool _videoState;

    //propiedades de video player
    bool isDone;

    public bool IsPlaying
    {
        get { return video.isPlaying; }
    }

    public bool IsLooping
    {
        get { return video.isLooping; }
    }

    public bool IsPrepared
    {
        get { return video.isPrepared; }
    }

    public bool IsDone
    {
        get { return isDone; }
    }

    public double Time
    {
        get { return video.time; }
    }

    public ulong Duration
    {
        get { return (ulong)(video.frameCount / video.frameRate); }
    }

    public double NTime
    {
        get { return Time / Duration; }
    }


    // Use this for initialization
    void Start()
    {
        enableSkip = new EnableSkip();
        _skipButton = GameObject.Find("Saltar");
        _playButton = GameObject.Find("Play");
        _pauseButton = GameObject.Find("Pause");
    }

    void Update()
    {
        if (!IsPrepared) return;

        slider.value = (float)NTime;

        if (IsPlaying)
        {
            _videoState = true;
            _playButton.SetActive(false);
            _pauseButton.SetActive(true);
            enableSkip.EnableSkipButton(GetLevelNumberFromSceneToLoad(newScene), 5, _skipButton);

        }
        else
        {
            _videoState = false;
            _pauseButton.SetActive(false);
            _playButton.SetActive(true);
            enableSkip.EnableSkipButton(GetLevelNumberFromSceneToLoad(newScene), 5, _skipButton);

        }

        if (Time >= duration) ChangeScene(); //cambio de escena automatico 


    }

    public void PlayVideo()
    {
        if (!IsPrepared) return;
        video.Play();
        sound.Play();
    }

    public void PauseVideo()
    {
        if (!IsPlaying) return;
        video.Pause();
        sound.Pause();
    }

    public void RestartVideo()
    {
        if (!IsPrepared) return;
        PauseVideo();
        Seek(0);
    }

    public void Seek(float nTime)
    {
        if (!video.canSetTime) return;
        if (!IsPrepared) return;
        nTime = Mathf.Clamp(nTime, 0, 1);
        video.time = nTime * Duration;
        sound.time = nTime * Duration;
        PlayVideo();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Tutorial");
    }

    private int GetLevelNumberFromSceneToLoad(string sceneToChange)
    {
        return int.Parse(sceneToChange.Split(' ')[1]);
    }
}
