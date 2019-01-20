using Assets.Scripts.DataPersistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.DataServices
{
    public class PlayerDataService : IDataService
    {

        public GameDataPersistence gdp = null;

        public PlayerDataService(GameDataPersistence gdp)
        {
            this.gdp = gdp;
        }

        public bool SaveDataToFile()
        {
            throw new NotImplementedException();
        }

        public bool LoadDataFromFile()
        {
            throw new NotImplementedException();
        }


    }
}
