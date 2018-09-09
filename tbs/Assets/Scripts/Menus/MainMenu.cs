using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    /*Declarando "contenedores" de UI*/
    GameObject PrincipalMenu;
    GameObject OptionsMenu;
    Slider TestSlider;

    /*Agregando Persistencia para Test*/
    GameDataPersistence gameDataPersistence;


    void Start()
    {
        PrincipalMenu = GameObject.Find("PrincipalMenu");
        OptionsMenu = GameObject.Find("OptionsMenu");
        TestSlider = GameObject.Find("TestSlider").GetComponent<Slider>();
        
        PrincipalMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        
        Debug.Log("Slider Value: "+TestSlider.value);

        gameDataPersistence = new GameDataPersistence();

        OptionTest ot = new OptionTest();
        ot.SaveData("slider", TestSlider.value.ToString());
        gameDataPersistence.SaveData(GameDataPersistence.DataType.OptionsData, ot);
        

        Debug.Log("Saving Data...");
        Debug.Log("Backing to PrincipalMenu...");


    }


}

