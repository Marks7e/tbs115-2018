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
    public class QuestionDataService : IDataService
    {
        private DataBaseConnector _dbc;
        private List<QuestionData> listQuestionData;
        private QuestionData QuestionModel;
        private SqliteConnection _db;

        /*Queries a base de datos (LevelData)*/
        private string QUESTION_ALL_DATA = "SELECT * FROM QUESTIONDATA ;";
        private string QUESTION_UPDATE_ANSWER_DATA = "UPDATE QUESTIONDATA SET ANSWER = @answer WHERE QUESTIONID = @questionid ;";

        public QuestionDataService(DataBaseConnector dbc)
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
                cmd.CommandText = QUESTION_ALL_DATA;
                IDataReader reader = cmd.ExecuteReader();
                listQuestionData = new List<QuestionData>();

                while (reader.Read())
                {
                    QuestionModel = new QuestionData()
                    {
                        QuestionID = reader.GetInt32(0),
                        RealmNumber = reader.GetInt32(1),
                        Question = reader.GetString(2),
                        Answer = reader.GetString(3)
                    };
                    listQuestionData.Add(QuestionModel);
                }
                reader.Close();
                _db.Close();
                _db.Dispose();
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
                QuestionData qd = (QuestionData)data;

                _db.Open();
                IDbCommand cmd = _db.CreateCommand();
                cmd.CommandText = QUESTION_UPDATE_ANSWER_DATA;

                /*Agregando Parametro para guardar respuesta de pregunta*/
                IDbDataParameter bestScoreParam = cmd.CreateParameter();
                bestScoreParam.ParameterName = "@answer";
                bestScoreParam.Value = qd.Answer;
                cmd.Parameters.Add(bestScoreParam);

                /*Agregando Parametro para identificar la que pregunta para guardar la respuesta*/
                IDbDataParameter levelParam = cmd.CreateParameter();
                levelParam.ParameterName = "@questionid";
                levelParam.Value = qd.QuestionID;
                cmd.Parameters.Add(levelParam);

                bool res  = cmd.ExecuteNonQuery() > 0;
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

        public List<QuestionData> GetAllQuestions()
        {
            LoadAllDataFromDB();
            return listQuestionData;
        }
    }
}
