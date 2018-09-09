using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceTEST : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameDataPersistence gameDataPersistence = new GameDataPersistence();
        var data = gameDataPersistence.LoadData(GameDataPersistence.DataType.OptionsData);
        Debug.Log("El valor del Slider es :" + data.GetData("slider"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
