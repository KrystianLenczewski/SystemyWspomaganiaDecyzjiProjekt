using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces;

namespace SystemyWspomaganiaDecyzjiProjekt.Services
{
    public class DataOperationsService : IDataOperationsService
    {
        private readonly DataStructure _dataStructure;

        public DataOperationsService(DataStructure dataStructure)
        {
            _dataStructure = dataStructure;
        }

        public void ChangeTextToNumbers(string columnName, Dictionary<string, int> mapping)
        {
            List<string> dataRaw = _dataStructure.GetRowsRawForColumn(columnName);
            if (dataRaw.Any())
            {
                List<string> mappedColumnValues = new();
                foreach (var stringValue in dataRaw)
                {
                    if (mapping.TryGetValue(stringValue, out int mappedValue))
                        mappedColumnValues.Add(mappedValue.ToString());
                    else
                        mappedColumnValues.Add(stringValue);
                }

                string newColumnName = GenerateGenericColumnName(_dataStructure.GetColumnNames());
                _dataStructure.AddColumn(newColumnName, mappedColumnValues);
            }
        }

        private static string GenerateGenericColumnName(List<string> existingColumnNames)
        {
            int counter = 0;
            while (true)
            {
                string columnName = $"Column{counter++}";
                if (!existingColumnNames.Contains(columnName))
                    return columnName;
            }
        }
    }
}
