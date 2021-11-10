using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Infrastructure;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels;
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

        public void DiscretizeVariable(DiscretizeVariableViewModel model)
        {
            List<string> dataRaw = _dataStructure.GetRowsRawForColumn(model.ColumnName);


            List<string> newColumnValues = StatisticOperation.DiscretizeVariable(dataRaw, model.DiscretizeIntervals);

            string newColumnName = GenerateGenericColumnName(_dataStructure.GetColumnNames());
            _dataStructure.AddColumn(newColumnName, newColumnValues);
        }




        public void NormalizeVariable(string columnName)
        {
            IEnumerable<double> dataRaw = _dataStructure.GetRowsRawForColumn(columnName).Select(x => double.Parse(x));
            List<string> newColumnValues = new List<string>();
            if (dataRaw.Any())
            {
                double avg = dataRaw.Average();
                double standardDeviation = StatisticOperation.StandardDeviation(dataRaw);

                foreach (var value in dataRaw)
                {
                    var result = (value - avg) / standardDeviation;
                    newColumnValues.Add(result.ToString("0.00"));
                }
                string newColumnName = GenerateGenericColumnName(_dataStructure.GetColumnNames());
                _dataStructure.AddColumn(newColumnName, newColumnValues);
            }

        }

        public void ChangeVariableRange(string columnName, decimal min, decimal max)
        {
            List<decimal> dataRaw = _dataStructure.GetRowsRawForColumn(columnName).Select(x => decimal.Parse(x)).ToList();
            List<string> newColumnValues = new List<string>();
            decimal minValue = dataRaw.Min();
            decimal maxValue = dataRaw.Max();

            decimal ratio = (max - min) / (maxValue - minValue);

            for(int i=0; i<dataRaw.Count(); i++)
            {
                decimal newValue = ((dataRaw[i] - minValue) * ratio) + min;
                newColumnValues.Add(newValue.ToString("0.00"));
            }
           
            string newColumnName = GenerateGenericColumnName(_dataStructure.GetColumnNames());
            _dataStructure.AddColumn(newColumnName, newColumnValues);


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
