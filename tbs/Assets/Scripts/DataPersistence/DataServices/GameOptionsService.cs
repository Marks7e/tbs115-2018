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


        /*Queries a base de datos (LevelData)*/
        private string Game_Options_Data = "SELECT * FROM GAMEOPTIONS;";
        private string Game_Options_Save_Volume = "UPDATE GAMEOPTIONS SET PVALUE= @volume WHERE OPTIONID = 1 ;";


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
                        QuestionId = dataReader.GetInt32(0),
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
                GameOptions gameOptionsModel = (GameOptions)dataModel;

                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Game_Options_Save_Volume;

                /*Actualizando volumen registrado*/
                IDbDataParameter dbVolumeDataParameter = dbCommand.CreateParameter();
                dbVolumeDataParameter.ParameterName = "@volume";
                dbVolumeDataParameter.Value = gameOptionsModel.PValue;
                dbCommand.Parameters.Add(dbVolumeDataParameter);

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

    }
}
