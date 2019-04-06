using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomizeTest : MonoBehaviour
{
    private DependencyInjector _dependencyInjector;
    public void RandomizeForTest()
    {
        try
        {
            if (Random.Range(0, 100.00f) >= 0.0f)
            { SceneManager.LoadScene("PostGameTest"); }
            else
            { SceneManager.LoadScene("MainMenu"); }
        }
        catch (System.Exception e)
        {
            throw new System.Exception("Stacktrace: " + e.StackTrace);
        }
    }
}
