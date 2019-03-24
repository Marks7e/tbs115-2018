using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts.DataPersistence.DataServices
{
    public class LevelSuccessTimeService : IDataService
    {
        private DataBaseConnector _dbc;
        private List<LevelSuccessTime> listLevelSuccessTime;
        private LevelSuccessTime LevelSuccessModel;
        private SqliteConnection _db;

        /*Queries a base de datos (LevelSuccessTime)*/
        private string LEVEL_SUCCESS_TIME_DATA = "SELECT * FROM LevelSuccessTime;";
        private string LEVEL_SUCCESS_TIME_DATA_BY_LEVELID = "SELECT * FROM LevelSuccessTime WHERE LevelID = @param1;";
        private string OLDEST_LEVEL_SUCCESS_TIME_DATA_BY_LEVELID = "SELECT * FROM LevelSuccessTime WHERE LevelID = @param1 ORDER BY SuccessID ASC LIMIT 1;";
        private string LEVEL_SUCCESS_UPDATE_TIME = "UPDATE LevelSuccessTime SET SuccessTime = @param1 WHERE SUCCESSID = @successid;";
        private string LEVEL_SUCCESS_TIME_DETELE_BY_SUCCESSID = "DELETE FROM LevelSuccessTime WHERE SUCCESSID = @param1 ;";
        private string LEVEL_SUCCESS_TIME_DETELE_BY_LEVELID = "DELETE FROM LevelSuccessTime WHERE LEVELID = @param1 ;";
        private string LEVEL_SUCCESS_INSERT_TIME = "INSERT INTO LevelSuccessTime(LevelID,SuccessTime) VALUES(@param1 , @param2);";

        public LevelSuccessTimeService(DataBaseConnector dbc)
        {
            _dbc = dbc;
            _db = _dbc.getDBInstance();
        }

        public List<LevelSuccessTime> GetAllSuccessTimeFromDB()
        {
            LoadAllDataFromDB();
            return listLevelSuccessTime;
        }

        public List<LevelSuccessTime> GetAllSuccessByLevel(int level)
        {
            try
            {
                LevelSuccessTime lst = null;
                List<LevelSuccessTime> lstl = null;

                _db.Open();
                IDbCommand cmd = _db.CreateCommand();
                cmd.CommandText = LEVEL_SUCCESS_TIME_DATA_BY_LEVELID;

                IDbDataParameter levelId = cmd.CreateParameter();
                levelId.ParameterName = "@param1";
                levelId.Value = level;
                cmd.Parameters.Add(levelId);

                IDataReader reader = cmd.ExecuteReader();
                lstl = new List<LevelSuccessTime>();

                while (reader.Read())
                {
                    lst = new LevelSuccessTime
                    {
                        SuccessID = reader.GetInt32(0),
                        LevelID = reader.GetInt32(1),
                        SuccessTime = reader.GetInt32(2)
                    };
                    lstl.Add(lst);
                }

                reader.Close();
                _db.Close();
                _db.Dispose();

                return lstl;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                _db.Close();
                _db.Dispose();
                return null;
            }
        }

        public bool SavePerformanceForLevel(IDataModel data)
        {
            LevelSuccessTime _lst = (LevelSuccessTime)data;
            if (SavePerformanceInNewEntry(_lst.LevelID, 5))
            { return SaveDataToDB(data); }
            return UpdateOldestEntry(data);
        }

        private bool SavePerformanceInNewEntry(int levelID, int entryCount)
        {
            try
            {
                int entries = 0;
                _db.Open();
                IDbCommand cmd = _db.CreateCommand();
                cmd.CommandText = LEVEL_SUCCESS_TIME_DATA_BY_LEVELID;

                IDbDataParameter levelId = cmd.CreateParameter();
                levelId.ParameterName = "@param1";
                levelId.Value = levelID;
                cmd.Parameters.Add(levelId);

                IDataReader reader = cmd.ExecuteReader();
                listLevelSuccessTime = new List<LevelSuccessTime>();

                while (reader.Read())
                {
                    entries++;
                }
                reader.Close();
                _db.Close();
                _db.Dispose();

                return entries < entryCount;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                _db.Close();
                _db.Dispose();
                return false;
            }
        }

        public bool DeleteLevelSuccessTimeByLevel(int level)
        {
            try
            {
                _db.Open();
                IDbCommand command = _db.CreateCommand();
                command.CommandText = LEVEL_SUCCESS_TIME_DETELE_BY_LEVELID;

                IDbDataParameter param1 = command.CreateParameter();
                param1.ParameterName = "@param1";
                param1.Value = level;

                command.Parameters.Add(param1);

                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                _db.Close();
                _db.Dispose();
                return false;
            }
        }

        private bool UpdateOldestEntry(IDataModel data)
        {
            try
            {
                int oldEntry = GetOldestEntryByLevelID(data).SuccessID;
                return UpdateHistoryEntryByLevelID(data, oldEntry);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                _db.Close();
                _db.Dispose();
                return false;
            }

        }
        private LevelSuccessTime GetOldestEntryByLevelID(IDataModel data)
        {
            try
            {
                LevelSuccessTime _lst = (LevelSuccessTime)data;
                LevelSuccessTime _rlst = new LevelSuccessTime();

                _db.Open();
                IDbCommand cmd = _db.CreateCommand();
                cmd.CommandText = OLDEST_LEVEL_SUCCESS_TIME_DATA_BY_LEVELID;

                IDbDataParameter levelId = cmd.CreateParameter();
                levelId.ParameterName = "@param1";
                levelId.Value = _lst.LevelID;
                cmd.Parameters.Add(levelId);

                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _rlst.SuccessID = reader.GetInt32(0);
                    _rlst.LevelID = reader.GetInt32(1);
                    _rlst.SuccessTime = reader.GetInt32(2);
                }

                reader.Close();
                _db.Close();
                _db.Dispose();

                return _rlst;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                _db.Close();
                _db.Dispose();
                return null;
            }
        }
        private bool UpdateHistoryEntryByLevelID(IDataModel data, int oldestSuccessID)
        {
            try
            {
                if (DeleteEntryBySuccessID(oldestSuccessID))
                    return SaveDataToDB(data);
                return false;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                _db.Close();
                _db.Dispose();
                return false;
            }
        }
        private bool DeleteEntryBySuccessID(int SuccessID)
        {
            try
            {
                _db.Open();
                IDbCommand cmd = _db.CreateCommand();
                cmd.CommandText = LEVEL_SUCCESS_TIME_DETELE_BY_SUCCESSID;

                IDbDataParameter levelId = cmd.CreateParameter();
                levelId.ParameterName = "@param1";
                levelId.Value = SuccessID;
                cmd.Parameters.Add(levelId);

                var res = cmd.ExecuteNonQuery() > 0;
                _db.Close();
                _db.Dispose();
                return res;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                _db.Close();
                _db.Dispose();
                return false;
            }
        }

        public bool LoadAllDataFromDB()
        {
            try
            {
                _db.Open();
                IDbCommand cmd = _db.CreateCommand();
                cmd.CommandText = LEVEL_SUCCESS_TIME_DATA;
                IDataReader reader = cmd.ExecuteReader();
                listLevelSuccessTime = new List<LevelSuccessTime>();

                while (reader.Read())
                {
                    LevelSuccessModel = new LevelSuccessTime()
                    {
                        SuccessID = reader.GetInt32(0),
                        LevelID = reader.GetInt32(1),
                        SuccessTime = reader.GetInt32(2)
                    };
                    listLevelSuccessTime.Add(LevelSuccessModel);
                }
                reader.Close();
                _db.Close();
                _db.Dispose();

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                _db.Close();
                _db.Dispose();
                return false;
            }
        }
        public bool SaveDataToDB(IDataModel data)
        {
            try
            {
                LevelSuccessTime lst = (LevelSuccessTime)data;

                _db.Open();
                IDbCommand cmd = _db.CreateCommand();
                cmd.CommandText = LEVEL_SUCCESS_INSERT_TIME;

                IDbDataParameter levelId = cmd.CreateParameter();
                levelId.ParameterName = "@param1";
                levelId.Value = lst.LevelID;
                cmd.Parameters.Add(levelId);

                IDbDataParameter successTime = cmd.CreateParameter();
                successTime.ParameterName = "@param2";
                successTime.Value = lst.SuccessTime;
                cmd.Parameters.Add(successTime);

                bool res = cmd.ExecuteNonQuery() > 0;
                _db.Close();
                _db.Dispose();

                return res;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
        }
    }
}
