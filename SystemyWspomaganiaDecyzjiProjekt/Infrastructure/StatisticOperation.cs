using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Infrastructure
{
    public static class StatisticOperation
    {
        public static double StandardDeviation(IEnumerable<double> sequence)
        {
            double result = 0;

            if (sequence.Any())
            {
                double average = sequence.Average();
                double sum = sequence.Sum(d => Math.Pow(d - average, 2));
                result = Math.Sqrt((sum) / (sequence.Count() - 1));
            }
            return result;
        }
    }
}
