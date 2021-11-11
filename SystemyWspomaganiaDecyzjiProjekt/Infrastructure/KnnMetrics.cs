using Accord.Math;
using Accord.Math.Distances;
using MathNet.Numerics.LinearAlgebra.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Infrastructure
{
    public static class KnnMetrics
    {
        public static decimal EvaluateDistanceEuclideaMetric(List<decimal> x, List<decimal> y)
        {
            if (x.Count != y.Count)
                throw new ArgumentException("Two vectors have different dimension");

            double sum = 0;
            for (int i = 0; i < x.Count; i++)
                sum += Math.Pow(decimal.ToDouble(x[i] - y[i]), 2);

            return (decimal)Math.Sqrt(sum);
        }

        public static decimal EvaluateDistanceManhattanMetric(List<decimal> x, List<decimal> y)
        {
            if (x.Count != y.Count)
                throw new ArgumentException("Two vectors have different dimension");

            double sum = 0;
            for (int i = 0; i < x.Count; i++)
                sum += Math.Abs(decimal.ToDouble(x[i] - y[i]));

            return (decimal)sum;
        }

        public static decimal EvaluateDistanceCzebyszewaMetric(List<decimal> x, List<decimal> y)
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

        public static decimal EvaluateDistanceMahalanobisaMetric(List<decimal> x, List<decimal> y, List<(List<decimal>, string)> _data)
        {
            if (x.Count != y.Count)
                throw new ArgumentException("Two vectors have different dimension");


         



            decimal result = 0;

            double[,] covarianceTable = new double[x.Count, x.Count];
            for (int i = 0; i < _data[0].Item1.Count; i++)
            {
                for (int j = 0; j < _data[0].Item1.Count; j++)
                {
                    List<decimal> listA = new();
                    List<decimal> listB = new();

                    for (int k = 0; k < _data.Count; k++)
                    {
                        listA.Add(_data[k].Item1[i]);
                        listB.Add(_data[k].Item1[j]);
                    }

                
                    covarianceTable[i, j] = (double)calculateCovariance(listA, listB, listA.Count);
                }
               
            }

            var mahalanobis = Mahalanobis.FromCovarianceMatrix(covarianceTable);
            // Then, you can compute the distance using:
            decimal distance = (decimal)mahalanobis.Distance(x.ToArray().ToDouble(), y.ToArray().ToDouble());
            return distance;
        }

        static decimal mean(List<decimal> arr, int n)
        {
            decimal sum = 0;

            for (int i = 0; i < n; i++)
                sum = sum + arr[i];

            return sum / n;
        }

        // Function to find covariance.
        static decimal calculateCovariance(List<decimal> arr1,
                            List<decimal> arr2, int n)
        {
            decimal sum = 0;

            for (int i = 0; i < n; i++)
                sum = sum + (arr1[i] - mean(arr1, n)) *
                                (arr2[i] - mean(arr2, n));
            return sum / (n - 1);
        }






        public static double[,] twoPassCovariance(List<decimal> data1, List<decimal> data2)
        {
            int n = data1.Count;

            decimal mean1 = data1.Average();
            decimal mean2 = data2.Average();

            decimal[,] covariance = new decimal[data1.Count, data2.Count / 2];
            decimal x;

            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < n; k++)
                {
                    decimal a = data1[i] - mean1;
                    decimal b = data2[k] - mean2;
                    x = a * b;
                    covariance[i, k] = x;
                }
            }

            covariance.Multiply(1 / n);
            return covariance.ToDouble();
        }
    }
}
