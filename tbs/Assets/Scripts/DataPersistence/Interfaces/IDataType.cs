using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataType
{
    void SaveDataLocally(string key, string value);
    string LoadDataLocally(string key);

}
