using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Enums;

namespace SystemyWspomaganiaDecyzjiProjekt.Infrastructure
{
    public class KnnClassifier
    {
        private readonly List<(List<decimal>, string)> _data;

        public KnnClassifier(List<(List<decimal>, string)> data)
        {
            _data = data;
        }

        public string GetNearestNeigboursClass(List<decimal> values, int k, KNNMetric metric)
        {
            var kNearestNeighboursDistance = GetKNearestNeighboursDistance(values, k, metric);
            return GetClassBasedOfNN(kNearestNeighboursDistance);
        }

        private static string GetClassBasedOfNN(List<(decimal, string)> kNearestNeighboursDistance)
        {
            var countGroups = kNearestNeighboursDistance.GroupBy(k => k.Item2).Select(s => (s.Key, s.Count())).OrderByDescending(o => o.Item2).ToList();
            int groupMaxCount = countGroups[0].Item2;
            var groupsWithMaxCount = countGroups.Where(w => w.Item2 == groupMaxCount);

            if (groupsWithMaxCount.Count() != 1)
                return GetNearestDistance(groupsWithMaxCount.Select(s => s.Key), kNearestNeighboursDistance).Item2;
            else
                return countGroups[0].Key;
        }

        private static (decimal, string) GetNearestDistance(IEnumerable<string> classes, List<(decimal, string)> kNearestNeighboursDistance)
        {
            kNearestNeighboursDistance = kNearestNeighboursDistance.OrderBy(o => o.Item1).ToList();
            List<(decimal, string)> resultDistances = new();
            foreach (string classString in classes)
            {
                var kNearestNeighbourDistance = kNearestNeighboursDistance.FirstOrDefault(f => f.Item2 == classString);
                if(kNearestNeighbourDistance.Item2 is not null)
                    resultDistances.Add(kNearestNeighbourDistance);
            }

            return resultDistances.OrderBy(o => o.Item1).First();
        }

        private List<(decimal, string)> GetKNearestNeighboursDistance(List<decimal> values, int k, KNNMetric metric)
        {
            List<(decimal, string)> result = new();
            foreach (var row in _data)
            {
                if (metric == KNNMetric.METRYKA_EUKLIDESOWA)
                    result.Add((KnnMetrics.EvaluateDistanceEuclideaMetric(values, row.Item1), row.Item2));
            }

            return result.OrderBy(o => o.Item1).Take(k).ToList();
        }
    }
}
