using Assets.Scripts.DataPersistence.DataServices;
using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.DependecyInjector
{
    public class DependencyInjector
    {
        private GameDataPersistence gdp = null;
        private PlayerDataService pds = null;
        
        public DependencyInjector()
        {
            gdp = new GameDataPersistence();

            pds = new PlayerDataService(gdp);
        }
        
        #region Methods of PlayerData Model
        public bool SaveTotalizedPlayerScore(int score)
        {
            return pds.SaveTotalizedScore(score);
        }
        public bool SaveBestScoreForLevel3(int score)
        {
            return pds.SaveBestScoreForLevel3(GeneralGameData.LevelNumber.Realm1Lvl3, score);
        }
        public int LoadPlayerScore()
        {
            return pds.GetTotalizedScore();
        }
        public int LoadPlayerBestScoreForLevel3()
        {
            return pds.GetBestScoreForLevel(GeneralGameData.LevelNumber.Realm1Lvl3);
        }
        public bool InitializeScoreForNewPlayer()
        {
            return pds.InitializeScoreForNewPlayer();
        }
        public bool SaveScoreForLevel3(int score)
        {
            SaveTotalizedPlayerScore(score);
            return SaveBestScoreForLevel3(score);



        }
        #endregion





    }
}
