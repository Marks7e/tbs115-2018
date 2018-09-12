using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    GameObject PrincipalMenu;
    GameObject OptionsMenu;


    void Start()
    {
        PrincipalMenu = GameObject.Find("PrincipalMenu");
        OptionsMenu = GameObject.Find("OptionsMenu");

        PrincipalMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

	public void Extras()
	{
		SceneManager.LoadScene ("ExtrasMenu");
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


}

