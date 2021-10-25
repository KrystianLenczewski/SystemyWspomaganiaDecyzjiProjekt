using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces
{
    public interface IDataOperationsService
    {
        void ChangeTextToNumbers(string columnName, Dictionary<string, int> mapping);
    }
}
