using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class PostGameTest : MonoBehaviour
{
    /*Inyectando dependencias*/
    private DependencyInjector _dependencyInjector = null;

    /*Declarando "contenedores" de UI*/
    GameObject txtQuestion;
    GameObject btnYes;
    GameObject btnNo;

    /*Declarando Variables de preguntas y respuestas*/
    public string question;
    public string level;
    public int counter;
    List<QuestionData> questionDataListModel = new List<QuestionData>();
    List<QuestionData> questionDataListModelToSave = new List<QuestionData>();
    QuestionData questionDataModel = null;

    /*Recuperando nombre de nivel*/
    void Start()
    {
        counter = 3;
        questionDataModel = new QuestionData();
        List<QuestionData> listQuestionDataToSave = new List<QuestionData>();
        txtQuestion = GameObject.Find("Question");
        btnYes = GameObject.Find("BtnYes");
        btnNo = GameObject.Find("BtnNo");

        txtQuestion.SetActive(true);
        btnYes.SetActive(true);
        btnNo.SetActive(true);

        LoadAllQuestionsToUse();
        SendQuestionToPlayer(txtQuestion);
    }

    //void Awake()
    //{
    //    counter = 3;
    //    questionDataModel = new QuestionData();
    //    List<QuestionData> listQuestionDataToSave = new List<QuestionData>();
    //    txtQuestion = GameObject.Find("Question");
    //    btnYes = GameObject.Find("BtnYes");
    //    btnNo = GameObject.Find("BtnNo");

    //    txtQuestion.SetActive(true);
    //    btnYes.SetActive(true);
    //    btnNo.SetActive(true);

    //    LoadAllQuestionsToUse();
    //    SendQuestionToPlayer(txtQuestion);
    //}

    private bool LoadAllQuestionsToUse()
    {
        try
        {
            int lastLevelPlayed = 0;
            int realmForLastLevelPlayed = 0;
            _dependencyInjector = new DependencyInjector();

            lastLevelPlayed = _dependencyInjector.GetLastGamePlayed().LevelID;
            realmForLastLevelPlayed = _dependencyInjector.GetRealmNumberFromLevelId(lastLevelPlayed);

            //questionDataListModel = _dependencyInjector.GetAllQuestionData();
            questionDataListModel = _dependencyInjector.GetAllRealmQuestionForRealm(realmForLastLevelPlayed);

            return questionDataListModel.Count > 0;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
    }

    public void PositiveAnswer()
    {
        //Debug.Log("Contestó que Sí!");
        questionDataModel.Answer = "S";
        SaveAnswerForQuestion(questionDataModel);
        SendQuestionToPlayer(txtQuestion);
    }
    public void NegativeAnswer()
    {
        //Debug.Log("Contestó que No!");
        questionDataModel.Answer = "N";
        SaveAnswerForQuestion(questionDataModel);
        SendQuestionToPlayer(txtQuestion);
    }

    private void SaveAnswerForQuestion(QuestionData questionDataModel)
    {
        questionDataListModelToSave.Add(questionDataModel);
    }

    private void SendQuestionToPlayer(GameObject gameObject)
    {
        try
        {
            if (counter > 0)
            {
                questionDataModel = GetRandomQuestion();
                gameObject.GetComponent<UnityEngine.UI.Text>().text = questionDataModel.Question;
                counter--;
            }
            else
            {
                SaveToDatabase(questionDataListModelToSave);
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
                _dependencyInjector.SaveAnswerForQuestion(qd);
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
        var value = r.Next(0, questionDataListModel.Count - 1);
        var question = questionDataListModel[value];
        questionDataListModel.RemoveAt(value);
        return question;

    }

    private void EvaluatingTimes(int times)
    {
        if (counter < times)
        {
            counter++;
            //Debug.Log(counter);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
