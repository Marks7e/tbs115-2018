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

        private SqliteConnection _sqliteConnection;

        public SqliteConnection GetDbInstance()
        {
            SetConnection(PlatformDeploy.PC);
            return _sqliteConnection;
        }

        private void SetConnection(PlatformDeploy platformDeploy)
        {
            switch (platformDeploy)
            {
                case PlatformDeploy.Android:
                    _sqliteConnection = new SqliteConnection("URI=file:" + Application.persistentDataPath + "/dbgame.db");
                    break;
                case PlatformDeploy.PC:
                    _sqliteConnection = new SqliteConnection("Data Source=" + Application.dataPath + "/Database/dbgame.db");
                    break;
            }

        }

    }
}
