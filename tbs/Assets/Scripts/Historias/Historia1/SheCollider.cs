using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionEnter2D(Collision2D collision){
		
		if( collision.gameObject.name == "Yo" ){
			Debug.Log("Perdiste!");
		}
		/*Debug.Log("COlision!");
		Debug.Log( collision.gameObject.name );*/
		
	}
}
