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

        private DataBaseConnector _dbc;
        private GameOptions _GameOptionsModel;
        private SqliteConnection _db;


        /*Queries a base de datos (LevelData)*/
        private string GAME_OPTIONS_DATA = "SELECT * FROM GAMEOPTIONS;";
        private string GAME_OPTIONS_RESET= "UPDATE GAMEOPTIONS SET PVALUE= 1 WHERE OPTIONID = 1 ;";
        private string GAME_OPTIONS_SAVE_VOLUME = "UPDATE GAMEOPTIONS SET PVALUE= @volume WHERE OPTIONID = 1 ;";


        public GameOptionsService(DataBaseConnector dbc)
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
                cmd.CommandText = GAME_OPTIONS_DATA;
                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _GameOptionsModel = new GameOptions()
                    {
                        QuestionID = reader.GetInt32(0),
                        Parameter = reader.GetString(1),
                        PValue = reader.GetString(2)
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
                GameOptions go = (GameOptions) data;

                _db.Open();
                IDbCommand cmd = _db.CreateCommand();
                cmd.CommandText = GAME_OPTIONS_SAVE_VOLUME;

                /*Actualizando volumen registrado*/
                IDbDataParameter volumenParam = cmd.CreateParameter();
                volumenParam.ParameterName = "@volume";
                volumenParam.Value = go.PValue;
                cmd.Parameters.Add(volumenParam);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
        }

    }
}
