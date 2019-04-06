using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoPointsForLevelModal : MonoBehaviour {

    public Text question;
    public Button okButton;
    public GameObject NoPointsForLevelModalObject;

    private static NoPointsForLevelModal _modalPanel;

    public static NoPointsForLevelModal Instance()
    {
        if (!_modalPanel)
        {
            _modalPanel = FindObjectOfType(typeof(NoPointsForLevelModal)) as NoPointsForLevelModal;
            if (!_modalPanel)
                Debug.LogError("There needs to be one active NoPointsForLevelModal script on a GameObject in your scene.");
        }
        return _modalPanel;
    }

    public void Choice(string question)
    {
        NoPointsForLevelModalObject.SetActive(true);

        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(ClosePanel);

        this.question.text = question;

        okButton.gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        NoPointsForLevelModalObject.SetActive(false);
    }

}
