using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Enums;

namespace SystemyWspomaganiaDecyzjiProjekt.Models
{
    public class DataStructure
    {
        private Dictionary<string, ColumnType> _columnTypes = new Dictionary<string, ColumnType>();
        private List<DataRow> rows = new List<DataRow>();

        public void InitializeColumnTypes(IEnumerable<string> columnNames)
        {
            foreach (var name in columnNames)
            {
                _columnTypes.Add(name, ColumnType.INT);
            }
        }

        public List<string> GetColumnNames()
        {
            return _columnTypes.Keys.ToList();
        }

        public List<List<string>> GetRowsRaw()
        {
            List<List<string>> result = new List<List<string>>();

            foreach (DataRow row in rows)
            {
                result.Add(row.GetRawData());
            }

            return result;
        }
        public IEnumerable<object> GetValuesDistinct(string columnName)
        {
            return  rows.Select(x => x.GetValue(columnName)).Distinct();
        }
         public void ImportData(DataTable dataTable)
        {
            foreach (System.Data.DataRow item in dataTable.Rows)
            {

                List<string> _cells = new List<string>();

                foreach (var cell in item.ItemArray)
                {
                    _cells.Add(cell.ToString());
                }

                //DataRow row = new DataRow(_cells);
                AddRow(_cells);
            }

        }

        public void AddEmptyColumn(string name, ColumnType columnType)
        {
            foreach (var row in rows)
            {
                row.AddCell(name);

            }
        }

        public void AddRow(List<string> values)
        {
            int i = 0;
            Dictionary<string, string> cells = new Dictionary<string, string>();
            foreach (var column in _columnTypes)
            {
                var cellValue = values.ElementAtOrDefault(i) ?? "";
                cells[column.Key] = cellValue;
                UpdateColumnType(column.Key, cellValue);
                i++;
            }

            rows.Add(new DataRow(cells));
        }


        public object GetValue(int rowNumber, string columnName)
        {
            return rows.ElementAtOrDefault(rowNumber)?.GetValue(columnName);
        }


        private void UpdateColumnType(string columnName, string value)
        {
            if (!int.TryParse(value, out _))
                _columnTypes[columnName] = ColumnType.DECIMAL;
            if (!decimal.TryParse(value, out _))
                _columnTypes[columnName] = ColumnType.STRING;
        }

    }
}