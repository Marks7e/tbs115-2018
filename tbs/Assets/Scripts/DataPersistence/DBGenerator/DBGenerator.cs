using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.DataPersistence.DBGenerator
{
    public class DBGenerator
    {
        private DataBaseConnector _dataBaseConnector;
        private SqliteConnection _sqliteConnector;
        private List<string> _cretaDatabaseListModel = null;
        public DBGenerator(DataBaseConnector dataBaseConnector)
        {
            _dataBaseConnector = dataBaseConnector;
            _sqliteConnector = _dataBaseConnector.GetDbInstance();
            _cretaDatabaseListModel = new List<string>();
            _cretaDatabaseListModel.Add("CREATE TABLE IF NOT EXISTS 'LevelSuccessTime' ('SuccessID' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,'levelId' INTEGER NOT NULL,'SuccessTime'	INTEGER NOT NULL)");
            _cretaDatabaseListModel.Add("CREATE TABLE IF NOT EXISTS 'GameOptions' ('OptionID' INTEGER PRIMARY KEY AUTOINCREMENT,'Parameter'TEXT NOT NULL, 'PValue' TEXT NOT NULL)");
            _cretaDatabaseListModel.Add("CREATE TABLE IF NOT EXISTS 'QuestionData' ('QuestionID' INTEGER PRIMARY KEY AUTOINCREMENT,'RealmNumber' INTEGER NOT NULL,'Question' TEXT NOT NULL,'Answer' TEXT NOT NULL)");
            _cretaDatabaseListModel.Add("CREATE TABLE IF NOT EXISTS 'PlayerData' ('PlayerID' INTEGER PRIMARY KEY AUTOINCREMENT,'TotalScore' INTEGER NOT NULL DEFAULT 0)");
            _cretaDatabaseListModel.Add("CREATE TABLE IF NOT EXISTS 'LevelData' ('levelId' INTEGER PRIMARY KEY AUTOINCREMENT,'Realm' INTEGER NOT NULL DEFAULT 0,'BestScore' INTEGER NOT NULL DEFAULT 0,'RoundTime'	INTEGER NOT NULL DEFAULT 0,'PointMultiplier' INTEGER NOT NULL DEFAULT 1,'UnlockLevelAt'	INTEGER NOT NULL DEFAULT 0,	'TimesPlayed' INTEGER)");
            _cretaDatabaseListModel.Add("INSERT INTO 'GameOptions' ('OptionID','Parameter','PValue') VALUES (1,'GeneralVolume','1')");
            _cretaDatabaseListModel.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (1,1,'¿Caminas sin hacer ruido cuando vas a tu cuarto?','')");
            _cretaDatabaseListModel.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (2,1,'¿Compartes el baño de la casa con tu familia o visitas?','')");
            _cretaDatabaseListModel.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (3,1,'¿Levantas tu mano para pedir turno en una conversación?','')");
            _cretaDatabaseListModel.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (4,1,'¿Saludas cuando llegas de visita a la casa de un familiar?','')");
            _cretaDatabaseListModel.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (5,2,'¿Compartes tus pertenencias con otras personas si te lo piden?','')");
            _cretaDatabaseListModel.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (6,2,'¿Sueles utilizar las palabras ''Por favor'' y ''Gracias''?','')");
            _cretaDatabaseListModel.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (7,2,'¿Si alguien te solicita algo, sueles ayudar?','')");
            _cretaDatabaseListModel.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (8,2,'¿Si alguien cumple años, sueles felicitarlo?','')");
            _cretaDatabaseListModel.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (9,3,'¿Si estas enojado, te alejas para tranquilizarte?','')");
            _cretaDatabaseListModel.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (10,3,'¿Puedes reconocer situaciones peligrosas facilmente?','')");
            _cretaDatabaseListModel.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (11,3,'¿Sueles saludar a tus amigos, aunque los veas todos los dias?','')");
            _cretaDatabaseListModel.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (12,3,'¿Eres capaz de controlar el temor por los sonidos fuertes?','')");
            _cretaDatabaseListModel.Add("INSERT INTO 'PlayerData' ('PlayerID','TotalScore') VALUES (1,0)");
            _cretaDatabaseListModel.Add("INSERT INTO 'LevelData' ('levelId','Realm','BestScore','RoundTime','PointMultiplier','UnlockLevelAt','TimesPlayed') VALUES (1,1,0,10,2,0,0)");
            _cretaDatabaseListModel.Add("INSERT INTO 'LevelData' ('levelId','Realm','BestScore','RoundTime','PointMultiplier','UnlockLevelAt','TimesPlayed') VALUES (2,1,0,10,2,0,0)");
            _cretaDatabaseListModel.Add("INSERT INTO 'LevelData' ('levelId','Realm','BestScore','RoundTime','PointMultiplier','UnlockLevelAt','TimesPlayed') VALUES (3,1,0,10,2,0,0)");
            _cretaDatabaseListModel.Add("INSERT INTO 'LevelData' ('levelId','Realm','BestScore','RoundTime','PointMultiplier','UnlockLevelAt','TimesPlayed') VALUES (4,1,0,10,2,0,0)");
            _cretaDatabaseListModel.Add("INSERT INTO 'LevelData' ('levelId','Realm','BestScore','RoundTime','PointMultiplier','UnlockLevelAt','TimesPlayed') VALUES (10,3,0,10,2,0,0)");
            _cretaDatabaseListModel.Add("COMMIT;");
        }
        public bool CreateDbIfNotExist()
        {
            try
            {
                if (!File.Exists(Application.dataPath + "/Database/dbgame.db"))
                {
                    return IsDbExist();
                }
                return false;

            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                return false;
            }
        }
        private bool IsDbExist()
        {
            try
            {
                _sqliteConnector.Open();

                foreach (string query in _cretaDatabaseListModel)
                {
                    IDbCommand dbCommand = _sqliteConnector.CreateCommand();
                    dbCommand.CommandText = query;
                    dbCommand.ExecuteNonQuery();
                }
                _sqliteConnector.Close();
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                return false;
            }
        }
    }
}

