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
    public class LevelDataService : IDataService
    {
        private DataBaseConnector _dbc;
        private List<LevelData> listLevelData;
        private LevelData LevelModel;
        private SqliteConnection _db;


        /*Queries a base de datos (LevelData)*/
        private string LEVEL_ALL_DATA = "SELECT * FROM LEVELDATA ;";
        private string LEVEL_UPDATE_BEST_SCORE_DATA = "UPDATE LEVELDATA SET BESTSCORE = @bestScore WHERE LEVELID = @level ;";
        private string LEVEL_UPDATE_TIMES_PLAYED = "UPDATE LEVELDATA SET TIMESPLAYED = @param1 WHERE LEVELID = @param2 ;";
        private string LEVEL_GET_ENTRY_BY_LEVELID = "SELECT * FROM LEVELDATA WHERE LEVELID = @param1 ;";

        public LevelDataService(DataBaseConnector dbc)
        {
            _dbc = dbc;
            _db = _dbc.getDBInstance();
        }

        public bool LoadAllDataFromDB()
        {
            try
            {
                _db.Open();
                IDbCommand cmd = _db.CreateCommand();
                cmd.CommandText = LEVEL_ALL_DATA;
                IDataReader reader = cmd.ExecuteReader();
                listLevelData = new List<LevelData>();

                while (reader.Read())
                {
                    LevelModel = new LevelData()
                    {
                        LevelID = reader.GetInt32(0),
                        BestScore = reader.GetInt32(1),
                        RoundTime = reader.GetInt32(2),
                        PointMultiplier = reader.GetDouble(3),
                        UnlockLevelAt = reader.GetInt32(4),
                        TimesPlayed = reader.GetInt32(5)
                    };
                    listLevelData.Add(LevelModel);
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
                LevelData ld = (LevelData)data;

                _db.Open();
                IDbCommand cmd = _db.CreateCommand();
                cmd.CommandText = LEVEL_UPDATE_BEST_SCORE_DATA;

                /*Agregando Parametro para guardar Mejor Score del nivel*/
                IDbDataParameter bestScoreParam = cmd.CreateParameter();
                bestScoreParam.ParameterName = "@bestScore";
                bestScoreParam.Value = ld.BestScore;
                cmd.Parameters.Add(bestScoreParam);

                /*Agregando Parametro para identificar a que nivel guardar los cambios*/
                IDbDataParameter levelParam = cmd.CreateParameter();
                levelParam.ParameterName = "@level";
                levelParam.Value = ld.LevelID;
                cmd.Parameters.Add(levelParam);

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

        public LevelData GetLevelData(int level)
        {
            try
            {
                LevelData res = new LevelData();
                _db.Open();
                IDbCommand cmd = _db.CreateCommand();
                cmd.CommandText = LEVEL_GET_ENTRY_BY_LEVELID;

                IDbDataParameter bestScoreParam = cmd.CreateParameter();
                bestScoreParam.ParameterName = "@param1";
                bestScoreParam.Value = level;
                cmd.Parameters.Add(bestScoreParam);

                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    res.LevelID = reader.GetInt32(0);
                    res.BestScore = reader.GetInt32(1);
                    res.RoundTime = reader.GetInt32(2);
                    res.PointMultiplier = reader.GetDouble(3);
                    res.UnlockLevelAt = reader.GetInt32(4);
                    res.TimesPlayed = reader.GetInt32(5);
                }
                reader.Close();
                _db.Close();
                _db.Dispose();
                return res;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                _db.Close();
                _db.Dispose();
                return null;
            }
        }

        public List<LevelData> GetAllLevelData()
        {
            LoadAllDataFromDB();
            return listLevelData;

        }
    }
}
