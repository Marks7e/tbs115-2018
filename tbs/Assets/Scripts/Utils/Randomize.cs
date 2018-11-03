using Assets.Scripts.DataPersistence.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Randomize : MonoBehaviour
{
    public void RandomizeForTest(string levelName)
    {
        try
        {

            if (Random.Range(0, 100.00f) >= 0.00f)
            {
                Debug.Log("Random >= 75.00f");
                TestGame tg = new TestGame();
                GameDataPersistence gdp = new GameDataPersistence();

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
            Debug.LogError("Stacktrace: " + e.StackTrace);

        }


    }
}
