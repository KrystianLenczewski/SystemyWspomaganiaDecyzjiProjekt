using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Enums;

namespace SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels
{
    public class ClassificationModeVm
    {
        public ClassificationVm ClassificationVm { get; set; }
        public Dictionary<string, ColumnType> ColumnTypes { get; set; }
        public Dictionary<string, string> RowValues { get; set; }
    }
}
