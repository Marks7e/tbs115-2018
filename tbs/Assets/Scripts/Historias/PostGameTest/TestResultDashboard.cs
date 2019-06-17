using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Global;
using Assets.Scripts.DataPersistence.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestResultDashboard : MonoBehaviour
{
    private GameObject _chartObject;
    private GameObject _bgSound;
    private Button _familyRealmButton;
    private Button _schoolRealmButton;
    private Button _autoevaluationRealmButton;
    private Image _charImage;
    private GameObject _noDataMessage;
    private GameObject _percentProgressMessage;
    private Color _familyColorGraph;
    private Color _shoolColorGraph;
    private Color _autoEvaluationColorGraph;
    private float _waitTime = 0.10f;
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
        GetGeneralVolume();
    }
    void Update()
    {
        //_charImage.fillAmount += 1.0f / _waitTime * Time.deltaTime;
        if (_familyRealmAnimationFlag == 1)
        { AnimateGraph(AnimationFlag.FamilyAnimation); }
        if (_schoolRealmAnimationFlag == 1)
        { AnimateGraph(AnimationFlag.SchoolAnimation); }
        if (_autoevaluationRealmAnimationFlag == 1)
        { AnimateGraph(AnimationFlag.AutoEvalAnimation); }
    }
    private void InitializeAllObjects()
    {
        _di = new DependencyInjector();
        _questionDataListModelForRealm = new List<QuestionData>();

        _bgSound = GameObject.Find("bgSound");
        _charImage = GameObject.Find("chartObject").GetComponent<Image>();
        _noDataMessage = GameObject.Find("noDataMessage");
        _percentProgressMessage = GameObject.Find("percentProgressMessage");

        _familyRealmButton = GameObject.Find("showRealm1").GetComponent<Button>();
        _schoolRealmButton = GameObject.Find("showRealm2").GetComponent<Button>();
        _autoevaluationRealmButton = GameObject.Find("showRealm3").GetComponent<Button>();

        _familyColorGraph = new Color(0.7686275f, 0.1176471f, 0.2392157f, 1f);
        _shoolColorGraph = new Color(0.2431373f, 0.572549f, 0.8f, 1f);
        _autoEvaluationColorGraph = new Color(0.6941177f, 0.454902f, 0.05882353f, 1f);

        _familyRealmButton.onClick.AddListener(delegate { StartAnimation(AnimationFlag.FamilyAnimation); });
        _schoolRealmButton.onClick.AddListener(delegate { StartAnimation(AnimationFlag.SchoolAnimation); });
        _autoevaluationRealmButton.onClick.AddListener(delegate { StartAnimation(AnimationFlag.AutoEvalAnimation); });

        _noDataMessage.SetActive(false);
        _percentProgressMessage.SetActive(false);
    }
    private void InitializeDataForGraph(AnimationFlag animationFlag)
    {
        _questionDataListModelForRealm = _di.GetAllRealmQuestionForRealm((int)animationFlag);
    }
    private void AnimateGraph(AnimationFlag animationFlag)
    {
        if (IsGraphAnimationReadyToStart())
        {
            InitializeAnimationAndDataGraph(animationFlag);
        }
        else
        {
            RunAnimation();
        }

    }
    private bool IsGraphAnimationReadyToStart()
    { return fillUntil == 0 || _charImage.fillAmount >= fillUntil; }
    private void InitializeAnimationAndDataGraph(AnimationFlag animationFlag)
    {
        _charImage.fillAmount = 0;
        _noDataMessage.SetActive(false);
        _percentProgressMessage.SetActive(false);
        InitializeDataForGraph(animationFlag);
        fillUntil = GetFillForGraph(_questionDataListModelForRealm);
        if (fillUntil <= 0)
        {
            _noDataMessage.SetActive(true);
        }
    }
    private void DeltaAnimationForGraph()
    { _charImage.fillAmount += 0.1f / _waitTime * Time.deltaTime; }
    private bool IsGraphAnimationFinish()
    { return _charImage.fillAmount >= fillUntil; }
    private void StopAndClearAnimation()
    {
        _charImage.fillAmount = fillUntil;
        _percentProgressMessage.SetActive(true);
        _percentProgressMessage.GetComponent<Text>().text = (Math.Round(fillUntil * 100f,2)) + "%";
        fillUntil = 0f;
        DisableAllAnimationFlags();
    }
    private void RunAnimation()
    {
        DeltaAnimationForGraph();
        if (IsGraphAnimationFinish())
        {
            StopAndClearAnimation();
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

        fillAmount = (((360f / listQuestionData.Count) * yesCount) / 360f);
        return fillAmount;
    }
    private void StartAnimation(AnimationFlag animationFlag)
    {
        SetGraphColor(animationFlag);

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
    private void SetGraphColor(AnimationFlag animationFlag)
    {

        switch (animationFlag)
        {
            case AnimationFlag.FamilyAnimation:
                _charImage.color = _familyColorGraph;
                break;
            case AnimationFlag.SchoolAnimation:
                _charImage.color = _shoolColorGraph;
                break;
            case AnimationFlag.AutoEvalAnimation:
                _charImage.color = _autoEvaluationColorGraph;
                break;
        }
    }

    private void GetGeneralVolume()
    {
        float generalVolume = 0.0f;
        generalVolume = GlobalVariables.GeneralVolume;
        _bgSound.GetComponent<AudioSource>().volume = generalVolume;
    }

}
