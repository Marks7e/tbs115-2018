using Assets.Scripts.DataPersistence.Global;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject _minigame1;
    public GameObject _minigame3;
    public GameObject _minigame4;
    public GameObject _m1_1;
    public GameObject _m1_2;
    public GameObject _m1_3;
    public GameObject _m3_1;
    public GameObject _m3_2;
    public GameObject _m4_1;
    public GameObject _m4_2;
    public GameObject _m4_3;
    public List<GameObject> _tutorialList;
    public List<GameObject> _minigameImages;

    void Start()
    {
        InitializeGameObjects();
        EnableTutorialByTutorialName(GlobalVariables.LoadTutorial);
    }
    public void InitializeGameObjects()
    {
        _minigame1 = GameObject.Find("Minijuego 1");
        _minigame3 = GameObject.Find("Minijuego 3");
        _minigame4 = GameObject.Find("Minijuego 4");

        _tutorialList = new List<GameObject>
        {
            _minigame1,
            _minigame3,
            _minigame4,
        };

        _m1_1 = GameObject.Find("m1_1");
        _m1_2 = GameObject.Find("m1_2");
        _m1_3 = GameObject.Find("m1_3");
        _m3_1 = GameObject.Find("m3_1");
        _m3_2 = GameObject.Find("m3_2");
        _m4_1 = GameObject.Find("m4_1");
        _m4_2 = GameObject.Find("m4_2");
        _m4_3 = GameObject.Find("m4_3");

        _minigameImages = new List<GameObject>
        {
            _m1_1,
            _m1_2,
            _m1_3,
            _m3_1,
            _m3_2,
            _m4_1,
            _m4_2,
            _m4_3
        };


    }
    public void EnableTutorialByTutorialName(string tutorialName)
    {
        foreach (GameObject tutorial in _tutorialList)
        {
            if (tutorial.gameObject.name == tutorialName)
            {
                tutorial.SetActive(true);
                EnableFirstTutorialImagen(tutorialName);
            }
            else
            {
                tutorial.SetActive(false);
            }
        }
    }

    private void EnableFirstTutorialImagen(string tutorialName)
    {
        string prefix = (tutorialName.Substring(0, 1)).ToLower() + tutorialName.Substring(tutorialName.Length - 1, 1);

        foreach (GameObject image in _minigameImages)
        {
            if (image.gameObject.name == prefix + "_1")
            {
                image.SetActive(true);
            }
            else
            {
                image.SetActive(false);
            }
        }
    }

    public void EnableTutorialImage(string imageName)
    {
        foreach (GameObject image in _minigameImages)
        {
            if (image.gameObject.name == imageName)
            {
                image.SetActive(true);
            }
            else
            {
                image.SetActive(false);
            }
        }
    }
}
