using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemyWspomaganiaDecyzjiProjekt.Enums;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Services;

namespace SystemyWspomaganiaDecyzjiProjekt.Controllers
{
    public class GroupingController : Controller
    {
        private readonly DataStructure _dataStructure;
        private readonly GroupingService _groupingService;
        public GroupingController(DataStructure dataStructure, GroupingService groupingService)
        {
            _dataStructure = dataStructure;
            _groupingService = groupingService;
        }

        [HttpPost]
        public IActionResult ClassificationKAverageMethod(KNNMetric kAverageMetric, int kKlastrow)
        {
           
            var result = _groupingService.ChooseKRandomObjects(kKlastrow);

            var labelForPoints = _groupingService.GetClusterLabelForPoints(result, kAverageMetric);
            _dataStructure.AddColumn(DataOperationsService.GenerateGenericColumnName(_dataStructure.GetColumnNames()), labelForPoints.Select(x => x.ToString()));

            string resultS = "";

            foreach (var item in result)
            {
                resultS += $"{item}\n";
            }

            return File(Encoding.UTF8.GetBytes(resultS), "text/plain", "Vectors.txt");
        }
    }
}
