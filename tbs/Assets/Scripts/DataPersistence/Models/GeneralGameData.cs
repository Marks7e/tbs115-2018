using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.DataPersistence.Models
{
    [Serializable]
    public class GeneralGameData : IDataType
    {

        #region Variable Declaration & Initialization
        public Dictionary<string, string> data = null;
        /*Objectos Privados*/
        private GameDataPersistence gdp = null;

        public enum LevelNumber
        {
            TotalizedScore = 0,
            Realm1Lvl1 = 1,
            Realm1Lvl2 = 2,
            Realm1Lvl3 = 3,
            Realm1Lvl4 = 4
        }

        public GeneralGameData()
        {
            data = new Dictionary<string, string>();
            gdp = new GameDataPersistence();
        }
        #endregion

        #region Public Methods
        public string LoadDataLocally(string key)
        {
            if (data.ContainsKey(key))
                return data[key].ToString();
            throw new ArgumentException("El Key solicitado no existe en el Dictionary correspondiente.", key);
        }
        public bool SaveDataLocally(string key, string value)
        {
            try
            {
                if (!data.ContainsKey(key))
                {
                    data.Add(key, value);
                    return true;
                }
                else
                {
                    data[key] = value;
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
        }
        #endregion

    }
}
