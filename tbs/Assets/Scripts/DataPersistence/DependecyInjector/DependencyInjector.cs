using Assets.Scripts.DataPersistence.DataServices;
using Assets.Scripts.DataPersistence.Interfaces;
using Assets.Scripts.DataPersistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.DependecyInjector
{
    public class DependencyInjector
    {
        private PlayerDataService _pds = null;
        private DataBaseConnector _dbc = null;

        public DependencyInjector()
        {
            _dbc = new DataBaseConnector();
            _pds = new PlayerDataService(_dbc);
        }

        public PlayerData GetAllPlayerData()
        {
            return _pds.GetPlayerData();
        }

        public bool UpdateTotalizedScore(int Score)
        {
            PlayerData pd = new PlayerData();
            pd.TotalScore = Score;

            return _pds.SaveDataToDB(pd);
        }

                


    }


}
