using Assets.Scripts.DataPersistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.Models
{
    public class LevelData : IDataModel
    {
        public int LevelID { get; set; }
        public int BestScore { get; set; }
        public int RoundTime { get; set; }
        public double PointMultiplier { get; set; }
        public int UnlockLevelAt { get; set; }

        public IDataModel ReturnModel()
        {
            return this;
        }
    }
}
