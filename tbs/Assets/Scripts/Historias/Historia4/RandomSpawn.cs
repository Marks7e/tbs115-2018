using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour {

	public GameObject smugie;
	public GameObject[] smugieList;
	public GameObject conejo;
	public GameObject[] conejoList;

	// Use this for initialization
	void Start () {
		//Random Smugies
		smugieList = GameObject.FindGameObjectsWithTag("Smugie");
		if(smugieList.Length == 0) SmugieSapwn();
		Debug.Log(smugieList.Length);
		//Random Conejos
		conejoList = GameObject.FindGameObjectsWithTag("Conejo");
		if(conejoList.Length == 0) ConejoSapwn();
		Debug.Log(conejoList.Length);
	}
	
	// Update is called once per frame
	void Update () {
		
		
		

	}

	//Random - Metodo generador de smugies
	void SmugieSapwn () {
		for(int i=0; i<6; i++){
			Vector3 randomSpawn = new Vector3(Random.Range(-20f, 20f), Random.Range(-0.7f, 0.9f),0);
			Instantiate(smugie, randomSpawn, transform.rotation);
		}
	}
	//Random - Metodo generador de conejos
	void ConejoSapwn () {
		for(int i=0; i<3; i++){
			Vector3 randomSpawn = new Vector3(Random.Range(-20f, 20f), Random.Range(0f, 0.9f),0);
			Instantiate(conejo, randomSpawn, transform.rotation);
		}
	}
}
