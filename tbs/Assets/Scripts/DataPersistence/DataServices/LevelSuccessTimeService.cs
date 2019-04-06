using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
namespace Assets.Scripts.DataPersistence.DataServices
{
    public class LevelSuccessTimeService : IDataService
    {
        private DataBaseConnector _dataBaseConnector;
        private List<LevelSuccessTime> _levelSuccessTimeListModel;
        private LevelSuccessTime _levelSuccessTimeModel;
        private SqliteConnection _sqliteConnection;

        /*Queries a base de datos (LevelSuccessTime)*/
        private string Level_Success_Time_Data = "SELECT * FROM LevelSuccessTime;";
        private string Level_Success_Time_Data_By_Levelid = "SELECT * FROM LevelSuccessTime WHERE levelId = @param1;";
        private string Oldest_Level_Success_Time_Data_By_Levelid = "SELECT * FROM LevelSuccessTime WHERE levelId = @param1 ORDER BY SuccessID ASC LIMIT 1;";
        private string Level_Success_Update_Time = "UPDATE LevelSuccessTime SET SuccessTime = @param1 WHERE SUCCESSID = @successid;";
        private string Level_Success_Time_Delete_By_Sucessid = "DELETE FROM LevelSuccessTime WHERE SUCCESSID = @param1 ;";
        private string Level_Success_Time_Delete_By_Levelid = "DELETE FROM LevelSuccessTime WHERE LEVELID = @param1 ;";
        private string Level_Success_Insert_Time = "INSERT INTO LevelSuccessTime(levelId,SuccessTime) VALUES(@param1 , @param2);";
        private string Level_Get_Last_Game_Played = "SELECT * FROM LevelSuccessTime ORDER BY SuccessID DESC LIMIT 1; ";


        public LevelSuccessTimeService(DataBaseConnector dataBaseConnector)
        {
            _dataBaseConnector = dataBaseConnector;
            _sqliteConnection = _dataBaseConnector.GetDbInstance();
        }
        public List<LevelSuccessTime> GetAllSuccessTimeFromDb()
        {
            LoadAllDataFromDb();
            return _levelSuccessTimeListModel;
        }
        public List<LevelSuccessTime> GetAllSuccessByLevel(int level)
        {
            try
            {
                LevelSuccessTime levelSuccessTimeModel = null;
                List<LevelSuccessTime> levelSuccessTimeListModel = null;

                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Level_Success_Time_Data_By_Levelid;

                IDbDataParameter dbDataParameterForLevelid = dbCommand.CreateParameter();
                dbDataParameterForLevelid.ParameterName = "@param1";
                dbDataParameterForLevelid.Value = level;
                dbCommand.Parameters.Add(dbDataParameterForLevelid);

                IDataReader dataReader = dbCommand.ExecuteReader();
                levelSuccessTimeListModel = new List<LevelSuccessTime>();

                while (dataReader.Read())
                {
                    levelSuccessTimeModel = new LevelSuccessTime
                    {
                        SuccessID = dataReader.GetInt32(0),
                        LevelID = dataReader.GetInt32(1),
                        SuccessTime = dataReader.GetInt32(2)
                    };
                    levelSuccessTimeListModel.Add(levelSuccessTimeModel);
                }

                dataReader.Close();
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();

                return levelSuccessTimeListModel;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return null;
            }
        }
        public bool SavePerformanceForLevel(IDataModel dataModel)
        {
            LevelSuccessTime levelSuccessTimeListModel = (LevelSuccessTime)dataModel;
            if (SavePerformanceInNewEntry(levelSuccessTimeListModel.LevelID, 5))
            { return SaveDataToDb(dataModel); }
            return UpdateOldestEntry(dataModel);
        }
        private bool SavePerformanceInNewEntry(int levelId, int entryCount)
        {
            try
            {
                int entries = 0;
                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Level_Success_Time_Data_By_Levelid;

                IDbDataParameter dbDataParameterForLevelid = dbCommand.CreateParameter();
                dbDataParameterForLevelid.ParameterName = "@param1";
                dbDataParameterForLevelid.Value = levelId;
                dbCommand.Parameters.Add(dbDataParameterForLevelid);

                IDataReader dataReader = dbCommand.ExecuteReader();
                _levelSuccessTimeListModel = new List<LevelSuccessTime>();

                while (dataReader.Read())
                {
                    entries++;
                }
                dataReader.Close();
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();

                return entries < entryCount;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return false;
            }
        }
        public LevelSuccessTime GetLastLevelPlayed()
        {
            try
            {
                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Level_Get_Last_Game_Played;

                IDataReader dataReader = dbCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    _levelSuccessTimeModel = new LevelSuccessTime()
                    {
                        SuccessID = dataReader.GetInt32(0),
                        LevelID = dataReader.GetInt32(1),
                        SuccessTime = dataReader.GetInt32(2)
                    };
                }

                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return _levelSuccessTimeModel;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return null;
            }
        }
        public bool DeleteLevelSuccessTimeByLevel(int level)
        {
            try
            {
                bool databaseResponse = false;
                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Level_Success_Time_Delete_By_Levelid;

                IDbDataParameter dbDataParameterForLevelid = dbCommand.CreateParameter();
                dbDataParameterForLevelid.ParameterName = "@param1";
                dbDataParameterForLevelid.Value = level;

                dbCommand.Parameters.Add(dbDataParameterForLevelid);

                databaseResponse = dbCommand.ExecuteNonQuery() > 0;
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();

                return databaseResponse;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return false;
            }
        }
        private bool UpdateOldestEntry(IDataModel dataModel)
        {
            try
            {
                int oldEntry = GetOldestEntryByLevelId(dataModel).SuccessID;
                return UpdateHistoryEntryByLevelID(dataModel, oldEntry);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return false;
            }

        }
        private LevelSuccessTime GetOldestEntryByLevelId(IDataModel dataModel)
        {
            try
            {
                LevelSuccessTime levelSuccessTimeListModelForParam = (LevelSuccessTime)dataModel;
                LevelSuccessTime levelSuccessTimeListModel = new LevelSuccessTime();

                _sqliteConnection.Open();
                IDbCommand cmd = _sqliteConnection.CreateCommand();
                cmd.CommandText = Level_Success_Time_Data_By_Levelid;

                IDbDataParameter levelId = cmd.CreateParameter();
                levelId.ParameterName = "@param1";
                levelId.Value = levelSuccessTimeListModelForParam.LevelID;
                cmd.Parameters.Add(levelId);

                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    levelSuccessTimeListModel.SuccessID = reader.GetInt32(0);
                    levelSuccessTimeListModel.LevelID = reader.GetInt32(1);
                    levelSuccessTimeListModel.SuccessTime = reader.GetInt32(2);
                }

                reader.Close();
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();

                return levelSuccessTimeListModel;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return null;
            }
        }
        private bool UpdateHistoryEntryByLevelID(IDataModel dataModel, int oldestSuccessId)
        {
            try
            {
                if (DeleteEntryBySuccessId(oldestSuccessId))
                    return SaveDataToDb(dataModel);
                return false;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return false;
            }
        }
        private bool DeleteEntryBySuccessId(int SuccessId)
        {
            try
            {
                bool databaseResponse = false;
                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Level_Success_Time_Delete_By_Sucessid;

                IDbDataParameter dbDataParameterForLevelid = dbCommand.CreateParameter();
                dbDataParameterForLevelid.ParameterName = "@param1";
                dbDataParameterForLevelid.Value = SuccessId;
                dbCommand.Parameters.Add(dbDataParameterForLevelid);

                databaseResponse = dbCommand.ExecuteNonQuery() > 0;
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();

                return databaseResponse;
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
                dbCommand.CommandText = Level_Success_Time_Data;
                IDataReader dataReader = dbCommand.ExecuteReader();
                _levelSuccessTimeListModel = new List<LevelSuccessTime>();

                while (dataReader.Read())
                {
                    _levelSuccessTimeModel = new LevelSuccessTime()
                    {
                        SuccessID = dataReader.GetInt32(0),
                        LevelID = dataReader.GetInt32(1),
                        SuccessTime = dataReader.GetInt32(2)
                    };
                    _levelSuccessTimeListModel.Add(_levelSuccessTimeModel);
                }

                dataReader.Close();
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();

                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return false;
            }
        }
        public bool SaveDataToDb(IDataModel dataModel)
        {
            try
            {
                bool databaseResponse = false;
                LevelSuccessTime levelSuccessTimeModel = (LevelSuccessTime)dataModel;

                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Level_Success_Insert_Time;

                IDbDataParameter dbDataParameterForLevelid = dbCommand.CreateParameter();
                dbDataParameterForLevelid.ParameterName = "@param1";
                dbDataParameterForLevelid.Value = levelSuccessTimeModel.LevelID;
                dbCommand.Parameters.Add(dbDataParameterForLevelid);

                IDbDataParameter dbDataParamterForSuccessTime = dbCommand.CreateParameter();
                dbDataParamterForSuccessTime.ParameterName = "@param2";
                dbDataParamterForSuccessTime.Value = levelSuccessTimeModel.SuccessTime;
                dbCommand.Parameters.Add(dbDataParamterForSuccessTime);

                databaseResponse = dbCommand.ExecuteNonQuery() > 0;
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();

                return databaseResponse;

            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                return false;
            }
        }
    }
}
