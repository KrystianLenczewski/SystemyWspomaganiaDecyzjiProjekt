using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Enums;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels;

namespace SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces
{
    public interface IDataAnalizerService
    {
        List<DiscretizeInterval> GetEqualDiscretizeIntervals(string columnName, int intervalCount);
        List<List<string>> GetPercentMinMaxValues(string columnName, decimal percent, DateLoadType dateLoadType);
        List<Point2D> GetDataForChart2D(string xName, string yName, string className);
        List<Point3D> GetDataForChart3D(string xName, string yName, string zName, string className);
        SortedDictionary<string, int> GetDataForHistogram(string columnName, int intervalCount);
    }
}
