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
        private QuestionDataService _qds = null;

        public DependencyInjector()
        {
            _dbc = new DataBaseConnector();
            _pds = new PlayerDataService(_dbc);
            _lds = new LevelDataService(_dbc);
            _qds = new QuestionDataService(_dbc);
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
        
        #region PlayerDataAndLevelData
        public bool UnlockGame(int level)
        {
            PlayerData pd = GetAllPlayerData();
            LevelData ld = GetAllLevelData().FirstOrDefault(l=>l.LevelID == level);

            return ld.UnlockLevelAt <= pd.TotalScore;
        }
        #endregion


        #region QuestionData
        public List<QuestionData> GetAllQuestionData()
        {
            return _qds.GetAllQuestions();

        }
        public bool SaveAnswerForQuestion(IDataModel data)
        {
            return _qds.SaveDataToDB(data);
        }
        #endregion

        #region ResetAllData

        #endregion





    }


}
