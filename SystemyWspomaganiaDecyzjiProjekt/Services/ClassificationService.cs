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

        public CrossValidationResult EvaluateKNNClassificationQuality(ClassificationVm classificationVm)
        {
            DistanceMatrix distanceMatrix = GetDistancesBetweenObjects(classificationVm.KNNMetric);
            List<(List<decimal>, string)> dataRows = GetDataPreparedForKNNClassifier(classificationVm.KlasaDecyzyjnaDropdown);
            int correctlyClassified = 0;

            for(int i = 0; i< dataRows.Count; i++)
            {
                List<int> kNearesNeighbourIndexes = distanceMatrix.GetKNearestNeighbourIndexes(i, classificationVm.KNajblizszychSasiadow);
                List<(decimal, string)> nearestNeighbours = new();
                foreach (var index in kNearesNeighbourIndexes)
                    nearestNeighbours.Add((distanceMatrix.GetDistance(i, index), dataRows[index].Item2));

                string predictedClass = KnnClassifier.GetClassBasedOfNN(nearestNeighbours);
                if (dataRows[i].Item2 == predictedClass)
                    correctlyClassified++;
            }

            return new CrossValidationResult(dataRows.Count, correctlyClassified);
        }

        private DistanceMatrix GetDistancesBetweenObjects(KNNMetric knnMetric)
        {
            List<List<decimal>> numericalDataRows = GetNumericalRawData();
            KnnDistanceEvaluator distanceEvaluator = new(numericalDataRows);

            decimal[,] distanceMatrix = new decimal[numericalDataRows.Count, numericalDataRows.Count];
            for (int i = 0; i < numericalDataRows.Count; i++)
            {
                for (int j = 0; j < numericalDataRows.Count; j++)
                    distanceMatrix[i, j] = distanceEvaluator.EvaluateDistance(numericalDataRows[i], numericalDataRows[j], knnMetric);
            }

            return new DistanceMatrix(distanceMatrix);
        }

        private List<(List<decimal>, string)> GetDataPreparedForKNNClassifier(string classColumn)
        {
            List<(List<decimal>, string)> result = new();
            List<string> classColumnValues = _dataStructure.GetRowsRawForColumn(classColumn);
            List<List<decimal>> numericalRawData = GetNumericalRawData();

            if (classColumnValues?.Count == numericalRawData?.Count)
            {
                for (int i = 0; i < classColumnValues.Count; i++)
                    result.Add((numericalRawData[i], classColumnValues[i]));
                return result;
            }

            throw new Exception("The date in DataStructure is not consistent.");
        }

        private List<List<decimal>> GetNumericalRawData()
        {
            List<string> numericalColumnNames = _dataStructure.GetColumnNames(ColumnType.DECIMAL, ColumnType.INT);
            List<List<decimal>> result = new();

            foreach (var row in _dataStructure.GetRowsRawWithHeaders())
            {
                List<decimal> values = new();
                foreach (var numericalColumnName in numericalColumnNames)
                    values.Add(decimal.Parse(row[numericalColumnName]));

                result.Add(values);
            }

            return result;
        }
    }
}
