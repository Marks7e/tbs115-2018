using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Assets.Scripts.DataPersistence.DataServices
{
    public class LevelDataService : IDataService
    {
        private DataBaseConnector _dataBaseConnector;
        private List<LevelData> _levelDataListModel;
        private LevelData _levelDataModel;
        private SqliteConnection _sqliteConnection;


        /*Queries a base de datos (LevelData)*/
        private string Level_All_Data = "SELECT * FROM LEVELDATA ;";
        private string Level_Update_Best_Score = "UPDATE LEVELDATA SET BESTSCORE = @bestScore WHERE LEVELID = @level ;";
        private string Level_Update_Times_Played = "UPDATE LEVELDATA SET TIMESPLAYED = @param1 WHERE LEVELID = @param2 ;";
        private string Level_Get_Entry_By_Levelid = "SELECT * FROM LEVELDATA WHERE LEVELID = @param1 ;";

        public LevelDataService(DataBaseConnector dataBaseConnector)
        {
            _dataBaseConnector = dataBaseConnector;
            _sqliteConnection = _dataBaseConnector.GetDbInstance();
        }


        public bool UpdateTimesPlayedForLevelId(int levelId)
        {
            try
            {
                LevelData levelDataModel = new LevelData();
                levelDataModel = GetLevelData(levelId);

                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Level_Update_Times_Played;

                IDbDataParameter dbDataParameterForBestScore = dbCommand.CreateParameter();
                dbDataParameterForBestScore.ParameterName = "@param1";
                dbDataParameterForBestScore.Value = levelDataModel.TimesPlayed + 1;
                dbCommand.Parameters.Add(dbDataParameterForBestScore);

                IDbDataParameter dbDataParameterForLevel = dbCommand.CreateParameter();
                dbDataParameterForLevel.ParameterName = "@param2";
                dbDataParameterForLevel.Value = levelId;
                dbCommand.Parameters.Add(dbDataParameterForLevel);

                _sqliteConnection.Close();
                _sqliteConnection.Dispose();

                return dbCommand.ExecuteNonQuery() > 0;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return false;
            }
        }

        public bool LoadAllDataFromDb()
        {
            try
            {
                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Level_All_Data;
                IDataReader dataReader = dbCommand.ExecuteReader();
                _levelDataListModel = new List<LevelData>();

                while (dataReader.Read())
                {
                    _levelDataModel = new LevelData()
                    {
                        LevelId = dataReader.GetInt32(0),
                        BestScore = dataReader.GetInt32(1),
                        RoundTime = dataReader.GetInt32(2),
                        PointMultiplier = dataReader.GetDouble(3),
                        UnlockLevelAt = dataReader.GetInt32(4),
                        TimesPlayed = dataReader.GetInt32(5)
                    };
                    _levelDataListModel.Add(_levelDataModel);
                }

                dataReader.Close();
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return false;
            }
        }
        public bool SaveDataToDb(IDataModel dataModel)
        {
            try
            {
                LevelData levelDataModel = (LevelData)dataModel;

                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Level_Update_Best_Score;

                /*Agregando Parametro para guardar Mejor Score del nivel*/
                IDbDataParameter dbDataParameterForBestScore = dbCommand.CreateParameter();
                dbDataParameterForBestScore.ParameterName = "@bestScore";
                dbDataParameterForBestScore.Value = levelDataModel.BestScore;
                dbCommand.Parameters.Add(dbDataParameterForBestScore);

                /*Agregando Parametro para identificar a que nivel guardar los cambios*/
                IDbDataParameter dbDataParameterLevel = dbCommand.CreateParameter();
                dbDataParameterLevel.ParameterName = "@level";
                dbDataParameterLevel.Value = levelDataModel.LevelId;
                dbCommand.Parameters.Add(dbDataParameterLevel);

                _sqliteConnection.Close();
                _sqliteConnection.Dispose();

                return dbCommand.ExecuteNonQuery() > 0;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                return false;
            }
        }

        public LevelData GetLevelData(int level)
        {
            try
            {
                LevelData levelDataModel = new LevelData();
                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Level_Get_Entry_By_Levelid;

                IDbDataParameter dbDataParameterForBestScore = dbCommand.CreateParameter();
                dbDataParameterForBestScore.ParameterName = "@param1";
                dbDataParameterForBestScore.Value = level;
                dbCommand.Parameters.Add(dbDataParameterForBestScore);

                IDataReader dataReader = dbCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    levelDataModel.LevelId = dataReader.GetInt32(0);
                    levelDataModel.BestScore = dataReader.GetInt32(1);
                    levelDataModel.RoundTime = dataReader.GetInt32(2);
                    levelDataModel.PointMultiplier = dataReader.GetDouble(3);
                    levelDataModel.UnlockLevelAt = dataReader.GetInt32(4);
                    levelDataModel.TimesPlayed = dataReader.GetInt32(5);
                }

                dataReader.Close();
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();

                return levelDataModel;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return null;
            }
        }
        public List<LevelData> GetAllLevelData()
        {
            LoadAllDataFromDb();
            return _levelDataListModel;
        }
    }
}
