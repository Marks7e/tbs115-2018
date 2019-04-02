using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.DataPersistence.DBGenerator
{
    public class DBGenerator
    {
        private DataBaseConnector _dbc;
        private SqliteConnection _db;
        private List<string> CREATE_DB = null;

        /*Queries a base de datos (Metadata/sqlite_master)*/
        private string IS_DB_EXIST = "SELECT Count(1) From sqlite_master WHERE type = 'table'";


        public DBGenerator(DataBaseConnector dbc)
        {
            _dbc = dbc;
            _db = _dbc.getDBInstance();
            CREATE_DB = new List<string>();
            CREATE_DB.Add("CREATE TABLE IF NOT EXISTS 'LevelSuccessTime' ('SuccessID' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,'LevelID' INTEGER NOT NULL,'SuccessTime'	INTEGER NOT NULL)");
            CREATE_DB.Add("CREATE TABLE IF NOT EXISTS 'GameOptions' ('OptionID' INTEGER PRIMARY KEY AUTOINCREMENT,'Parameter'TEXT NOT NULL, 'PValue' TEXT NOT NULL)");
            CREATE_DB.Add("CREATE TABLE IF NOT EXISTS 'QuestionData' ('QuestionID' INTEGER PRIMARY KEY AUTOINCREMENT,'RealmNumber' INTEGER NOT NULL,'Question' TEXT NOT NULL,'Answer' TEXT NOT NULL)");
            CREATE_DB.Add("CREATE TABLE IF NOT EXISTS 'PlayerData' ('PlayerID' INTEGER PRIMARY KEY AUTOINCREMENT,'TotalScore' INTEGER NOT NULL DEFAULT 0)");
            CREATE_DB.Add("CREATE TABLE IF NOT EXISTS 'LevelData' ('LevelID' INTEGER PRIMARY KEY AUTOINCREMENT,'BestScore' INTEGER NOT NULL DEFAULT 0,'RoundTime'	INTEGER NOT NULL DEFAULT 0,'PointMultiplier' INTEGER NOT NULL DEFAULT 1,'UnlockLevelAt'	INTEGER NOT NULL DEFAULT 0,	'TimesPlayed' INTEGER)");
            CREATE_DB.Add("INSERT INTO 'GameOptions' ('OptionID','Parameter','PValue') VALUES (1,'GeneralVolume','1')");
            CREATE_DB.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (1,1,'¿Caminas sin hacer ruido cuando vas a tu cuarto?','')");
            CREATE_DB.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (2,1,'¿Compartes el baño de la casa con tu familia o visitas?','')");
            CREATE_DB.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (3,1,'¿Levantas tu mano para pedir turno en una conversación?','')");
            CREATE_DB.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (4,1,'¿Compartes tus pertenencias con otras personas si te lo piden?','')");
            CREATE_DB.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (5,1,'¿Sueles utilizar las palabras ''Por favor'' y ''Gracias''?','')");
            CREATE_DB.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (6,1,'¿Si alguien te solicita algo, sueles ayudar?','')");
            CREATE_DB.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (7,1,'¿Si alguien cumple años, sueles felicitarlo?','')");
            CREATE_DB.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (8,1,'¿Si estas enojado, te alejas para tranquilizarte?','')");
            CREATE_DB.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (9,1,'¿Sueles saludar a tus amigos, aunque los veas todos los dias?','')");
            CREATE_DB.Add("INSERT INTO 'QuestionData' ('QuestionID','RealmNumber','Question','Answer') VALUES (10,1,'¿Eres capaz de controlar el temor por los sonidos fuertes?','')");
            CREATE_DB.Add("INSERT INTO 'PlayerData' ('PlayerID','TotalScore') VALUES (1,0)");
            CREATE_DB.Add("INSERT INTO 'LevelData' ('LevelID','BestScore','RoundTime','PointMultiplier','UnlockLevelAt','TimesPlayed') VALUES (1,0,10,2,0,0)");
            CREATE_DB.Add("INSERT INTO 'LevelData' ('LevelID','BestScore','RoundTime','PointMultiplier','UnlockLevelAt','TimesPlayed') VALUES (2,0,10,2,0,0)");
            CREATE_DB.Add("INSERT INTO 'LevelData' ('LevelID','BestScore','RoundTime','PointMultiplier','UnlockLevelAt','TimesPlayed') VALUES (3,0,10,2,0,0)");
            CREATE_DB.Add("INSERT INTO 'LevelData' ('LevelID','BestScore','RoundTime','PointMultiplier','UnlockLevelAt','TimesPlayed') VALUES (4,0,10,2,0,0)");
            CREATE_DB.Add("COMMIT;");
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
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }

        }

        private bool IsDbExist()
        {
            try
            {
                _db.Open();

                foreach (string query in CREATE_DB)
                {
                    IDbCommand cmd = _db.CreateCommand();
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }

                _db.Close();
                return true;

            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
        }
    }
}

