using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

namespace Assets.Scripts.DataPersistence
{
    public class DataBaseConnector
    {
        private SqliteConnection sqlite;
         
        public SqliteConnection getDBInstance()
        {
            SetConnection();
            return sqlite;
        }

        private void SetConnection()
        {
            sqlite = new SqliteConnection("Data Source=" + Application.dataPath + "/Database/dbgame.db");
            //sqlite = new SqliteConnection("Data Source=" + Application.persistentDataPath + "/Database/dbgame.db");
        }

    }
}
