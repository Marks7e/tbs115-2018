using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataType
{
    void SaveData(string key, string value);
    string GetData(string key);

}
