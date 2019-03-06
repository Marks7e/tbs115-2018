using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.DataServices
{
    public class LevelSuccessTimeService : IDataService
    {
        private DataBaseConnector _dbc;
        private LevelSuccessTime _LevelSuccessTime;
        private SqliteConnection _db;

        /*Queries a base de datos (LevelData)*/
        private string LEVEL_SUCCESS_TIME_DATA = "SELECT * FROM GAMEOPTIONS;";

        public LevelSuccessTimeService(DataBaseConnector dbc)
        {
            _dbc = dbc;
            _db = _dbc.getDBInstance();
        }

        public bool LoadAllDataFromDB()
        {
            throw new NotImplementedException();
        }

        public bool SaveDataToDB(IDataModel data)
        {
            throw new NotImplementedException();
        }
    }
}
