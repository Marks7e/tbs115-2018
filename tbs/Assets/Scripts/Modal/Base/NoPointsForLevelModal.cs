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

    private static NoPointsForLevelModal modalPanel;

    public static NoPointsForLevelModal Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(NoPointsForLevelModal)) as NoPointsForLevelModal;
            if (!modalPanel)
                Debug.LogError("There needs to be one active NoPointsForLevelModal script on a GameObject in your scene.");
        }
        return modalPanel;
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
