using Mono.Data.Sqlite;
using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using System;
using System.Data;
using UnityEngine;

namespace Assets.Scripts.DataPersistence.DataServices
{
    public class PlayerDataService : IDataService
    {
        private DataBaseConnector _dataBaseConnector;
        private PlayerData playerDataModel;
        private SqliteConnection _sqliteConnection;

        /*Queries a base de datos (PlayerData)*/
        private string Player_All_Data = "SELECT * FROM PLAYERDATA WHERE PLAYERID = 1;";
        private string Player_Update_Total_Score = "UPDATE PLAYERDATA SET TOTALSCORE = @param ;";

        public PlayerDataService(DataBaseConnector dataBaseConnector)
        {
            _dataBaseConnector = dataBaseConnector;
            _sqliteConnection = _dataBaseConnector.GetDbInstance();
        }

        public bool LoadAllDataFromDb()
        {
            try
            {
                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Player_All_Data;
                IDataReader dataReader = dbCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    playerDataModel = new PlayerData()
                    {
                        PlayerID = dataReader.GetInt32(0),
                        TotalScore = dataReader.GetInt32(1)
                    };
                }
                dataReader.Close();
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                return false;
            }

        }
        public bool SaveDataToDb(IDataModel dataModel)
        {
            try
            {
                bool databaseResponse = false;
                PlayerData playerDataModel = (PlayerData)dataModel;
                LoadAllDataFromDb();

                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Player_Update_Total_Score;

                IDbDataParameter dbDataParameterForTotalScore = dbCommand.CreateParameter();
                dbDataParameterForTotalScore.ParameterName = "@param";
                dbDataParameterForTotalScore.Value = playerDataModel.TotalScore + this.playerDataModel.TotalScore;
                dbCommand.Parameters.Add(dbDataParameterForTotalScore);

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

        public PlayerData GetPlayerData()
        {
            LoadAllDataFromDb();
            return playerDataModel;
        }
    }


}

