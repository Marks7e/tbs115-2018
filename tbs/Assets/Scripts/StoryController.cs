using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Global;

public class StoryController : MonoBehaviour
{
    public DependencyInjector _di = null;
    public GameObject _audio = null;
    public VideoPlayer video;
    public AudioSource sound;
    public Slider slider;
    public float duration; //duracion
        
    private GameObject _playButton;
    private GameObject _pauseButton;
    private GameObject _replayButton;
    private GameObject _homeButton;

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
        _playButton = GameObject.Find("Play");
        _pauseButton = GameObject.Find("Pause");
        _replayButton = GameObject.Find("Reinicio");
        _homeButton = GameObject.Find("Home");

        _homeButton.SetActive(false);
        _replayButton.SetActive(false);

        _audio = GameObject.Find("Audio");

        GetGeneralVolume();
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
            _homeButton.SetActive(false);
            _replayButton.SetActive(false);
           
        }
        else
        {
            _videoState = false;
            _pauseButton.SetActive(false);
            _playButton.SetActive(true);
            _homeButton.SetActive(false);
            _replayButton.SetActive(false);
            
        }

        if (Time >= duration)
        {
            _playButton.SetActive(false);
            _pauseButton.SetActive(false);
            
            _homeButton.SetActive(true);
            _replayButton.SetActive(true);

        }      

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
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void replayVideo(string animation)
    {
        SceneManager.LoadScene(animation);
    }

    private void GetGeneralVolume()
    {
        _di = new DependencyInjector();
        float generalVolume = GlobalVariables.GeneralVolume;
        _audio.GetComponent<AudioSource>().volume = generalVolume;
    }
}
