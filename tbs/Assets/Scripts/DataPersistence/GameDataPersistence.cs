using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameDataPersistence
{
    public object BynaryFormatter { get; private set; }

    public enum DataType
    {
        OptionsData,
        PlayerData
    }
    public bool SaveData(DataType type, IDataType data)
    {
        return Persist(type, data);
    }

    public IDataType LoadData(DataType type)
    {
        try
        {
            if (!File.Exists(DataPath(type)))
                throw new Exception("Archivo de persistencia de datos no encontrado, ha sido declarado en el Enum de tipos de dato?");

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(DataPath(type), FileMode.Open);
            IDataType data = (IDataType)bf.Deserialize(fs);
            fs.Close();
            return data;

        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return null;
        }
    }

    private string DataPath(DataType type)
    { return Application.persistentDataPath + "/" + type.ToString() + ".dat"; }

    private bool Persist(DataType type, IDataType data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(DataPath(type), FileMode.OpenOrCreate);
        try
        {
            bf.Serialize(fs, data);
            fs.Close();
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            fs.Close();
            return false;
        }
    }
}
