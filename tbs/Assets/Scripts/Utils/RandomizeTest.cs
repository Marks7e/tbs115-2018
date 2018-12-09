using Assets.Scripts.DataPersistence.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomizeTest : MonoBehaviour
{

    //Reservando memoria para variables
    private TestGame tg = null;
    private GameDataPersistence gdp = null;

    public void RandomizeForTest(string levelName)
    {
        try
        {
            tg = new TestGame();
            gdp = new GameDataPersistence();

            if (Random.Range(0, 100.00f) >= 0.00f)
            {
                Debug.Log("Random >= 75.00f");

                if (tg.data.ContainsKey("SceneName"))
                    tg.data.Remove("SceneName");

                tg.SaveData("SceneName", levelName);
                gdp.SaveData(GameDataPersistence.DataType.TestData, tg);

                SceneManager.LoadScene("PostGameTest");
            }
            else
            {
                Debug.Log("Random < 75.00f");
                SceneManager.LoadScene("MainMenu");
            }

        }
        catch (System.Exception e)
        {
            throw new System.Exception("Stacktrace: " + e.StackTrace);
        }


    }
}
