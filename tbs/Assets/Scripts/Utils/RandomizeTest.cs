using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomizeTest : MonoBehaviour
{
    private SqliteConnection sql = null;
    private DependencyInjector di = null;

    public void RandomizeForTest(int levelID)
    {
        try
        {
            if (Random.Range(0, 100.00f) >= 75.00f)
                SceneManager.LoadScene("PostGameTest");
            SceneManager.LoadScene("MainMenu");
        }
        catch (System.Exception e)
        {
            throw new System.Exception("Stacktrace: " + e.StackTrace);
        }
    }
}
