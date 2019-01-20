using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataPersistence.Interfaces
{
    public interface IDataService
    {
        bool SaveDataToFile();
        bool LoadDataFromFile();
    }
}
