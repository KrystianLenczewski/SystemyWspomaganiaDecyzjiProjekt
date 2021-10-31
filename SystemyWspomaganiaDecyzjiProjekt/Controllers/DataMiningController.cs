using Microsoft.AspNetCore.Mvc;
using SystemyWspomaganiaDecyzjiProjekt.Enums;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces;

namespace SystemyWspomaganiaDecyzjiProjekt.Controllers
{
    public class DataMiningController : Controller
    {
        private readonly IDataAnalizerService _dataAnalizerService;
        private DataStructure _dataStructure;
        public DataMiningController(IDataAnalizerService dataAnalizerService, DataStructure dataStructure)
        {
            _dataAnalizerService = dataAnalizerService;
            _dataStructure = dataStructure;
        }

       

        [HttpPost]
        public IActionResult GetPercentMinMaxValues(string percentMinMaxValuesDropdown, decimal percent, DateLoadType loadType)
        {
            var result = _dataAnalizerService.GetPercentMinMaxValues(percentMinMaxValuesDropdown, percent, loadType);

            DataTableVm dataTableVm = new DataTableVm();

            dataTableVm.Rows = result;
            dataTableVm.ColumnNames = _dataStructure.GetColumnNames();

            return View("Index", dataTableVm);
        }
    }
}
