using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Consts;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels;
using SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces;

namespace SystemyWspomaganiaDecyzjiProjekt.Controllers
{
    public class DataOperationController : Controller
    {
        private DataStructure _dataStructure;
        private IDataOperationsService _dataOperationsService;
        private IDataAnalizerService _dataAnalizerService;

        public DataOperationController(DataStructure dataStructure, IDataOperationsService dataOperationsService, IDataAnalizerService dataAnalizerService)
        {
            _dataStructure = dataStructure;
            _dataOperationsService = dataOperationsService;
            _dataAnalizerService = dataAnalizerService;
        }

        public IActionResult ChangeTextToNumbers(string textToNumberDropdown)
        {
            Dictionary<string, int> valuesMapping = _dataStructure.GetValuesDistinct(textToNumberDropdown).ToDictionary(k => k.ToString(), v => 0);
            return View(valuesMapping);
        }

        [HttpPost]
        public IActionResult ChangeTextToNumbers(Dictionary<string, int> mapping, string columnName)
        {
            try
            {
                if (!mapping.Values.GroupBy(x => x).Any(g => g.Count() > 1))
                {
                    _dataOperationsService.ChangeTextToNumbers(columnName, mapping);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("DuplicatedNumbers", ModelError.DUPLICATED_MAPPING_NUMBERS);
            }
            catch (Exception)
            {
                ModelState.AddModelError("InternalError", GenericError.SOMETHING_WENT_WRONG);
            }

            return View(mapping);
        }

        public IActionResult DiscretizeVariable(string columnDiscretisationDropdown, int countOfIntervals)
        {
            List<DiscretizeInterval> discretizeIntervals = _dataAnalizerService.GetEqualDiscretizeIntervals(columnDiscretisationDropdown, countOfIntervals);
            return View(new DiscretizeVariableViewModel(columnDiscretisationDropdown, discretizeIntervals));
        }

        [HttpPost]
        public IActionResult DiscretizeVariable(DiscretizeVariableViewModel model)
        {

            if (model is not null)
            {
                _dataOperationsService.DiscretizeVariable(model);
                return RedirectToAction("Index", "Home");

            }

            return View(model);
        }

        [HttpPost]
        public IActionResult NormalizeVariable(string normalizationDropdown)
        {

            _dataOperationsService.NormalizeVariable(normalizationDropdown);

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult ChangeVariableRange(string changeVariableRangeDropdown, decimal min, decimal max)
        {

            _dataOperationsService.ChangeVariableRange(changeVariableRangeDropdown, min, max);

            return RedirectToAction("Index", "Home");
        }
        
    }
}
