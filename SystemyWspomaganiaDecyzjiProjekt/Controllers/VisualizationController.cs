using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels;
using SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces;

namespace SystemyWspomaganiaDecyzjiProjekt.Controllers
{
    public class VisualizationController : Controller
    {
        private DataStructure _dataStructure;
        private readonly IDataAnalizerService _dataAnalizerService;
        public VisualizationController(DataStructure dataStructure, IDataAnalizerService dataAnalizerService)
        {
            _dataStructure = dataStructure;
            _dataAnalizerService = dataAnalizerService;
        }
        public IActionResult Show2DChart(string xAxisDropdown, string yAxisDropdown, string classDropdown)
        {
            Chart2DVm chart2DVm = new Chart2DVm
            {
                XColumnName = xAxisDropdown,
                YColumnName = yAxisDropdown,
                ClassName = classDropdown,
                ClassColorMapping = _dataStructure.GetValuesDistinct(classDropdown).Select(x => x.ToString()).ToDictionary(x => x, y => string.Empty)
        };

            return View("ColorSelecting", chart2DVm);
        }

        [HttpPost]
        public IActionResult Show2DChart(Chart2DVm model)
        {
            List<Point2D> points2D = _dataAnalizerService.GetDataForChart2D(model.XColumnName, model.YColumnName, model.ClassName);
            model.ClassPoints = points2D;

            return View(model);
        }

        public IActionResult Show3DChart(string xAxisDropdown, string yAxisDropdown, string zAxisDropdown, string classDropdown)
        {
            Chart3DVm chart3DVm = new Chart3DVm
            {
                XColumnName = xAxisDropdown,
                YColumnName = yAxisDropdown,
                ZColumnName = zAxisDropdown,
                ClassName = classDropdown,
                ClassColorMapping = _dataStructure.GetValuesDistinct(classDropdown).Select(x => x.ToString()).ToDictionary(x => x, y => string.Empty)
            };

            return View("ColorSelectingForChart3D", chart3DVm);
        }

        [HttpPost]
        public IActionResult Show3DChart(Chart3DVm model)
        {
            List<Point3D> points3D = _dataAnalizerService.GetDataForChart3D(model.XColumnName, model.YColumnName, model.ZColumnName, model.ClassName);
            model.ClassPoints = points3D;

            return View(model);
        }


        [HttpPost]
        public IActionResult ShowHistogram(string columnNameDropdown, int intervalCount)
        {
            

            return View(_dataAnalizerService.GetDataForHistogram(columnNameDropdown, intervalCount));
        }
        
    }
}
