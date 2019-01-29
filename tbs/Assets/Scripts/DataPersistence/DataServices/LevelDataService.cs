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
                    };
                    listLevelData.Add(LevelModel);
                }
                reader.Close();
                _db.Close();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
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
                bestScoreParam.Value = ld. BestScore;
                cmd.Parameters.Add(bestScoreParam);

                /*Agregando Parametro para identificar a que nivel guardar los cambios*/
                IDbDataParameter levelParam= cmd.CreateParameter();
                levelParam.ParameterName = "@level";
                levelParam.Value = ld.LevelID;
                cmd.Parameters.Add(levelParam);


                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
        }

        public LevelData GetLevelData(int level)
        {
            LoadAllDataFromDB();
            return listLevelData.FirstOrDefault(l => l.LevelID == level);
        }

        public List<LevelData> GetAllLevelData()
        {
            LoadAllDataFromDB();
            return listLevelData;

        }
    }
}
