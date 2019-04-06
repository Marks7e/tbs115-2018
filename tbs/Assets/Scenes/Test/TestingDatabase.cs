using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using System.Collections.Generic;
using UnityEngine;

public class TestingDatabase : MonoBehaviour
{
    public DependencyInjector dependencyInjector;
    public LevelSuccessTime levelSuccessTimeModel;
    public List<LevelSuccessTime> levelSuccesTimeListModel;

    public void CreateNewEntryForLevelSuccessTime()
    {
        dependencyInjector = new DependencyInjector();
        levelSuccessTimeModel = new LevelSuccessTime();
        levelSuccessTimeModel.LevelID = (int)Random.Range(1f, 3f);
        levelSuccessTimeModel.SuccessTime = (int)Random.Range(0f, 100f);
        dependencyInjector.SaveSuccesTime(levelSuccessTimeModel);
    }

    public void GetRandomEntryFromLevelSuccessTime()
    {
        dependencyInjector = new DependencyInjector(); 
        levelSuccessTimeModel = new LevelSuccessTime();
        levelSuccesTimeListModel = new List<LevelSuccessTime>();
        levelSuccesTimeListModel = dependencyInjector.GetAllLevelSuccessTime();
        levelSuccessTimeModel = levelSuccesTimeListModel[Random.Range(1, levelSuccesTimeListModel.Count)];
    }
}
