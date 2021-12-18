using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Enums;
using SystemyWspomaganiaDecyzjiProjekt.Infrastructure;
using SystemyWspomaganiaDecyzjiProjekt.Models;

namespace SystemyWspomaganiaDecyzjiProjekt.Services
{
    public class GroupingService
    {
        private readonly DataStructure _dataStructure;
        private static Random rnd = new Random();
        public GroupingService(DataStructure dataStructure)
        {
            _dataStructure = dataStructure;
        }

        public List<DataPoint> ChooseKRandomObjects(int kKlastrow)
        {
            
            List<DataPoint> list = _dataStructure.GetPointsWithId().OrderBy(x => rnd.Next()).Take(kKlastrow).ToList();

            return list;
        }

        public List<int> GetClusterLabelForPoints(List<DataPoint> data, KNNMetric kAverageMetric)
        {

            KnnDistanceEvaluator knnDistanceEvaluator = new KnnDistanceEvaluator(GetNumericalRawData());

            List<int> result = new();

            List<List<decimal>> list = GetNumericalRawData();
            List<string> numericalColumnNames = _dataStructure.GetColumnNames(ColumnType.DECIMAL, ColumnType.INT);


            for (int i = 0; i < list.Count(); i++)
            {
                List<decimal> distances = new();
                foreach (var point in data)
                {
                    if (point.Id == i)
                        distances.Add(decimal.MaxValue);
                    else
                        distances.Add(knnDistanceEvaluator.EvaluateDistance(point.GetNumericalData(numericalColumnNames), list[i], kAverageMetric));

                }

                if (data.Any(x => x.Id == i))
                    result.Add(data.FindIndex(x => x.Id == i));
                else
                    result.Add(distances.IndexOf(distances.Min()));

            }

            return result;

        }

        private List<List<decimal>> GetNumericalRawData()
        {
            List<string> numericalColumnNames = _dataStructure.GetColumnNames(ColumnType.DECIMAL, ColumnType.INT);
            List<List<decimal>> result = new();

            foreach (var row in _dataStructure.GetRowsRawWithHeaders())
            {
                List<decimal> values = new();
                foreach (var numericalColumnName in numericalColumnNames)
                    values.Add(decimal.Parse(row[numericalColumnName]));

                result.Add(values);
            }

            return result;
        }

    }
}
