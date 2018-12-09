using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.Models
{
    [Serializable]
    public class PlayerAnswerData : IDataType
    {

        public Dictionary<string, string> data = new Dictionary<string, string>();
        GameDataPersistence gdp = new GameDataPersistence();


        public void SavePlayerAnswer(string question, string answer)
        {
            if (data.Select(d => d).Where(dr => dr.Key == question).Any())
            {
                data[question] = answer;
                gdp.SaveData(GameDataPersistence.DataType.PlayerData, this );
            }
            else
            {
                data.Add(question, answer);
                gdp.SaveData(GameDataPersistence.DataType.PlayerData, this);
            }

        }
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
}
