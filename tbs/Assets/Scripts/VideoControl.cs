using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoControl : MonoBehaviour
{
    public EnableSkip enableSkip;
    public GameObject skipButton;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public GameObject btnPlay, btnPause;
    public string sceneToChange;
    public float duration;
    private bool _videoState;

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
            _videoState = true;
            btnPlay.SetActive(false);
            btnPause.SetActive(true);
            enableSkip.EnableSkipButton(GetLevelNumberFromSceneToLoad("Tutorial"), 5, skipButton);
        }
        else
        {
            _videoState = false;
            btnPause.SetActive(false);
            btnPlay.SetActive(true);
            enableSkip.EnableSkipButton(GetLevelNumberFromSceneToLoad("Tutorial"), 5, skipButton);
        }

        if (videoPlayer.time >= duration)
        {
            SceneManager.LoadScene("Tutorial");
        }

    }

    public void ActionPause()
    {
        videoPlayer.Pause();
        audioSource.Pause();
    }

    public void ActionPlay()
    {
        videoPlayer.Play();
        audioSource.Play();
    }

    private int GetLevelNumberFromSceneToLoad(string sceneToChange)
    {
        return int.Parse(sceneToChange.Split(' ')[1]);
    }
}
