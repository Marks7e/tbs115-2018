using Assets.Scripts.DataPersistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.Models
{
    public class PlayerData : IDataModel
    {
        public int PlayerID { get; set; }
        public int TotalScore { get; set; }

        public IDataModel ReturnModel()
        {
            return this;
        }
    }
}
