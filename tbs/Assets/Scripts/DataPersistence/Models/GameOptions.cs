using Assets.Scripts.DataPersistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.Models
{
    public class GameOptions : IDataModel
    {
        public int QuestionID { get; set; }
        public string Parameter { get; set; }
        public string PValue { get; set; }

        public IDataModel ReturnModel()
        {
            return this;
        }
    }
}
