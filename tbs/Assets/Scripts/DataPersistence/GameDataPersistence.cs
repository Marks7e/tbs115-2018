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
        PlayerData,
        PostGameTestData,
        LevelData
    }
    public bool SaveData(DataType type, IDataType data)
    {
        return Persist(type, data);
    }
    public IDataType LoadData(DataType type)
    {

        if (!File.Exists(DataPath(type)))
            throw new Exception("Archivo de persistencia de datos no encontrado, ha sido declarado en el Enum de tipos de dato?");
        return GetDataFromFile(type);

    }

    private bool Persist(DataType type, IDataType data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fs = File.Open(DataPath(type),
                        FileMode.OpenOrCreate,
                        FileAccess.ReadWrite,
                        FileShare.ReadWrite))
        {
            try
            {
                bf.Serialize(fs, data);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
    private string DataPath(DataType type)
    { return Application.persistentDataPath + "/" + type.ToString() + ".dat"; }
    private IDataType GetDataFromFile(DataType type)
    {
        BinaryFormatter bf = new BinaryFormatter();
        IDataType data = null;
        using (FileStream fs = File.Open(DataPath(type),
                        FileMode.OpenOrCreate,
                        FileAccess.ReadWrite,
                        FileShare.ReadWrite))
        {
            data = (IDataType)bf.Deserialize(fs);
        }
        return data;
    }
}
