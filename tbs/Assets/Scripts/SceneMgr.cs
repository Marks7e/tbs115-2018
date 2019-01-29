using Assets.Scripts.DataPersistence.DependecyInjector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour {

    private DependencyInjector di = null;

	//Carga las Animaciones
	public void loadAnimation(string pAnimacion){

        SceneManager.LoadScene (pAnimacion);
	}


    private void ValidatingEnoughtPointsForLevel(string lvl)
    {
        ShowModal sm = new ShowModal();
        sm.SendNoPointsForLevelModalToView();
    }


    public bool UnlockLevel(int levelid)
    {
        di = new DependencyInjector();
        return di.UnlockGame(levelid);
    }
}
