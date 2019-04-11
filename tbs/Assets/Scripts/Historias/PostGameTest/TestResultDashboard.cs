using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestResultDashboard : MonoBehaviour
{
    private GameObject _chartObject;
    private Image _charImage;
    private float _waitTime = 1.5f;
    private List<QuestionData> _questionDataListModelForRealm1;
    private List<QuestionData> _questionDataListModelForRealm2;
    private List<QuestionData> _questionDataListModelForRealm3;
    private DependencyInjector _di;

    void Start()
    {
        InitializeAllObjects();
    }
    void Update()
    {
        AnimateGraph(_waitTime);
    }
    private void InitializeAllObjects()
    {
        _di = new DependencyInjector();
        _questionDataListModelForRealm1 = new List<QuestionData>();
        _questionDataListModelForRealm2 = new List<QuestionData>();
        _questionDataListModelForRealm3 = new List<QuestionData>();
        _charImage = GameObject.Find("chartObject").GetComponent<Image>();
    }
    private void InitializeDataForGraph()
    {
        _questionDataListModelForRealm1 = _di.GetAllRealmQuestionForRealm(1);
        _questionDataListModelForRealm2 = _di.GetAllRealmQuestionForRealm(2);
        _questionDataListModelForRealm3 = _di.GetAllRealmQuestionForRealm(3);
    }
    private void AnimateGraph(int realm, float animationWaitTime)
    {

        _charImage.fillAmount += 1.0f / animationWaitTime * Time.deltaTime;
    }
    private float GetFillForGraph(List<QuestionData> listQuestionData)
    {
        int yesCount = 0;
        float fillAmount = 0f;

        foreach (QuestionData questionData in listQuestionData)
        {
            if (questionData.Answer == "S")
                yesCount += 1;
        }

        fillAmount = ((360 / listQuestionData.Count) * yesCount);
        return fillAmount;
    }
}
