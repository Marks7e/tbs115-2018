using Assets.Scripts.DataPersistence.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public PlayerData pd;
    GameObject PrincipalMenu;
    GameObject OptionsMenu;
    GameObject ExtrasMenu;
    GameObject RealmsMenu;
    GameObject StoriesMenu;
    GameObject FamiliaMenu;
    GameObject EscuelaMenu;
    GameObject AutoevaluacionMenu;
    GameObject FerrejeMenu;
    GameObject BanMenu;
    GameObject KanslorMenu;
    GameObject ModalPanel;
    GameObject NoPointsModalPanel;
    Slider VolumeSlider;
    AudioSource MainAudio;

    void Start()
    {
        PrincipalMenu = GameObject.Find("PrincipalMenu");
        OptionsMenu = GameObject.Find("OptionsMenu");
        ExtrasMenu = GameObject.Find("ExtrasMenu");
        RealmsMenu = GameObject.Find("RealmsMenu");
        StoriesMenu = GameObject.Find("StoriesMenu");
        FamiliaMenu = GameObject.Find("FamiliaMenu");
        EscuelaMenu = GameObject.Find("EscuelaMenu");
        AutoevaluacionMenu = GameObject.Find("AutoevaluacionMenu");
        FerrejeMenu = GameObject.Find("FerrejeMenu");
        BanMenu = GameObject.Find("BanMenu");
        KanslorMenu = GameObject.Find("KanslorMenu");
        ModalPanel = GameObject.Find("Modal Panel");
        NoPointsModalPanel = GameObject.Find("NoPointsModalPanel");
        VolumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        MainAudio = Camera.main.GetComponent<AudioSource>();

        VolumeSlider.value = 1.0f;
        VolumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        PrincipalMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        ExtrasMenu.SetActive(false);
        RealmsMenu.SetActive(false);
        StoriesMenu.SetActive(false);
        FamiliaMenu.SetActive(false);
        EscuelaMenu.SetActive(false);
        AutoevaluacionMenu.SetActive(false);
        FerrejeMenu.SetActive(false);
        BanMenu.SetActive(false);
        KanslorMenu.SetActive(false);
        ModalPanel.SetActive(false);
        NoPointsModalPanel.SetActive(false);

        pd = new PlayerData();
        pd.InitializeScoreForNewPlayer();



    }
    public void ValueChangeCheck()
    {
        MainAudio.volume = VolumeSlider.value;
        Debug.Log(MainAudio.volume);
    }
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        PrincipalMenu.SetActive(false);
        RealmsMenu.SetActive(true);
    }
    public void ExitGame()
    {
        Debug.Log("Exiting Game... Editor does not support QUIT event!");
        Application.Quit();
    }
    public void Options()
    {
        PrincipalMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        Debug.Log("Option button Clicked!");
    }
    public void OptionsGoBack()
    {
        PrincipalMenu.SetActive(true);
        OptionsMenu.SetActive(false);

        Debug.Log("Backing to PrincipalMenu...");
    }
    public void DeleteGameFiles()
    {
        ShowModal sw = new ShowModal();
        sw.SendDeleteModalToView();
    }
    public void Extras()
    {
        //SceneManager.LoadScene ("ExtrasMenu");
        PrincipalMenu.SetActive(false);
        ExtrasMenu.SetActive(true);
        Debug.Log("Extras Button Clicked!");
    }
    public void Stories()
    {
        ExtrasMenu.SetActive(false);
        StoriesMenu.SetActive(true);
    }
    public void Familia()
    {
        StoriesMenu.SetActive(false);
        FamiliaMenu.SetActive(true);
    }
    public void Escuela()
    {
        StoriesMenu.SetActive(false);
        EscuelaMenu.SetActive(true);
    }
    public void AutoEva()
    {
        StoriesMenu.SetActive(false);
        AutoevaluacionMenu.SetActive(true);
    }
    public void Ferreje()
    {
        RealmsMenu.SetActive(false);
        FerrejeMenu.SetActive(true);
    }
    public void Ban()
    {
        RealmsMenu.SetActive(false);
        BanMenu.SetActive(true);
    }
    public void Kanslor()
    {
        RealmsMenu.SetActive(false);
        KanslorMenu.SetActive(true);
    }
    public void MainGoBack(string option)
    {
        PrincipalMenu.SetActive(true);

        switch (option)
        {
            case "Extras":
                ExtrasMenu.SetActive(false);
                break;
            case "Options":
                OptionsMenu.SetActive(false);
                break;
            case "Realms":
                RealmsMenu.SetActive(false);
                break;
        }
    }
    public void ExtrasGoBack(string option)
    {
        ExtrasMenu.SetActive(true);

        switch (option)
        {
            case "Stories":
                StoriesMenu.SetActive(false);
                break;
            case "Test":
                break;
        }
    }
    public void StoriesGoBack(string option)
    {
        StoriesMenu.SetActive(true);

        switch (option)
        {
            case "Familia":
                FamiliaMenu.SetActive(false);
                break;
            case "Escuela":
                EscuelaMenu.SetActive(false);
                break;
            case "Autoevaluacion":
                AutoevaluacionMenu.SetActive(false);
                break;
        }
    }
    public void RealmsGoBack(string option)
    {
        RealmsMenu.SetActive(true);

        switch (option)
        {
            case "Ferreje":
                FerrejeMenu.SetActive(false);
                break;
            case "Ban":
                BanMenu.SetActive(false);
                break;
            case "Kanslor":
                KanslorMenu.SetActive(false);
                break;
        }
    }
}

