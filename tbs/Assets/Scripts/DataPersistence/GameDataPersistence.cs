using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameDataPersistence
{
    public object BynaryFormatter { get; private set; }


    #region Public Methods and Enums
    public enum DataType
    {

        GeneralGameData,
        PlayerData,
        PostGameTestData,
        RealmData
    }
    public bool SaveDataToFile(DataType type, IDataType data)
    {
        return Persist(type, data);
    }
    public IDataType LoadDataFromFile(DataType type)
    {

        if (!File.Exists(DataPath(type)))
            throw new Exception("Archivo de persistencia de datos no encontrado, ha sido declarado en el Enum de tipos de dato?");
        return GetDataFromFile(type);

    }
    public void DeleteDatFiles()
    {
        DeleteFiles(GetDatFiles(Application.persistentDataPath));
    }
    public bool VerifyIfDatFileExist(DataType dataType)
    {
        string dataPath = DataPath(dataType);
        return File.Exists(dataPath);
    }

    #endregion


    #region Private Methods
    private bool Persist(DataType type, IDataType data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(DataPath(type),FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
        try
        {
            bf.Serialize(file, data);
            file.Close();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }

    }
    private string DataPath(DataType type)
    { return Application.persistentDataPath + "/" + type.ToString() + ".dat"; }
    private IDataType GetDataFromFile(DataType type)
    {
        IDataType data = null;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(DataPath(type), FileMode.Open, FileAccess.Read, FileShare.Read);
                        
        data = (IDataType)bf.Deserialize(file);
        file.Close();
        
        return data;
    }
    private List<string> GetDatFiles(string path)
    {
        string[] datFilesArray = null;
        datFilesArray = Directory.GetFiles(path, "*.dat");

        if (datFilesArray.Length > 0)
            return new List<string>(datFilesArray);
        return null;
    }
    private void DeleteFiles(List<string> filesToDelete)
    {
        if (filesToDelete != null)
        {
            foreach (string file in filesToDelete)
            {
                File.Delete(file);
            }
        }

    }
    #endregion

}
