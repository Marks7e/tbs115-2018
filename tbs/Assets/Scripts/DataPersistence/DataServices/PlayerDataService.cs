using Mono.Data.Sqlite;
using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Data.SqlClient;

namespace Assets.Scripts.DataPersistence.DataServices
{
    public class PlayerDataService : IDataService
    {
        private DataBaseConnector _dbc;
        private PlayerData PlayerModel;
        private SqliteConnection _db;

        /*Queries a base de datos (PlayerData)*/
        private string PLAYER_ALL_DATA = "SELECT * FROM PLAYERDATA WHERE PLAYERID = 1;";
        private string PLAYER_UPDATE_TOTAL_SCORE = "UPDATE PLAYERDATA SET TOTALSCORE = @param ;";

        public PlayerDataService(DataBaseConnector dbc)
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
                cmd.CommandText = PLAYER_ALL_DATA;
                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PlayerModel = new PlayerData()
                    {
                        PlayerID = reader.GetInt32(0),
                        TotalScore = reader.GetInt32(1)
                    };
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
                PlayerData pd = (PlayerData)data;
                LoadAllDataFromDB();

                _db.Open();
                IDbCommand cmd = _db.CreateCommand();
                cmd.CommandText = PLAYER_UPDATE_TOTAL_SCORE;

                IDbDataParameter parameter = cmd.CreateParameter();
                parameter.ParameterName = "@param";
                parameter.Value = pd.TotalScore + PlayerModel.TotalScore;
                cmd.Parameters.Add(parameter);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
        }


        public PlayerData GetPlayerData()
        {
            LoadAllDataFromDB();
            return PlayerModel;
        }
    }


}

