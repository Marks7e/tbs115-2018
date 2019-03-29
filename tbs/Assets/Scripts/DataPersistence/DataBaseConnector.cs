using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

namespace Assets.Scripts.DataPersistence
{
    public class DataBaseConnector
    {
        private SqliteConnection sqlite;
        private enum PlatformDeploy
        {
            Android,
            PC
        }
         
        public SqliteConnection getDBInstance()
        {
            SetConnection();
            return sqlite;
        }

        private void SetConnection()
        {
            PlatformDeploy deploy = PlatformDeploy.Android;

            switch (deploy)
            {
                case PlatformDeploy.Android:
                    sqlite = new SqliteConnection("URI=file:" + Application.persistentDataPath + "/dbgame");
                    Debug.Log(sqlite.ConnectionString);
                    break;
                case PlatformDeploy.PC:
                    sqlite = new SqliteConnection("Data Source=" + Application.dataPath + "/Database/dbgame.db");
                    Debug.Log(sqlite.ConnectionString);
                    break;
                default:
                    sqlite = new SqliteConnection("Data Source=" + Application.dataPath + "/Database/dbgame.db");
                    Debug.Log(sqlite.ConnectionString);
                    break;                   
            }
            
        }

    }
}
