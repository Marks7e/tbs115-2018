using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

namespace Assets.Scripts.DataPersistence
{
    public class DataBaseConnector
    {
        private SqliteConnection sqlite;

        public DataBaseConnector()
        {
            sqlite = new SqliteConnection("Data Source="+ Application.dataPath + "/Database/dbgame.db");
        }

        public DataTable ExecuteQuery(string query)
        {
            SqliteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SqliteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SqliteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SqliteException ex)
            {
                Debug.LogError(ex.Message);
            }
            sqlite.Close();
            return dt;
        }

    }
}
