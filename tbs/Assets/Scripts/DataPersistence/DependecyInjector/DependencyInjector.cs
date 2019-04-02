using Assets.Scripts.DataPersistence.DataServices;
using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.DataPersistence.DBGenerator;

namespace Assets.Scripts.DataPersistence.DependecyInjector
{
    public class DependencyInjector
    {
        private DataBaseConnector _dbc = null;
        private PlayerDataService _pds = null;
        private LevelDataService _lds = null;
        private QuestionDataService _qds = null;
        private LevelSuccessTimeService _lst = null;
        private DBGenerator.DBGenerator _dbg = null;

        public DependencyInjector()
        {
            _dbc = new DataBaseConnector();
            _pds = new PlayerDataService(_dbc);
            _lds = new LevelDataService(_dbc);
            _qds = new QuestionDataService(_dbc);
            _lst = new LevelSuccessTimeService(_dbc);
            _dbg = new DBGenerator.DBGenerator(_dbc);
        }

        #region DatabaseGenerator

        public bool CreateDatabaseIfNotExist()
        {
            return _dbg.CreateDbIfNotExist();
        }

        #endregion

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
        public bool UpdateLevelTimesPlayed(int level)
        {
            return _lds.UpdateTimesPlayedForLevelID(level);
        }
        public int CalculateRoundTimeByDynamicGameBalancing(int level)
        {
            LevelData lvl = GetLevelData(level);
            List<LevelSuccessTime> lst = GetAllLevelSuccessTimeByLevel(level);
            DynamicGameBalance dgb = new DynamicGameBalance();
            return dgb.CalculateRoundTime(lvl.RoundTime, lst);
        }
        public int GetRoundTime(int level)
        {
            List<LevelSuccessTime> llst = GetAllLevelSuccessTimeByLevel(level);
            if (llst.Count >= 5)
            { return CalculateRoundTimeByDynamicGameBalancing(level); }
            return GetLevelData(level).RoundTime;
        }
        #endregion

        #region PlayerDataAndLevelData
        public bool UnlockGame(int level)
        {
            PlayerData pd = GetAllPlayerData();
            LevelData ld = GetAllLevelData().FirstOrDefault(l => l.LevelID == level);
            return ld.UnlockLevelAt <= pd.TotalScore;
        }
        #endregion

        #region QuestionData
        public List<QuestionData> GetAllQuestionData()
        {
            return _qds.GetAllQuestions();

        }
        public bool SaveAnswerForQuestion(IDataModel QuestionData)
        {
            return _qds.SaveDataToDB(QuestionData);
        }
        #endregion

        #region LevelSuccessTime
        public List<LevelSuccessTime> GetAllLevelSuccessTime()
        {
            return _lst.GetAllSuccessTimeFromDB();
        }
        public List<LevelSuccessTime> GetAllLevelSuccessTimeByLevel(int level)
        {
            return _lst.GetAllSuccessByLevel(level);
        }
        public bool SaveSuccesTime(IDataModel LevelSuccessTime)
        {
            return _lst.SavePerformanceForLevel(LevelSuccessTime);
        }
        public bool ResetLevelSuccessTimeByLevel(int level)
        {
            return _lst.DeleteLevelSuccessTimeByLevel(level);
        }
        #endregion

    }


}
