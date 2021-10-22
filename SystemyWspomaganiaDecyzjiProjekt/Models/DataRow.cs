using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Models
{
    public class DataRow
    {
        private Dictionary<string, string> _cells = new Dictionary<string, string>();

        public DataRow(Dictionary<string, string> cells)
        {
            _cells = cells;
        }
        public void AddCell(string name, string value = "")
        {
            if (!_cells.ContainsKey(name))
            {
                _cells.Add(name, value);
            }
        }

        public string GetValue(string cellName)
        {
            return _cells.ContainsKey(cellName) ? _cells[cellName] : "";
        }
    }
}