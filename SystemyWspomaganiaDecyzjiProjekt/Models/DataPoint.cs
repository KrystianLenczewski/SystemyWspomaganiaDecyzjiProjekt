using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Models
{
    public class DataPoint
    {
        public int Id { get; set; }
        public Dictionary<string, string> RowRawWithHeaders { get; set; }

        public List<decimal> GetNumericalData(List<string> numericalColumnNames)
        {
            List<decimal> values = new();
            foreach (var numericalColumnName in numericalColumnNames)
                values.Add(decimal.Parse(RowRawWithHeaders[numericalColumnName].Replace('.',',')));

            return values;
        }

        public override string ToString()
        {
            string result = "";
            foreach (var item in RowRawWithHeaders)
            {
                result += $"{item.Key} : {item.Value}, ";
            }
            return result;
        }
    }
}
