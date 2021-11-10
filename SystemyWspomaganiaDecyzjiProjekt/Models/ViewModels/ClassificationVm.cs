using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Enums;

namespace SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels
{
    public class ClassificationVm
    {
        public KNNMetric KNNMetric { get; set; }
        public int KNajblizszychSasiadow { get; set; }
        public string KlasaDecyzyjnaDropdown { get; set; }
    }
}
