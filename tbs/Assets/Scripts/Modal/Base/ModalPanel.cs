using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class ModalPanel : MonoBehaviour
{
    public Text question;
    public Button yesButton;
    public Button noButton;
    public GameObject modalPanelObject;
    private SqliteConnection sql = null;

    private static ModalPanel modalPanel;

    public static ModalPanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel)
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
        }
        return modalPanel;
    }

    public void Choice(string question)
    {
        modalPanelObject.SetActive(true);

        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(YesFunction);
        yesButton.onClick.AddListener(ClosePanel);

        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(ClosePanel);

        this.question.text = question;

        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        modalPanelObject.SetActive(false);
    }

    public void YesFunction()
    {
        Debug.Log("Eliminando archivos .dat");
        //gdp.DeleteDatFiles();
        SceneManager.LoadScene("MainMenu");
    }
}
