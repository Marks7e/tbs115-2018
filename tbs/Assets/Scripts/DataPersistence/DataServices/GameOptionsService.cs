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
    public class GameOptionsService : IDataService
    {

        private DataBaseConnector _dataBaseConnector;
        private GameOptions _gameOptionModel;
        private SqliteConnection _sqliteConnection;

        public enum GameSettings
        {
            Volume = 1,
            GameBalanceEngine = 2
        }

        /*Queries a base de datos (LevelData)*/
        private string Game_Options_Data = "SELECT * FROM GAMEOPTIONS;";
        private string Game_Options_Save_Volume = "UPDATE GAMEOPTIONS SET PVALUE= @volume WHERE OPTIONID = 1 ;";
        private string Game_Options_Load_Setting_By_Id = "SELECT * FROM GAMEOPTIONS WHERE OPTIONID = @param1 ;";
        private string Game_Options_Save_Settings_By_Id = "UPDATE GAMEOPTIONS SET PVALUE = @param1 WHERE OPTIONID = @param2 ;";

        public GameOptionsService(DataBaseConnector dataBaseConnector)
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
                dbCommand.CommandText = Game_Options_Data;
                IDataReader dataReader = dbCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    _gameOptionModel = new GameOptions()
                    {
                        OptionID = dataReader.GetInt32(0),
                        Parameter = dataReader.GetString(1),
                        PValue = dataReader.GetString(2)
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
                GameOptions gameOptionsModel = (GameOptions)dataModel;

                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Game_Options_Save_Volume;

                /*Actualizando volumen registrado*/
                IDbDataParameter dbVolumeDataParameter = dbCommand.CreateParameter();
                dbVolumeDataParameter.ParameterName = "@volume";
                dbVolumeDataParameter.Value = gameOptionsModel.PValue;
                dbCommand.Parameters.Add(dbVolumeDataParameter);

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
        public GameOptions LoadGameOptionByOptionID(GameSettings gameSettings)
        {
            try
            {
                int setting = (int)gameSettings;
                GameOptions gameOption = new GameOptions();

                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Game_Options_Load_Setting_By_Id;

                IDbDataParameter dataParameterValue = dbCommand.CreateParameter();
                dataParameterValue.ParameterName = "@param1";
                dataParameterValue.Value = setting;

                dbCommand.Parameters.Add(dataParameterValue);

                IDataReader dataReader = dbCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    gameOption.OptionID = dataReader.GetInt32(0);
                    gameOption.Parameter = dataReader.GetString(1);
                    gameOption.PValue = dataReader.GetString(2);
                }
                return gameOption;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return null;
            }
        }
        public bool SaveGameOptionByOptionID(IDataModel dataModel)
        {
            try
            {
                _sqliteConnection.Open();
                GameOptions gameOption = new GameOptions();
                gameOption = (GameOptions)dataModel;
                bool dataBaseResponse = false;

                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Game_Options_Save_Settings_By_Id;
                IDataParameter dataParameterValue = dbCommand.CreateParameter();
                dataParameterValue.ParameterName = "@param1";
                dataParameterValue.Value = gameOption.PValue;

                IDataParameter dataParameterOptionId = dbCommand.CreateParameter();
                dataParameterOptionId.ParameterName = "@param2";
                dataParameterOptionId.Value = gameOption.OptionID;

                dbCommand.Parameters.Add(dataParameterValue);
                dbCommand.Parameters.Add(dataParameterOptionId);

                dataBaseResponse = dbCommand.ExecuteNonQuery() > 0;
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();

                return dataBaseResponse;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
        }

    }
}
