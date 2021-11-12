using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels;

namespace SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces
{
    public interface IClassificationService
    {
        string ClassifyNewObjectUsingKNN(ClassificationModeVm model);
        CrossValidationResult EvaluateKNNClassificationQuality(ClassificationVm classificationVm);
    }
}
