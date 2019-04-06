using UnityEngine;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour {
	public float reloj = 0;
	public string sceneToChange;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		//Llama a la escena del minijuego cuando se termine de reproducir
		reloj -= Time.deltaTime;
		if(reloj<=0){
			SceneManager.LoadScene(sceneToChange);
		}
	}

}
