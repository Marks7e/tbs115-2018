using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour {

	//Carga las Animaciones
	public void loadAnimation(string pAnimacion){


		SceneManager.LoadScene (pAnimacion);
	}


    private void ValidatingEnoughtPointsForLevel(string lvl)
    {
        ShowModal sm = new ShowModal();
        sm.SendNoPointsForLevelModalToView();
    }

}
