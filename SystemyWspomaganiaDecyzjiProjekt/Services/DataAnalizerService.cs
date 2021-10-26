using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Enums;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels;
using SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces;

namespace SystemyWspomaganiaDecyzjiProjekt.Services
{
    public class DataAnalizerService : IDataAnalizerService
    {
        private readonly DataStructure _dataStructure;

        public DataAnalizerService(DataStructure dataStructure)
        {
            _dataStructure = dataStructure;
        }

        public List<DiscretizeInterval> GetEqualDiscretizeIntervals(string columnName, int intervalCount)
        {
            if (_dataStructure.GetColumnNames().Contains(columnName) && _dataStructure.GetColumnType(columnName) != ColumnType.STRING)
            {
                IEnumerable<decimal> distinctValues = _dataStructure.GetValuesDistinct(columnName).Select(s => decimal.Parse(s.ToString()));
                var orderedIntervalBorders = GetOrderedIntervalBorders(distinctValues.Min(), distinctValues.Max(), intervalCount);
                List<DiscretizeInterval> discretizeIntervals = new();

                for (int i = 0; i < orderedIntervalBorders.Count - 1; i++)
                    discretizeIntervals.Add(new DiscretizeInterval { MinValue = orderedIntervalBorders[i], MaxValue = orderedIntervalBorders[i + 1] });

                return discretizeIntervals;
            }

            return new List<DiscretizeInterval>();
        }

        private static List<decimal> GetOrderedIntervalBorders(decimal min, decimal max, int intervalCount)
        {
            List<decimal> result = new List<decimal> { min };
            decimal differential = (max - min) / intervalCount;

            for (int i = 1; i < intervalCount; i++)
            {
                decimal previousBorder = result[i - 1];
                result.Add(previousBorder + differential);
            }

            result.Add(max);
            return result;
        }
    }
}
