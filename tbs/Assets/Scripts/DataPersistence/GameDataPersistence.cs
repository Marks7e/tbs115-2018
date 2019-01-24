using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameDataPersistence
{
    private SqliteConnection db;

    public GameDataPersistence(SqliteConnection sqlite)
    {
        db = sqlite;
    }

    public DataTable ExecuteQuery(string query)
    {
        SqliteDataAdapter ad;
        DataTable dt = new DataTable();

        try
        {
            ad = new SqliteDataAdapter(SetupQuery(query));
            ad.Fill(dt); //fill the datasource
        }
        catch (SqliteException ex)
        {
            Debug.LogError(ex.Message);
        }
        db.Close();
        return dt;
    }
    public bool ExecuteNonQuery(string query)
    {
        try
        {
            SqliteCommand cmd = SetupQuery(query);
            return cmd.ExecuteNonQuery() > 0 ? true : false;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
    }

    private SqliteCommand SetupQuery(string query)
    {
        try
        {
            SqliteCommand cmd;
            db.Open();  //Initiate connection to the db
            cmd = db.CreateCommand();
            cmd.CommandText = query;
            return cmd;
        }
        catch (SqliteException e)
        {
            Debug.LogError(e.Message);
            return null;
        }
    }


}
