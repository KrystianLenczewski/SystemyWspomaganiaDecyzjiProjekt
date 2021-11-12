using Accord.Math;
using Accord.Math.Distances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Enums;

namespace SystemyWspomaganiaDecyzjiProjekt.Infrastructure
{
    public class KnnDistanceEvaluator
    {
        private readonly List<List<decimal>> _dataWithClassificationClass;
        private readonly double[,] _covarianceMatrix;

        public KnnDistanceEvaluator(List<List<decimal>> dataWithClassificationClass)
        {
            _dataWithClassificationClass = dataWithClassificationClass;
            _covarianceMatrix = EvaluateCovarianceMatrix();
        }

        public decimal EvaluateDistance(List<decimal> x, List<decimal> y, KNNMetric knnMetric)
        {
            if (knnMetric == KNNMetric.METRYKA_EUKLIDESOWA)
                return EvaluateDistanceEuclideaMetric(x, y);
            else if (knnMetric == KNNMetric.METRYKA_MANHATTAN)
                return EvaluateDistanceManhattanMetric(x, y);
            else if (knnMetric == KNNMetric.METRYKA_CZEBYSZEWA)
                return EvaluateDistanceCzebyszewaMetric(x, y);
            else if (knnMetric == KNNMetric.METRYKA_MAHALANOBISA)
                return EvaluateDistanceMahalanobisaMetric(x, y);

            throw new NotImplementedException($"Metric specified in argument is not implemented.");
        }

        private decimal EvaluateDistanceEuclideaMetric(List<decimal> x, List<decimal> y)
        {
            if (x.Count != y.Count)
                throw new ArgumentException("Two vectors have different dimension");

            double sum = 0;
            for (int i = 0; i < x.Count; i++)
                sum += Math.Pow(decimal.ToDouble(x[i] - y[i]), 2);

            return (decimal)Math.Sqrt(sum);
        }

        private decimal EvaluateDistanceManhattanMetric(List<decimal> x, List<decimal> y)
        {
            if (x.Count != y.Count)
                throw new ArgumentException("Two vectors have different dimension");

            double sum = 0;
            for (int i = 0; i < x.Count; i++)
                sum += Math.Abs(decimal.ToDouble(x[i] - y[i]));

            return (decimal)sum;
        }

        private decimal EvaluateDistanceCzebyszewaMetric(List<decimal> x, List<decimal> y)
        {
            if (x.Count != y.Count)
                throw new ArgumentException("Two vectors have different dimension");

            decimal result = 0;
            for (int i = 0; i < x.Count; i++)
            {
                decimal distance = Math.Abs(x[i] - y[i]);
                if (result < distance)
                    result = distance;
            }

            return result;
        }

        private decimal EvaluateDistanceMahalanobisaMetric(List<decimal> x, List<decimal> y)
        {
            if (x.Count != y.Count)
                throw new ArgumentException("Two vectors have different dimension");

            var mahalanobis = Mahalanobis.FromCovarianceMatrix(_covarianceMatrix);
            // Then, you can compute the distance using:
            decimal distance = (decimal)mahalanobis.Distance(x.ToArray().ToDouble(), y.ToArray().ToDouble());
            return distance;
        }

        private double[,] EvaluateCovarianceMatrix()
        {
            int matrixDimension = _dataWithClassificationClass[0].Count;
            double[,] covarianceTable = new double[matrixDimension, matrixDimension];
            for (int i = 0; i < _dataWithClassificationClass[0].Count; i++)
            {
                for (int j = 0; j < _dataWithClassificationClass[0].Count; j++)
                {
                    List<decimal> listA = new();
                    List<decimal> listB = new();

                    for (int k = 0; k < _dataWithClassificationClass.Count; k++)
                    {
                        listA.Add(_dataWithClassificationClass[k][i]);
                        listB.Add(_dataWithClassificationClass[k][j]);
                    }


                    covarianceTable[i, j] = (double)StatisticOperation.CalculateCovariance(listA, listB, listA.Count);
                }
            }

            return covarianceTable;
        }
    }
}
