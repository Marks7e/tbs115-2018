using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bar : MonoBehaviour {
    
	public static int [] slots;
	public float currentTime = 0;
	float maxTime = 1;
	
    //Ganar/Perder
    public GameStatus gs;
    public AudioSource audioSource;
    public AudioClip bgMusic;
    public int waitingTime = 3;
    private string musicName = "Sounds/Minigame";
	private int index = 0;

	// Use this for initialization
	void Start () {
		GetInitializeMusic();
		slots = new int[5];
	}
	
	// Update is called once per frame
	void Update () {
		
		currentTime += Time.deltaTime;
		
		if(currentTime >= maxTime){
			currentTime = 0;
			if(slots[0] == 1)
			{
				if(slots[1] == 1)
				{
					if(slots[2] == 1)
					{
						if(slots[3] == 1)
						{
							if(slots[4] == 1)
							{
								index++;
								WinGame();
							}
						}
					}
				}
			}
		}
	}

	void GetInitializeMusic()
    {
        audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>(musicName);
        audioSource.clip = bgMusic;
        audioSource.Play(0);
    }

	void WinGame()
    {
		if(index == 1)
		{
			audioSource.Stop();
        	gs = new GameStatus();
        	gs.PlayerWinGame(audioSource, waitingTime, 4);
			index++;
		}
	}
}
