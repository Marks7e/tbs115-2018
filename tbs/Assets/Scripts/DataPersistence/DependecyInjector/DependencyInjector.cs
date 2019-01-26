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
        private DataBaseConnector _dbc = null;

        private PlayerDataService _pds = null;
        private LevelDataService _lds = null;

        public DependencyInjector()
        {
            _dbc = new DataBaseConnector();
            _pds = new PlayerDataService(_dbc);
            _lds = new LevelDataService(_dbc);
        }

        #region PlayerData
        public PlayerData GetAllPlayerData()
        {
            return _pds.GetPlayerData();
        }
        public bool UpdateTotalizedScore(int Score)
        {
            PlayerData pd = new PlayerData();
            pd.TotalScore = Score;

            return _pds.SaveDataToDB(pd);
        }
        #endregion

        #region LevelData
        public List<LevelData> GetAllLevelData()
        {
            return _lds.GetAllLevelData();
        }
        public LevelData GetLevelData(int level)
        {
            return _lds.GetLevelData(level);
        }
        public bool UpdateBestScoreForLevel(int level, int bestScore)
        {
            LevelData ld = new LevelData();
            ld.LevelID = level;
            ld.BestScore = bestScore;
            return _lds.SaveDataToDB(ld);
        }
        #endregion





    }


}
