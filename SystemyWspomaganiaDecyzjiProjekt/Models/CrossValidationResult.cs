using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Models
{
    public class CrossValidationResult
    {
        public int SampleSize { get; set; }
        public int CorrectlyClassified { get; set; }
        public double CorrectnessPercent { get => SampleSize == 0 ? 0 : (double)CorrectlyClassified / SampleSize * 100; private set { } }

        public CrossValidationResult(int sampleSize, int correctlyClassified)
        {
            SampleSize = sampleSize;
            CorrectlyClassified = correctlyClassified;
        }
    }
}
