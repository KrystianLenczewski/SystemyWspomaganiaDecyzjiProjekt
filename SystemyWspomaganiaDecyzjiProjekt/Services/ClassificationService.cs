using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Enums;
using SystemyWspomaganiaDecyzjiProjekt.Infrastructure;
using SystemyWspomaganiaDecyzjiProjekt.Models;
using SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels;
using SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces;

namespace SystemyWspomaganiaDecyzjiProjekt.Services
{
    public class ClassificationService : IClassificationService
    {
        private readonly DataStructure _dataStructure;

        public ClassificationService(DataStructure dataStructure)
        {
            _dataStructure = dataStructure;
        }

        public string ClassifyNewObjectUsingKNN(ClassificationModeVm model)
        {
            List<decimal> values = model.RowValues.Values.Select(s => decimal.Parse(s)).ToList();
            KnnClassifier knnClassifier = new(GetDataPreparedForKNNClassifier(model.ClassificationVm.KlasaDecyzyjnaDropdown));
            return knnClassifier.GetNearestNeigboursClass(values, model.ClassificationVm.KNajblizszychSasiadow, model.ClassificationVm.KNNMetric);
        }

        private List<(List<decimal>, string)> GetDataPreparedForKNNClassifier(string classColumn)
        {
            List<string> numericalColumnNames = _dataStructure.GetColumnNames(ColumnType.DECIMAL, ColumnType.INT);
            List<(List<decimal>, string)> result = new();

            foreach (var row in _dataStructure.GetRowsRawWithHeaders())
            {
                List<decimal> values = new();
                foreach (var numericalColumnName in numericalColumnNames)
                    values.Add(decimal.Parse(row[numericalColumnName]));

                result.Add((values, row[classColumn]));
            }

            return result;
        }
    }
}
