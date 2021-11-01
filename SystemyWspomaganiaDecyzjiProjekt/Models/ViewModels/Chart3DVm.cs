using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels
{
    public class Chart3DVm
    {
        public string XColumnName { get; set; }
        public string YColumnName { get; set; }
        public string ZColumnName { get; set; }
        public string ClassName { get; set; }
        public Dictionary<string, string> ClassColorMapping { get; set; }
        public List<Point3D> ClassPoints { get; set; }
    }
}
