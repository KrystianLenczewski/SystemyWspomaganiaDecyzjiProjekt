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
    }
}
