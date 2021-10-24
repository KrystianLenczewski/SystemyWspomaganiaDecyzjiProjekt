using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Extensions
{
    public static class DataTableExtension
    {
        public static IEnumerable<string> GetColumnNames(this DataTable dataTable)
        {
            foreach (DataColumn item in dataTable.Columns)
            {
                yield return item.ColumnName;
            }   
        }
    }
}
