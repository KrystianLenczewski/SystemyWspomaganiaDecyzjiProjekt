using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels;

namespace SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces
{
    public interface IDataOperationsService
    {
        void ChangeTextToNumbers(string columnName, Dictionary<string, int> mapping);
        void NormalizeVariable(string columnName);
        void DiscretizeVariable(DiscretizeVariableViewModel model);
        void ChangeVariableRange(string columnName, decimal min, decimal max);
    }
}
