using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.Interfaces
{
     public interface IDataService
    {
        bool SaveDataToDB();
        DataTable LoadAllDataFromDB();
    }
}
