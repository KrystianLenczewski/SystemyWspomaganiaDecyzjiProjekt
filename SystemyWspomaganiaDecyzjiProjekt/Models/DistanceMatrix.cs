using Accord.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Models
{
    public class DistanceMatrix
    {
        private readonly decimal[,] _distances;

        public DistanceMatrix(decimal[,] distances)
        {
            _distances = distances;
        }

        public decimal GetDistance(int index1, int index2) => _distances[index1, index2];

        public List<int> GetKNearestNeighbourIndexes(int elementIndex, int neighbourCount)
        {
            decimal[,] distances = (decimal[,])_distances.Clone();

            var row = distances.GetRow(elementIndex);
            List<int> nearestNeighbourIndexes = new();
            while (nearestNeighbourIndexes.Count != neighbourCount)
            {
                decimal minValue = row.Min();
                var minIndex = row.IndexOf(minValue);
                row[minIndex] = decimal.MaxValue;
                if (minValue > 0)
                    nearestNeighbourIndexes.Add(minIndex);
            }

            return nearestNeighbourIndexes;
        }
    }
}
