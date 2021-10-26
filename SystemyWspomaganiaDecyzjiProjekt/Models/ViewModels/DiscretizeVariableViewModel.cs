using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels
{
    public class DiscretizeVariableViewModel
    {
        public string ColumnName { get; set; }
        public List<DiscretizeInterval> DiscretizeIntervals { get; set; }

        public DiscretizeVariableViewModel(string columnName, List<DiscretizeInterval> discretizeIntervals)
        {
            ColumnName = columnName;
            DiscretizeIntervals = discretizeIntervals;
        }

        public DiscretizeVariableViewModel()
        {

        }
    }
}
