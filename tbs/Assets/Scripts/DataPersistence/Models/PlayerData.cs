using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.Models
{
    [Serializable]
    public class PlayerData : IDataType
    {
        #region Variable Declaration & Initialization
        /*Declaración de Strings Finales*/
        private const string TOTALIZED_SCORE = "TotalizedScore";
        private const string BEST_SCORE_FOR_LEVEL = "BestScoreForGame";

        /*Objetos Públicos*/
        public Dictionary<string, string> data = null;
        /*Objectos Privados*/
        private GameDataPersistence gdp = null;
        private GeneralGameData ggd = null;

        /*Constructor...*/
        public PlayerData()
        {
            data = new Dictionary<string, string>();
            gdp = new GameDataPersistence();
        }
        #endregion


        #region Public Methods
        public void SaveData(string key, string value)
        {
            data.Add(key, value);
        }
        public string GetData(string key)
        {
            if (data.ContainsKey(key))
                return data[key].ToString();
            throw new ArgumentException("El Key solicitado no existe en el Dictionary correspondiente.", key);
        }
        public void SavePlayerAnswer(string question, string answer)
        {
            if (data.Select(d => d).Where(dr => dr.Key == question).Any())
            {
                data[question] = answer;
                gdp.SaveData(GameDataPersistence.DataType.PlayerData, this);
            }
            else
            {
                data.Add(question, answer);
                gdp.SaveData(GameDataPersistence.DataType.PlayerData, this);
            }

        }
        public void SaveTotalizedScore(int score)
        {
            int totalizedScore = 0;
            if(VerifyIfKeyExist(TOTALIZED_SCORE))
            {
                totalizedScore = GetTotalizedScore();
                SaveData(TOTALIZED_SCORE, (totalizedScore + score).ToString());
            }
            else
            {
                SaveData(TOTALIZED_SCORE, "0" );
            }
            
            gdp.SaveData(GameDataPersistence.DataType.RealmData, this);
        }
        public void SaveBestScoreForLevel(GeneralGameData.LevelNumber level, int score)
        {
            SaveData(BEST_SCORE_FOR_LEVEL + level, score.ToString());
            gdp.SaveData(GameDataPersistence.DataType.PlayerData,this);

        }
        public int GetTotalizedScore()
        {
            int totalizedScore = 0;
            bool successParse = false;
            successParse = int.TryParse(GetData(TOTALIZED_SCORE), out totalizedScore);

            if (successParse)
                return totalizedScore;
            return -1;
        }
        public int GetPlayerBestScoreForLevel(string levelNumber)
        {
            int BestScore = 0;
            bool successParse = false;
            successParse = int.TryParse(GetData(BEST_SCORE_FOR_LEVEL + levelNumber), out BestScore);

            if (successParse)
                return BestScore;
            return -1;


        }
        public void InitializeScoreForNewPlayer()
        {
            if (!gdp.VerifyIfDatFileExist(GameDataPersistence.DataType.RealmData))
            {
                SaveBestScoreForLevel(GeneralGameData.LevelNumber.Realm1Lvl1, 0);
                SaveBestScoreForLevel(GeneralGameData.LevelNumber.Realm1Lvl2, 0);
                SaveBestScoreForLevel(GeneralGameData.LevelNumber.Realm1Lvl3, 0);
                SaveBestScoreForLevel(GeneralGameData.LevelNumber.Realm1Lvl4, 0);
                SaveTotalizedScore(0);
            }

        }
        #endregion

        #region Private Methods
        private void PersistToFile(GameDataPersistence.DataType type, IDataType data)
        {
            gdp.SaveData(type, data);
        }
        private IDataType GetPersistenceData()
        {
            return gdp.LoadData(GameDataPersistence.DataType.PlayerData);
        }
        private bool VerifyIfKeyExist(string key)
        {
            return data.ContainsKey(key);
        }
        #endregion

    }
}
