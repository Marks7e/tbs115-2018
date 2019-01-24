using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.DataPersistence.DataServices
{
    public class PlayerDataService : IDataService
    {
        private GameDataPersistence _gdp;

        /*Queries a base de datos (PlayerData)*/
        private const string PLAYER_ALL_DATA = "SELECT * FROM PLAYERDATA";

        public PlayerDataService(GameDataPersistence gdp)
        {
            _gdp = gdp;
        }

        public DataTable LoadAllDataFromDB()
        {
            return _gdp.ExecuteQuery(PLAYER_ALL_DATA);
        }

        public bool SaveDataToDB()
        {
            throw new NotImplementedException();
        }
    }
}

