using Assets.Scripts.DataPersistence.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostGameTest : MonoBehaviour
{

    /*Declarando "contenedores" de UI*/
    GameObject TxtQuestion;
    GameObject BtnYes;
    GameObject BtnNo;
    GameDataPersistence gdp = new GameDataPersistence();
    PlayerAnswerData pad = new PlayerAnswerData();

    /*Declarando Variables de preguntas y respuestas*/
    public string question;
    public string level;
    public int counter = 0;

    /*Recuperando nombre de nivel*/
    void Start()
    {
        TxtQuestion = GameObject.Find("Question");
        BtnYes = GameObject.Find("BtnYes");
        BtnNo = GameObject.Find("BtnNo");

        TxtQuestion.SetActive(true);
        BtnYes.SetActive(true);
        BtnNo.SetActive(true);

        IDataType data = gdp.LoadData(GameDataPersistence.DataType.TestData);
        SendQuestionToPlayer(data, TxtQuestion);
    }

    public void PositiveAnswer()
    {
        Debug.Log("Contestó que Sí!");
        string res = TxtQuestion.GetComponent<UnityEngine.UI.Text>().text;
        pad.SavePlayerAnswer(res, "Y");
        EvaluatingNewQuestion(gdp, counter, 3);
    }
    public void NegativeAnswer()
    {
        Debug.Log("Contestó que No!");
        string res = TxtQuestion.GetComponent<UnityEngine.UI.Text>().text;
        pad.SavePlayerAnswer(res, "N");
        EvaluatingNewQuestion(gdp, counter, 3);
    }
    private void SendQuestionToPlayer(IDataType data, GameObject qText)
    {
        try
        {
            qText.GetComponent<UnityEngine.UI.Text>().text = data.GetData("SceneName");
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.StackTrace);
        }
    }
    private void EvaluatingTimes(int times)
    {
        if (counter < times)
        {
            counter++;
            Debug.Log(counter);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    private void EvaluatingNewQuestion(GameDataPersistence gdp, int counter, int times)
    {
        EvaluatingTimes(times);
        IDataType data = gdp.LoadData(GameDataPersistence.DataType.TestData);
        SendQuestionToPlayer(data, TxtQuestion);
    }

}
