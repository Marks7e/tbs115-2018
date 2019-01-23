using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.Models
{
    public class LevelData
    {
        public int LevelID { get; set; }
        public int BestScore { get; set; }
        public int ActualScore{ get; set; }
        public int RoundTime { get; set; }
        public double PointMultiplier { get; set; }
    }
}
