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

        public List<List<string>> GetPercentMinMaxValues(string columnName, decimal percent, DateLoadType dateLoadType)
        {
            List<Dictionary<string, string>> rowsRawWithHeaders = _dataStructure.GetRowsRawWithHeaders();

            if(_dataStructure.GetColumnNames().Contains(columnName))
            {
                if (dateLoadType == DateLoadType.MAX)
                    rowsRawWithHeaders = rowsRawWithHeaders.OrderByDescending(x => x[columnName]).ToList();
                else
                    rowsRawWithHeaders = rowsRawWithHeaders.OrderBy(x => x[columnName]).ToList();
            }
            return rowsRawWithHeaders.Take((int)(rowsRawWithHeaders.Count * (percent / 100))).Select(x=>x.Values.ToList()).ToList();
        }

        public List<Point2D> GetDataForChart2D(string xName, string yName, string className)
        {
            var rowsRawWithHeders = _dataStructure.GetRowsRawWithHeaders();
            List<Point2D> result = new List<Point2D>();
            foreach (var row in rowsRawWithHeders)
            {
                Point2D point2D = new Point2D
                {
                    X = decimal.Parse(row[xName]),
                    Y = decimal.Parse(row[yName]),
                    Class = row[className]
                };
                result.Add(point2D);
            }

            return result;
        }

        public List<Point3D> GetDataForChart3D(string xName, string yName, string zName, string className)
        {
            var rowsRawWithHeders = _dataStructure.GetRowsRawWithHeaders();
            List<Point3D> result = new List<Point3D>();
            foreach (var row in rowsRawWithHeders)
            {
                Point3D point3D = new Point3D
                {
                    X = decimal.Parse(row[xName]),
                    Y = decimal.Parse(row[yName]),
                    Z = decimal.Parse(row[zName]),
                    Class = row[className]
                };
                result.Add(point3D);
            }

            return result;
        }

       


    }
}
