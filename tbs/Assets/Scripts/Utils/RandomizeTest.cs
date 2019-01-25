using Assets.Scripts.DataPersistence.Models;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomizeTest : MonoBehaviour
{
    private GameDataPersistence gdp = null;
    private SqliteConnection sql = null;

    public void RandomizeForTest(string levelName)
    {
        try
        {
            gdp = new GameDataPersistence(sql);

            if (Random.Range(0, 100.00f) >= 75.00f)
            {
                SceneManager.LoadScene("PostGameTest");
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }

        }
        catch (System.Exception e)
        {
            throw new System.Exception("Stacktrace: " + e.StackTrace);
        }


    }
}
