using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Consts;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces;

namespace SystemyWspomaganiaDecyzjiProjekt.Controllers
{
    public class DataOperationController : Controller
    {
        private DataStructure _dataStructure;
        private IDataOperationsService _dataOperationsService;

        public DataOperationController(DataStructure dataStructure, IDataOperationsService dataOperationsService)
        {
            _dataStructure = dataStructure;
            _dataOperationsService = dataOperationsService;
        }

        public IActionResult ChangeTextToNumbers(string textToNumberDropdown)
        {
            Dictionary<string, int> valuesMapping = _dataStructure.GetValuesDistinct(textToNumberDropdown).ToDictionary(k => k.ToString(), v => 0);
            return View(valuesMapping);
        }

        [HttpPost]
        public IActionResult ChangeTextToNumbers(Dictionary<string, int> mapping, string columnName)
        {
            if (!mapping.Values.GroupBy(x => x).Any(g => g.Count() > 1))
            {
                _dataOperationsService.ChangeTextToNumbers(columnName, mapping);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("DuplicatedNumbers", ModelError.DUPLICATED_MAPPING_NUMBERS);
            return View(mapping);
        }
    }
}
