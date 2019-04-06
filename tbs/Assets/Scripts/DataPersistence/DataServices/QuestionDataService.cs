using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Assets.Scripts.DataPersistence.DataServices
{
    public class QuestionDataService : IDataService
    {
        private DataBaseConnector _dataBaseConnector;
        private List<QuestionData> _questionDataListModel;
        private QuestionData _questionDataModel;
        private SqliteConnection _sqliteConnection;

        /*Queries a base de datos (LevelData)*/
        private string Question_All_Data = "SELECT * FROM QUESTIONDATA ;";
        private string Question_Update_Answer_Data = "UPDATE QUESTIONDATA SET ANSWER = @answer WHERE QUESTIONID = @questionid ;";

        public QuestionDataService(DataBaseConnector dataBaseConnector)
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
                dbCommand.CommandText = Question_All_Data;
                IDataReader dataReader = dbCommand.ExecuteReader();
                _questionDataListModel = new List<QuestionData>();

                while (dataReader.Read())
                {
                    _questionDataModel = new QuestionData()
                    {
                        QuestionID = dataReader.GetInt32(0),
                        RealmNumber = dataReader.GetInt32(1),
                        Question = dataReader.GetString(2),
                        Answer = dataReader.GetString(3)
                    };
                    _questionDataListModel.Add(_questionDataModel);
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
                QuestionData questionDataModel = (QuestionData)dataModel;

                _sqliteConnection.Open();
                IDbCommand dbCommand = _sqliteConnection.CreateCommand();
                dbCommand.CommandText = Question_Update_Answer_Data;

                /*Agregando Parametro para guardar respuesta de pregunta*/
                IDbDataParameter dbDataParameterForBestScore = dbCommand.CreateParameter();
                dbDataParameterForBestScore.ParameterName = "@answer";
                dbDataParameterForBestScore.Value = questionDataModel.Answer;
                dbCommand.Parameters.Add(dbDataParameterForBestScore);

                /*Agregando Parametro para identificar la que pregunta para guardar la respuesta*/
                IDbDataParameter dbDataParameterForLevel = dbCommand.CreateParameter();
                dbDataParameterForLevel.ParameterName = "@questionid";
                dbDataParameterForLevel.Value = questionDataModel.QuestionID;
                dbCommand.Parameters.Add(dbDataParameterForLevel);

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

        public List<QuestionData> GetAllQuestions()
        {
            LoadAllDataFromDb();
            return _questionDataListModel;
        }
    }
}
