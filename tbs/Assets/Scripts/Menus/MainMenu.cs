using Assets.Scripts.DataPersistence;
using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public PlayerData playerDataModel;
    private DependencyInjector _dependencyInjector;
    GameObject principalMenu;
    GameObject optionMenu;
    GameObject extrasMenu;
    GameObject realmsMenu;
    GameObject storiesMenu;
    GameObject familyMenu;
    GameObject schoolMenu;
    GameObject AutoevaluationMenu;
    GameObject ferrejeMenu;
    GameObject banMenu;
    GameObject kanslorMenu;
    GameObject modalPanel;
    GameObject noPointsModalPanel;
    Slider volumeSlider;
    AudioSource mainAudio;

    void Start()
    {
        _dependencyInjector = new DependencyInjector();
        _dependencyInjector.CreateDatabaseIfNotExist();
        principalMenu = GameObject.Find("PrincipalMenu");
        optionMenu = GameObject.Find("OptionsMenu");
        extrasMenu = GameObject.Find("ExtrasMenu");
        realmsMenu = GameObject.Find("RealmsMenu");
        storiesMenu = GameObject.Find("StoriesMenu");
        familyMenu = GameObject.Find("FamiliaMenu");
        schoolMenu = GameObject.Find("EscuelaMenu");
        AutoevaluationMenu = GameObject.Find("AutoevaluacionMenu");
        ferrejeMenu = GameObject.Find("FerrejeMenu");
        banMenu = GameObject.Find("BanMenu");
        kanslorMenu = GameObject.Find("KanslorMenu");
        modalPanel = GameObject.Find("Modal Panel");
        noPointsModalPanel = GameObject.Find("NoPointsModalPanel");
        volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        mainAudio = Camera.main.GetComponent<AudioSource>();

        GameOptions gameOptions = new GameOptions();
        gameOptions = _dependencyInjector.LoadGameOptionById(Assets.Scripts.DataPersistence.DataServices.GameOptionsService.GameSettings.Volume);

        mainAudio.volume = float.Parse(gameOptions.PValue);
        mainAudio.PlayScheduled(0);
        volumeSlider.value = float.Parse(gameOptions.PValue);
        volumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        principalMenu.SetActive(true);
        optionMenu.SetActive(false);
        extrasMenu.SetActive(false);
        realmsMenu.SetActive(false);
        storiesMenu.SetActive(false);
        familyMenu.SetActive(false);
        schoolMenu.SetActive(false);
        AutoevaluationMenu.SetActive(false);
        ferrejeMenu.SetActive(false);
        banMenu.SetActive(false);
        kanslorMenu.SetActive(false);
        modalPanel.SetActive(false);
        noPointsModalPanel.SetActive(false);

    }
    public void ValueChangeCheck()
    {
        mainAudio.volume = volumeSlider.value;
        //Debug.Log(mainAudio.volume);
    }
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        principalMenu.SetActive(false);
        realmsMenu.SetActive(true);
    }
    public void ExitGame()
    {
        //Debug.Log("Exiting Game... Editor does not support QUIT event!");
        Application.Quit();
    }
    public void Options()
    {
        principalMenu.SetActive(false);
        optionMenu.SetActive(true);
       //Debug.Log("Option button Clicked!");
    }
    public void OptionsGoBack()
    {
        principalMenu.SetActive(true);
        optionMenu.SetActive(false);

        //Debug.Log("Backing to PrincipalMenu...");
    }
    public void DeleteGameFiles()
    {
        ShowModal sw = new ShowModal();
        sw.SendDeleteModalToView();
    }
    public void Extras()
    {
        //SceneManager.LoadScene ("ExtrasMenu");
        principalMenu.SetActive(false);
        extrasMenu.SetActive(true);
        //Debug.Log("Extras Button Clicked!");
    }
    public void Stories()
    {
        extrasMenu.SetActive(false);
        storiesMenu.SetActive(true);
    }
    public void Familia()
    {
        storiesMenu.SetActive(false);
        familyMenu.SetActive(true);
    }
    public void Escuela()
    {
        storiesMenu.SetActive(false);
        schoolMenu.SetActive(true);
    }
    public void AutoEva()
    {
        storiesMenu.SetActive(false);
        AutoevaluationMenu.SetActive(true);
    }
    public void Ferreje()
    {
        realmsMenu.SetActive(false);
        ferrejeMenu.SetActive(true);
    }
    public void Ban()
    {
        realmsMenu.SetActive(false);
        banMenu.SetActive(true);
    }
    public void Kanslor()
    {
        realmsMenu.SetActive(false);
        kanslorMenu.SetActive(true);
    }
    public void MainGoBack(string option)
    {
        principalMenu.SetActive(true);

        switch (option)
        {
            case "Extras":
                extrasMenu.SetActive(false);
                break;
            case "Options":
                SaveOptionsBeforeExit();
                optionMenu.SetActive(false);
                break;
            case "Realms":
                realmsMenu.SetActive(false);
                break;
        }
    }
    public void ExtrasGoBack(string option)
    {
        extrasMenu.SetActive(true);

        switch (option)
        {
            case "Stories":
                storiesMenu.SetActive(false);
                break;
            case "Test":
                break;
        }
    }
    public void StoriesGoBack(string option)
    {
        storiesMenu.SetActive(true);

        switch (option)
        {
            case "Familia":
                familyMenu.SetActive(false);
                break;
            case "Escuela":
                schoolMenu.SetActive(false);
                break;
            case "Autoevaluacion":
                AutoevaluationMenu.SetActive(false);
                break;
        }
    }
    public void RealmsGoBack(string option)
    {
        realmsMenu.SetActive(true);

        switch (option)
        {
            case "Ferreje":
                ferrejeMenu.SetActive(false);
                break;
            case "Ban":
                banMenu.SetActive(false);
                break;
            case "Kanslor":
                kanslorMenu.SetActive(false);
                break;
        }
    }

    private void SaveOptionsBeforeExit()
    {

    }
}


