using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Global;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    private DependencyInjector _dependencyInjector = null;

    //Carga las Animaciones
    public void LoadAnimation(string pAnimation)
    {
        if (UnlockLevel(GetHistoryNumber(pAnimation)))
        { SceneManager.LoadScene(pAnimation); }
        else
        { ValidatingEnoughtPointsForLevel(); }
    }

    public void LoadGameDirectly(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadTutorialFirst(string sceneName)
    {
        GlobalVariables.LoadTutorial = sceneName;
        SceneManager.LoadScene("Tutorial");
    }

    private void ValidatingEnoughtPointsForLevel()
    {
        ShowModal showModal = new ShowModal();
        showModal.SendNoPointsForLevelModalToView();
    }

    public bool UnlockLevel(int levelId)
    {
        _dependencyInjector = new DependencyInjector();
        Debug.Log(_dependencyInjector.UnlockGame(levelId));
        return _dependencyInjector.UnlockGame(levelId);
    }

    private int GetHistoryNumber(string levelName)
    {
        return int.Parse(levelName.Split(' ')[1]);
    }
}
