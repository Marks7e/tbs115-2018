using Assets.Scripts.DataPersistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.Models
{
    public class LevelSuccessTime : IDataModel
    {
        public int SuccessID { get; set; }
        public int LevelID { get; set; }
        public int SuccessTime { get; set; }

        public IDataModel ReturnModel()
        {
            return this;
        }
    }
}
