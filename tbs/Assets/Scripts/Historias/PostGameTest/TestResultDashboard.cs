using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestResultDashboard : MonoBehaviour
{
    private GameObject _chartObject;
    private Image _charImage;
    private float _waitTime = 1.5f;

    void Start()
    {
        InitializeAllObjects();
    }
    void Update()
    {
        AnimateGraph();
    }
    private void InitializeAllObjects()
    {
        _charImage = GameObject.Find("chartObject").GetComponent<Image>();
    }
    private void AnimateGraph()
    {
        _charImage.fillAmount += 1.0f / _waitTime * Time.deltaTime;
    }
}
