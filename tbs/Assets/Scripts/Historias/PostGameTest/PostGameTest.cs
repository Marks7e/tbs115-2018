using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class PostGameTest : MonoBehaviour
{
    /*Inyectando dependencias*/
    private DependencyInjector di = null;

    /*Declarando "contenedores" de UI*/
    GameObject TxtQuestion;
    GameObject BtnYes;
    GameObject BtnNo;

    /*Declarando Variables de preguntas y respuestas*/
    public string question;
    public string level;
    public int counter;
    List<QuestionData> listQuestionData = new List<QuestionData>();
    List<QuestionData> listQuestionDataToSave = new List<QuestionData>();
    QuestionData qd = null;

    /*Recuperando nombre de nivel*/
    void Start()
    {
        counter = 3;
        qd = new QuestionData();
        List<QuestionData> listQuestionDataToSave = new List<QuestionData>();
        TxtQuestion = GameObject.Find("Question");
        BtnYes = GameObject.Find("BtnYes");
        BtnNo = GameObject.Find("BtnNo");

        TxtQuestion.SetActive(true);
        BtnYes.SetActive(true);
        BtnNo.SetActive(true);

        LoadAllQuestionsToUse();
        SendQuestionToPlayer(TxtQuestion);
    }

    private bool LoadAllQuestionsToUse()
    {
        try
        {
            di = new DependencyInjector();
            listQuestionData = di.GetAllQuestionData();
            return listQuestionData.Count > 0;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
    }

    public void PositiveAnswer()
    {
        Debug.Log("Contestó que Sí!");
        qd.Answer = "S";
        SaveAnswerForQuestion(qd);        
        SendQuestionToPlayer(TxtQuestion);
    }
    public void NegativeAnswer()
    {
        Debug.Log("Contestó que No!");
        qd.Answer = "N";
        SaveAnswerForQuestion(qd);
        SendQuestionToPlayer(TxtQuestion);
    }

    private void SaveAnswerForQuestion(QuestionData qd)
    {
        listQuestionDataToSave.Add(qd);
    }

    private void SendQuestionToPlayer(GameObject qText)
    {
        try
        {
            if (counter > 0)
            {
                qd = GetRandomQuestion();
                qText.GetComponent<UnityEngine.UI.Text>().text = qd.Question;
                counter--;
            }
            else
            {
                SaveToDatabase(listQuestionDataToSave);
                SceneManager.LoadScene("MainMenu");
            }

            return;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void SaveToDatabase(List<QuestionData> listQuestionDataToSave)
    {
        try
        {
            foreach (QuestionData qd in listQuestionDataToSave)
            {
                di.SaveAnswerForQuestion(qd);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private QuestionData GetRandomQuestion()
    {
        Random r = new Random();
        var value = r.Next(0, listQuestionData.Count - 1);
        var question = listQuestionData[value];
        listQuestionData.RemoveAt(value);
        return question;

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

}
