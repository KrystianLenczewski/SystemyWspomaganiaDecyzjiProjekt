using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels
{
    public class Chart2DVm
    {
        public string XColumnName { get; set; }
        public string YColumnName { get; set; }
        public string ClassName { get; set; }
        public Dictionary<string, string> ClassColorMapping { get; set; }
        public List<Point2D> ClassPoints { get; set; }
    }
}
