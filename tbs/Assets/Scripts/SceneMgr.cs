using Assets.Scripts.DataPersistence.DependecyInjector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{

    private DependencyInjector di = null;

    //Carga las Animaciones
    public void loadAnimation(string pAnimacion)
    {

        if (UnlockLevel(GetHistoryNumber(pAnimacion)))
        { SceneManager.LoadScene(pAnimacion); }
        else
        { ValidatingEnoughtPointsForLevel(); }

    }


    private void ValidatingEnoughtPointsForLevel()
    {
        ShowModal sm = new ShowModal();
        sm.SendNoPointsForLevelModalToView();
    }

    public bool UnlockLevel(int levelid)
    {
        di = new DependencyInjector();
        Debug.Log(di.UnlockGame(levelid));
        return di.UnlockGame(levelid);
    }

    private int GetHistoryNumber(string levelName)
    {
        return int.Parse(levelName.Split(' ')[1]);
    }
}
