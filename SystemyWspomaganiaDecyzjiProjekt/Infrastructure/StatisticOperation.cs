using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels;

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

        public static List<string> DiscretizeVariable(List<string> dataRaw, List<DiscretizeInterval> discretizeIntervals)
        {
            if (!discretizeIntervals.Any())
                return dataRaw;

            List<string> newColumnValues = new List<string>();
            DiscretizeInterval lastInterval = discretizeIntervals.Last();
            foreach (var value in dataRaw.Select(x => decimal.Parse(x)))
            {
                foreach (var discretizeInterval in discretizeIntervals)
                {
                    bool isLastInterval = discretizeInterval.Label == lastInterval.Label;

                    if ((value >= discretizeInterval.MinValue && value < discretizeInterval.MaxValue)
                        || (value >= discretizeInterval.MinValue && value <= discretizeInterval.MaxValue && isLastInterval))
                    {
                        newColumnValues.Add(discretizeInterval.Label);
                    }

                }
            }

            return newColumnValues;
        }
    }
}
