using Assets.Scripts.DataPersistence.Global;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject _minigame1;
    public GameObject _minigame2;
    public GameObject _minigame3;
    public GameObject _minigame4;
    public GameObject _minigame5;
    public GameObject _minigame6;
    public GameObject _minigame7;
    public GameObject _minigame8;
    public GameObject _minigame9;
    public GameObject _minigame10;
    public GameObject _minigame11;
    public GameObject _minigame12;
    public GameObject _m1_1;
    public GameObject _m1_2;
    public GameObject _m1_3;
    public GameObject _m2_1;
    public GameObject _m2_2;
    public GameObject _m2_3;
    public GameObject _m3_1;
    public GameObject _m3_2;
    public GameObject _m4_1;
    public GameObject _m4_2;
    public GameObject _m4_3;
    public GameObject _m5_1;
    public GameObject _m5_2;
    public GameObject _m5_3;
    public GameObject _m6_1;
    public GameObject _m6_2;
    public GameObject _m6_3;
    public GameObject _m7_1;
    public GameObject _m7_2;
    public GameObject _m7_3;
    public GameObject _m8_1;
    public GameObject _m8_2;
    public GameObject _m8_3;
    public GameObject _m9_1;
    public GameObject _m9_2;
    public GameObject _m9_3;
    public GameObject _m10_1;
    public GameObject _m10_2;
    public GameObject _m10_3;
    public GameObject _m11_1;
    public GameObject _m11_2;
    public GameObject _m11_3;
    public GameObject _m12_1;
    public GameObject _m12_2;
    public GameObject _m12_3;
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
        _minigame2 = GameObject.Find("Minijuego 2");
        _minigame3 = GameObject.Find("Minijuego 3");
        _minigame4 = GameObject.Find("Minijuego 4");
        _minigame5 = GameObject.Find("Minijuego 5");
        _minigame6 = GameObject.Find("Minijuego 6");
        _minigame7 = GameObject.Find("Minijuego 7");
        _minigame8 = GameObject.Find("Minijuego 8");
        _minigame9 = GameObject.Find("Minijuego 9");
        _minigame10 = GameObject.Find("Minijuego 10");
        _minigame11 = GameObject.Find("Minijuego 11");
        _minigame12 = GameObject.Find("Minijuego 12");

        _tutorialList = new List<GameObject>
        {
            _minigame1,
            _minigame2,
            _minigame3,
            _minigame4,
            _minigame5,
            _minigame6,
            _minigame7,
            _minigame8,
            _minigame9,
            _minigame10,
            _minigame11,
            _minigame12,
        };

        _m1_1 = GameObject.Find("m1_1");
        _m1_2 = GameObject.Find("m1_2");
        _m1_3 = GameObject.Find("m1_3");
        _m2_1 = GameObject.Find("m2_1");
        _m2_2 = GameObject.Find("m2_2");
        _m2_3 = GameObject.Find("m2_3");
        _m3_1 = GameObject.Find("m3_1");
        _m3_2 = GameObject.Find("m3_2");
        _m4_1 = GameObject.Find("m4_1");
        _m4_2 = GameObject.Find("m4_2");
        _m4_3 = GameObject.Find("m4_3");
        _m5_1 = GameObject.Find("m5_1");
        _m5_2 = GameObject.Find("m5_2");
        _m5_3 = GameObject.Find("m5_3");
        _m6_1 = GameObject.Find("m6_1");
        _m6_2 = GameObject.Find("m6_2");
        _m6_3 = GameObject.Find("m6_3");
        _m7_1 = GameObject.Find("m7_1");
        _m7_2 = GameObject.Find("m7_2");
        _m7_3 = GameObject.Find("m7_3");
        _m8_1 = GameObject.Find("m8_1");
        _m8_2 = GameObject.Find("m8_2");
        _m8_3 = GameObject.Find("m8_3");
        _m9_1 = GameObject.Find("m9_1");
        _m9_2 = GameObject.Find("m9_2");
        _m9_3 = GameObject.Find("m9_3");
        _m10_1 = GameObject.Find("m10_1");
        _m10_2 = GameObject.Find("m10_2");
        _m10_3 = GameObject.Find("m10_3");
        _m11_1 = GameObject.Find("m11_1");
        _m11_2 = GameObject.Find("m11_2");
        _m11_3 = GameObject.Find("m11_3");
        _m12_1 = GameObject.Find("m12_1");
        _m12_2 = GameObject.Find("m12_2");
        _m12_3 = GameObject.Find("m12_3");



        _minigameImages = new List<GameObject>
        {
            _m1_1,
            _m1_2,
            _m1_3,
            _m2_1,
            _m2_2,
            _m2_3,
            _m3_1,
            _m3_2,
            _m4_1,
            _m4_2,
            _m4_3,
            _m5_1,
            _m5_2,
            _m5_3,
            _m6_1,
            _m6_2,
            _m6_3,
            _m7_1,
            _m7_2,
            _m7_3,
            _m8_1,
            _m8_2,
            _m8_3,
            _m9_1,
            _m9_2,
            _m9_3,
            _m10_1,
            _m10_2,
            _m10_3,
            _m11_1,
            _m11_2,
            _m11_3,
            _m12_1,
            _m12_2,
            _m12_3
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
    private void EnableFirstTutorialImagen(string tutorialName)
    {
        string prefix = GetTutorialPrefixByRegExp(tutorialName);

        foreach (GameObject image in _minigameImages)
        {
            if (image.gameObject.name == prefix)
            {
                image.SetActive(true);
            }
            else
            {
                image.SetActive(false);
            }
        }
    }
    private string GetTutorialPrefixByRegExp(string tutorialName)
    {
        return "m" + tutorialName.Split(' ')[1] + "_1";
    }
}
