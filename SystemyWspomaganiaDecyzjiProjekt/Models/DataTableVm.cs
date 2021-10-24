using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Models
{
    public class DataTableVm
    {
        public List<string> ColumnNames { get; set; }
        public List<List<string>> Rows { get; set; }
    }
}
