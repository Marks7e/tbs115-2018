using Assets.Scripts.DataPersistence.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomizeTest : MonoBehaviour
{
    private RealmData rd = null;
    private GameDataPersistence gdp = null;

    public void RandomizeForTest(string levelName)
    {
        try
        {
            rd = new RealmData();
            gdp = new GameDataPersistence();

            if (Random.Range(0, 100.00f) >= 75.00f)
            {
                rd.SaveDataLocally("LevelName", levelName);
                gdp.SaveData(GameDataPersistence.DataType.RealmData, rd);
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
