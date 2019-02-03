using Assets.Scripts.DataPersistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.Models
{
    public class QuestionData : IDataModel
    {
        public int QuestionID { get; set; }
        public int RealmNumber { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public IDataModel ReturnModel()
        {
            return this;
        }
    }
}
