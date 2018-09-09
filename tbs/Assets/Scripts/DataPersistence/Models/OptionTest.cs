using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class OptionTest : IDataType
{
    public Dictionary<string, string> data = new Dictionary<string, string>();

    public string GetData(string key)
    {
        if (data.ContainsKey(key))
            return data[key].ToString();
        throw new ArgumentException("El Key solicitado no existe en el Dictionary correspondiente.", key);
    }

    public void SaveData(string key, string value)
    {
        data.Add(key, value);
    }
}
