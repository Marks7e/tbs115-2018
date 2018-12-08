using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barra : MonoBehaviour {

	public static int [] slots;
	public float currentTime = 0;
	float maxTime = 1;
	public string sceneToChange;

	// Use this for initialization
	void Start () {
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
								SceneManager.LoadScene(sceneToChange);
							}
						}
					}
				}
			}
		}
	}
}
