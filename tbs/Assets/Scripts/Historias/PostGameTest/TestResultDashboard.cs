using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestResultDashboard : MonoBehaviour
{
    private GameObject _chartObject;
    private Button _familyRealmButton;
    private Button _schoolRealmButton;
    private Button _autoevaluationRealmButton;
    private Image _charImage;
    private float _waitTime = 1.5f;
    public float fillUntil = 0f;
    private List<QuestionData> _questionDataListModelForRealm;
    private DependencyInjector _di;
    private int _familyRealmAnimationFlag = 0;
    private int _schoolRealmAnimationFlag = 0;
    private int _autoevaluationRealmAnimationFlag = 0;

    enum AnimationFlag
    {
        FamilyAnimation = 1,
        SchoolAnimation = 2,
        AutoEvalAnimation = 3
    }

    void Start()
    {
        InitializeAllObjects();
    }
    void Update()
    {
        //_charImage.fillAmount += 1.0f / _waitTime * Time.deltaTime;
        if (_familyRealmAnimationFlag == 1)
        { AnimateGraph((int)AnimationFlag.FamilyAnimation); }
        if (_schoolRealmAnimationFlag == 1)
        { AnimateGraph((int)AnimationFlag.SchoolAnimation); }
        if (_autoevaluationRealmAnimationFlag == 1)
        { AnimateGraph((int)AnimationFlag.AutoEvalAnimation); }
    }
    private void InitializeAllObjects()
    {
        _di = new DependencyInjector();
        _questionDataListModelForRealm = new List<QuestionData>();

        _charImage = GameObject.Find("chartObject").GetComponent<Image>();
        _familyRealmButton = GameObject.Find("showRealm1").GetComponent<Button>();
        _schoolRealmButton = GameObject.Find("showRealm2").GetComponent<Button>();
        _autoevaluationRealmButton = GameObject.Find("showRealm3").GetComponent<Button>();

        _familyRealmButton.onClick.AddListener(delegate { EnableAnimationFlag(AnimationFlag.FamilyAnimation); });
        _familyRealmButton.onClick.AddListener(delegate { EnableAnimationFlag(AnimationFlag.SchoolAnimation); });
        _familyRealmButton.onClick.AddListener(delegate { EnableAnimationFlag(AnimationFlag.AutoEvalAnimation); });
    }
    private void InitializeDataForGraph(int realm)
    {
        _questionDataListModelForRealm = _di.GetAllRealmQuestionForRealm(realm);
    }
    private void AnimateGraph(int realm)
    {
        if (fillUntil == 0 || _charImage.fillAmount >= fillUntil)
        {
            _charImage.fillAmount = 0;
            InitializeDataForGraph(realm);
            fillUntil = GetFillForGraph(_questionDataListModelForRealm);
        }
        else
        {
            _charImage.fillAmount += 1.0f / _waitTime * Time.deltaTime;
            if (_charImage.fillAmount >= fillUntil)
            {
                fillUntil = 0f;
                DisableAllAnimationFlags();
            }
        }

    }
    private float GetFillForGraph(List<QuestionData> listQuestionData)
    {
        int yesCount = 0;
        float fillAmount;

        foreach (QuestionData questionData in listQuestionData)
        {
            if (questionData.Answer == "S")
                yesCount += 1;
        }

        fillAmount = (((360 / listQuestionData.Count) * yesCount) * 0.01f);
        return fillAmount;
    }
    private void EnableAnimationFlag(AnimationFlag animationFlag)
    {
        switch (animationFlag)
        {
            case AnimationFlag.FamilyAnimation:
                _familyRealmAnimationFlag = 1;
                break;
            case AnimationFlag.SchoolAnimation:
                _schoolRealmAnimationFlag = 1;
                break;
            case AnimationFlag.AutoEvalAnimation:
                _autoevaluationRealmAnimationFlag = 1;
                break;
        }
    }
    private void DisableAllAnimationFlags()
    {
        _familyRealmAnimationFlag = 0;
        _schoolRealmAnimationFlag = 0;
        _autoevaluationRealmAnimationFlag = 0;
    }
}
