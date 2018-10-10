using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Carga las Animaciones
	public void loadAnimation(string pAnimacion){
		SceneManager.LoadScene (pAnimacion);
	}
}
