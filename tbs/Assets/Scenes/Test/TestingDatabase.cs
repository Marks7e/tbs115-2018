using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDatabase : MonoBehaviour
{
    public DependencyInjector di;
    public LevelSuccessTime lst;
    public List<LevelSuccessTime> llst;

    public void CreateNewEntryForLevelSuccessTime()
    {
        di = new DependencyInjector();
        lst = new LevelSuccessTime();
        lst.LevelID = (int)Random.Range(1f, 3f);
        lst.SuccessTime = (int)Random.Range(0f, 100f);
        di.SaveSuccesTime(lst);
    }

    public void GetRandomEntryFromLevelSuccessTime()
    {
        di = new DependencyInjector(); 
        lst = new LevelSuccessTime();
        llst = new List<LevelSuccessTime>();
        llst = di.GetAllLevelSuccessTime();
        lst = llst[Random.Range(1, llst.Count)];
    }
}
