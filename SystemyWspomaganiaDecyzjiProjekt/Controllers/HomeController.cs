using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Infrastructure.Importers;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces;

namespace SystemyWspomaganiaDecyzjiProjekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataStructure _dataStructure;
        public HomeController(DataStructure dataStructure)
        {
            _dataStructure = dataStructure;
        }

        public IActionResult Index()
        {
            DataTableVm dataTableVm = new DataTableVm
            {
                ColumnNames = _dataStructure.GetColumnNames(),
                Rows = _dataStructure.GetRowsRaw()
            };

            return View(dataTableVm);
        }

        
    }
}
