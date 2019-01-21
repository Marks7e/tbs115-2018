using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.DataPersistence.DataServices
{
    public class PlayerDataService : IDataService
    {
        /*Declaración de Strings Finales*/
        private const string TOTALIZED_SCORE = "TotalizedScore";
        private const string BEST_SCORE_FOR_LEVEL = "BestScoreForGame";

        private GameDataPersistence gdp = null;
        private PlayerData pd = null;

        public PlayerDataService(GameDataPersistence gdp)
        {
            this.gdp = gdp;
            pd = new PlayerData();
            LoadDataFromFile();
        }

        public bool SaveDataToFile()
        {
            try
            {
                return gdp.SaveDataToFile(GameDataPersistence.DataType.PlayerData, pd);
            }
            catch (Exception e)
            {
                Debug.LogError("Error en PlayerDataService:" + e.Message);
                return false;
            }
        }

        public bool SaveBestScoreForLevel3(GeneralGameData.LevelNumber level, int score)
        {
            return SaveBestScoreForLevel(level, score);
        }

        public bool LoadDataFromFile()
        {
            if (!gdp.VerifyIfDatFileExist(GameDataPersistence.DataType.PlayerData))
            { return SaveDataToFile(); }
            else
            {
                pd = (PlayerData)gdp.LoadDataFromFile(GameDataPersistence.DataType.PlayerData);
                return pd != null ? true : false;
            }

        }

        public bool SaveTotalizedScore(int score)
        {
            int totalizedScore = 0;
            if (pd.data.ContainsKey(TOTALIZED_SCORE))
            {
                totalizedScore = GetTotalizedScore();
                pd.SaveDataLocally(TOTALIZED_SCORE, (totalizedScore + score).ToString());
            }
            else
            {
                pd.SaveDataLocally(TOTALIZED_SCORE, "0");
            }
            return gdp.SaveDataToFile(GameDataPersistence.DataType.RealmData, pd);
        }
        public bool SaveBestScoreForLevel(GeneralGameData.LevelNumber level, int score)
        {
            int lscore = score;
            int lbestScore = 0;

            if (!pd.data.ContainsKey(BEST_SCORE_FOR_LEVEL + level))
            {
                pd.SaveDataLocally(BEST_SCORE_FOR_LEVEL + level, score.ToString());
                SaveDataToFile();
            }


            lbestScore = int.Parse(pd.LoadDataLocally(BEST_SCORE_FOR_LEVEL + level));
            if (lbestScore < lscore)
            {
                pd.SaveDataLocally(BEST_SCORE_FOR_LEVEL + level, score.ToString());
                SaveDataToFile();
            }
            return true;
        }
        public int GetTotalizedScore()
        {
            int totalizedScore = 0;
            bool successParse = false;
            successParse = int.TryParse(pd.LoadDataLocally(TOTALIZED_SCORE), out totalizedScore);

            if (successParse)
                return totalizedScore;
            return -1;
        }
        public int GetBestScoreForLevel(GeneralGameData.LevelNumber level)
        {
            if (pd.data.ContainsKey(BEST_SCORE_FOR_LEVEL + level))
            { return int.Parse(pd.LoadDataLocally(BEST_SCORE_FOR_LEVEL + level)); }
            else
            {
                pd.SaveDataLocally(BEST_SCORE_FOR_LEVEL + level, "0");
                SaveDataToFile();
                return 0;
            }

        }
        public bool InitializeScoreForNewPlayer()
        {
            if (!gdp.VerifyIfDatFileExist(GameDataPersistence.DataType.RealmData))
            {
                SaveBestScoreForLevel(GeneralGameData.LevelNumber.Realm1Lvl1, 0);
                SaveBestScoreForLevel(GeneralGameData.LevelNumber.Realm1Lvl2, 0);
                SaveBestScoreForLevel(GeneralGameData.LevelNumber.Realm1Lvl3, 0);
                SaveBestScoreForLevel(GeneralGameData.LevelNumber.Realm1Lvl4, 0);
                SaveTotalizedScore(0);
                return true;
            }
            return false;

        }





    }
}

