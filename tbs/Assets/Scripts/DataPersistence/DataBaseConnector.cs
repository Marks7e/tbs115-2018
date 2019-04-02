using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

namespace Assets.Scripts.DataPersistence
{
    public class DataBaseConnector
    {
        private enum PlatformDeploy
        {
            Android,
            PC
        }

        private SqliteConnection sqlite;

        public SqliteConnection getDBInstance()
        {
            SetConnection(PlatformDeploy.Android);
            return sqlite;
        }

        private void SetConnection(PlatformDeploy deploy)
        {
            switch (deploy)
            {
                case PlatformDeploy.Android:
                    sqlite = new SqliteConnection("URI=file:" + Application.persistentDataPath + "/dbgame.db");
                    //sqlite = new SqliteConnection("URI=file:" + Application.dataPath + "/Database/dbgame.db");
                    break;
                case PlatformDeploy.PC:
                    sqlite = new SqliteConnection("Data Source=" + Application.dataPath + "/Database/dbgame.db");
                    break;
            }

        }

    }
}
