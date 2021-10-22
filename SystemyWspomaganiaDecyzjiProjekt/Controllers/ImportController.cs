using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces;

namespace SystemyWspomaganiaDecyzjiProjekt.Controllers
{
    public class ImportController : Controller
    {
        private readonly IImportService _importService;
        private readonly DataStructure _dataStructure;
        public ImportController(IImportService importService, DataStructure dataStructure)
        {
            _importService = importService;
            _dataStructure = dataStructure;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            if(file is not null)
            {
                DataTable dataTable = _importService.ImportData(file);
                _dataStructure.ImportData(dataTable);
            }
            return View();
        }
    }
}
