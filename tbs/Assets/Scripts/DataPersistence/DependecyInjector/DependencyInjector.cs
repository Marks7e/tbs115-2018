using Assets.Scripts.DataPersistence.DataServices;
using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DataPersistence.DependecyInjector
{
    public class DependencyInjector
    {
        private DataBaseConnector _dataBaseConnector = null;
        private PlayerDataService _playerDataService = null;
        private LevelDataService _levelDataService = null;
        private QuestionDataService _questionDataService = null;
        private LevelSuccessTimeService _levelSuccessTimeService = null;
        private DBGenerator.DBGenerator _databaseGeneratorService = null;

        public DependencyInjector()
        {
            _dataBaseConnector = new DataBaseConnector();
            _playerDataService = new PlayerDataService(_dataBaseConnector);
            _levelDataService = new LevelDataService(_dataBaseConnector);
            _questionDataService = new QuestionDataService(_dataBaseConnector);
            _levelSuccessTimeService = new LevelSuccessTimeService(_dataBaseConnector);
            _databaseGeneratorService = new DBGenerator.DBGenerator(_dataBaseConnector);
        }

        #region DatabaseGenerator

        public bool CreateDatabaseIfNotExist()
        {
            return _databaseGeneratorService.CreateDbIfNotExist();
        }

        #endregion

        #region PlayerData
        public PlayerData GetAllPlayerData()
        {
            return _playerDataService.GetPlayerData();
        }
        public bool UpdateTotalizedScore(int score)
        {
            PlayerData playerDataModel = new PlayerData();
            playerDataModel.TotalScore = score;

            return _playerDataService.SaveDataToDb(playerDataModel);
        }
        #endregion

        #region LevelData
        public List<LevelData> GetAllLevelData()
        {
            return _levelDataService.GetAllLevelData();
        }
        public LevelData GetLevelData(int level)
        {
            return _levelDataService.GetLevelData(level);
        }
        public bool UpdateBestScoreForLevel(int level, int bestScore)
        {
            LevelData levelDataModel = new LevelData();
            levelDataModel.LevelId = level;
            levelDataModel.BestScore = bestScore;
            return _levelDataService.SaveDataToDb(levelDataModel);
        }
        public bool UpdateLevelTimesPlayed(int level)
        {
            return _levelDataService.UpdateTimesPlayedForLevelId(level);
        }
        public int CalculateRoundTimeByDynamicGameBalancing(int level)
        {
            LevelData levelDataModel = GetLevelData(level);
            List<LevelSuccessTime> levelSuccessTimeListModel = GetAllLevelSuccessTimeByLevel(level);
            DynamicGameBalance dynamicGameBalance = new DynamicGameBalance();
            return dynamicGameBalance.CalculateRoundTime(levelDataModel.RoundTime, levelSuccessTimeListModel);
        }
        public int GetRoundTime(int level)
        {
            List<LevelSuccessTime> levelSuccessTimeListModel = GetAllLevelSuccessTimeByLevel(level);
            if (levelSuccessTimeListModel.Count >= 5)
            { return CalculateRoundTimeByDynamicGameBalancing(level); }
            return GetLevelData(level).RoundTime;
        }
        #endregion

        #region PlayerDataAndLevelData
        public bool UnlockGame(int level)
        {
            PlayerData playerDataModel = GetAllPlayerData();
            LevelData levelDataModel = GetAllLevelData().FirstOrDefault(l => l.LevelId == level);
            return levelDataModel.UnlockLevelAt <= playerDataModel.TotalScore;
        }
        #endregion

        #region QuestionData
        public List<QuestionData> GetAllQuestionData()
        {
            return _questionDataService.GetAllQuestions();

        }
        public bool SaveAnswerForQuestion(IDataModel dataModel)
        {
            return _questionDataService.SaveDataToDb(dataModel);
        }
        #endregion

        #region LevelSuccessTime
        public List<LevelSuccessTime> GetAllLevelSuccessTime()
        {
            return _levelSuccessTimeService.GetAllSuccessTimeFromDb();
        }
        public List<LevelSuccessTime> GetAllLevelSuccessTimeByLevel(int level)
        {
            return _levelSuccessTimeService.GetAllSuccessByLevel(level);
        }
        public bool SaveSuccesTime(IDataModel LevelSuccessTime)
        {
            return _levelSuccessTimeService.SavePerformanceForLevel(LevelSuccessTime);
        }
        public bool ResetLevelSuccessTimeByLevel(int level)
        {
            return _levelSuccessTimeService.DeleteLevelSuccessTimeByLevel(level);
        }
        #endregion

    }


}
