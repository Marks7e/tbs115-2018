using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.DataPersistence.Models
{
    [Serializable]
    public class RealmData : IDataType
    {
        private Dictionary<string, string> data = new Dictionary<string, string>();
        
        public string LoadDataLocally(string key)
        {
            if (data.ContainsKey(key))
                return data[key];
            return null;
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
    }
}
