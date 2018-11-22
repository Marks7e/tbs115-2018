using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoControl : MonoBehaviour {

	public VideoPlayer videoPlayer;
	public AudioSource audioSource;
	public GameObject btnPlay,btnPause;
	public string sceneToChange;
	public float duration;
	private bool videostate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(videoPlayer.isPlaying){
			videostate = true;
			btnPlay.SetActive(false);
			btnPause.SetActive(true);
			Debug.Log("Esta reproduciendo"+", "+"frame: "+videoPlayer.frame+", "+"tiempo: "+videoPlayer.time);
		}else{
			videostate = false;
			btnPause.SetActive(false);
			btnPlay.SetActive(true);
			Debug.Log("Esta Pausado"+" "+"frame: "+videoPlayer.frame+", "+"tiempo: "+videoPlayer.time);
		}

		if(videoPlayer.time >= duration){
			Debug.Log("cambio de escena");
			SceneManager.LoadScene(sceneToChange);
		}

	}

	public void actionPause(){		
		videoPlayer.Pause();
		audioSource.Pause();
	}

	public  void actionPlay(){
		videoPlayer.Play();
		audioSource.Play();
	}
}
