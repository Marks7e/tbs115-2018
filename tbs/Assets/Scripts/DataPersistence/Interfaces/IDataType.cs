using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataType
{
    bool SaveDataLocally(string key, string value);
    string LoadDataLocally(string key);

}
