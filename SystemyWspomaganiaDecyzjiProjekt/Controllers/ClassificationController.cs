using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemyWspomaganiaDecyzjiProjekt.Enums;
using SystemyWspomaganiaDecyzjiProjekt.Infrastructure;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels;
using SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces;

namespace SystemyWspomaganiaDecyzjiProjekt.Controllers
{
    public class ClassificationController : Controller
    {
        private readonly DataStructure _dataStructure;
        private readonly IClassificationService _classificationService;

        public ClassificationController(DataStructure dataStructure, IClassificationService classificationService)
        {
            _dataStructure = dataStructure;
            _classificationService = classificationService;
        }

        [HttpPost]
        public IActionResult ClassificationKNNMethod(KNNMetric kNNMetric, int kNajblizszychSasiadow, string klasaDecyzyjnaDropdown, int tryb)
        {
          
            ClassificationVm classificationVm = new ClassificationVm
            {
                KNNMetric = kNNMetric,
                KNajblizszychSasiadow = kNajblizszychSasiadow,
                KlasaDecyzyjnaDropdown = klasaDecyzyjnaDropdown
            };


            ClassificationModeVm classificationModeVm = new ClassificationModeVm();
            classificationModeVm.ClassificationVm = classificationVm;

            Dictionary<string, ColumnType> columnTypes = new Dictionary<string, ColumnType>();
            Dictionary<string, string> rowValues = new Dictionary<string, string>();

            if (tryb == 1)
            {
                List<string> columnNames = _dataStructure.GetColumnNames();

                foreach (var columnName in columnNames.Where(x=>x != classificationModeVm.ClassificationVm.KlasaDecyzyjnaDropdown))
                {
                    columnTypes.Add(columnName, _dataStructure.GetColumnType(columnName));
                    rowValues.Add(columnName, "");
                }

                classificationModeVm.ColumnTypes = columnTypes;
                classificationModeVm.RowValues = rowValues;

                return View("ClassificationMode", classificationModeVm);
            }

            return RedirectToAction("ClassificationEvaluation", classificationVm);
        }

  

        [HttpPost]
        public IActionResult ClassificationMode(ClassificationModeVm model)
        {
            string newObjectClass = _classificationService.ClassifyNewObjectUsingKNN(model);
            Dictionary<string, string> newRow = new Dictionary<string, string>(model.RowValues);
            newRow.Add(model.ClassificationVm.KlasaDecyzyjnaDropdown, newObjectClass);
            _dataStructure.AddRow(newRow);

            return RedirectToAction("Index","Home");
        }

        public IActionResult ClassificationEvaluation(ClassificationVm classificationVm)
        {
            
            CrossValidationResult crossValidationResult = new CrossValidationResult();
            List<string> classificationResult = new List<string>();
            for(int i=1;i<=132;i++)
            {
                classificationVm.KNajblizszychSasiadow = i;
                crossValidationResult = _classificationService.EvaluateKNNClassificationQuality(classificationVm);
                classificationResult.Add($"{i}\t{crossValidationResult.CorrectlyClassified}\t{crossValidationResult.SampleSize}\t{crossValidationResult.CorrectnessPercent.ToString("0.##")}");
                
            }
            System.IO.File.WriteAllLines(@"C:\Users\Krystian\Desktop\SystemyyBazDanych_Dane\classificationResult.txt", classificationResult);
            return View(crossValidationResult);
        }
    }
}
