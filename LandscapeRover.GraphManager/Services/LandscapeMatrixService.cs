using System;
using System.Collections.Generic;
using System.Linq;
using LandscapeRover.Common.Constants;
using LandscapeRover.GraphManager.Interfaces;
using LandscapeRover.GraphManager.Items;

namespace LandscapeRover.GraphManager.Services
{
    internal class LandscapeMatrixService : ILandscapeMatrixService
    {
        public int[,] GenerateMatrix(int matrixSize, int minValue, int maxValue)
        {
            if (matrixSize < MatrixConstants.MinSize || minValue > maxValue)
            {
                throw new NotSupportedException();
            }

            var random = new Random();
            var matrix = new int[matrixSize, matrixSize];

            for (var i = 0; i < matrixSize; i++)
            {
                for (var j = 0; j < matrixSize; j++)
                {
                    matrix[i, j] = random.Next(minValue, maxValue + 1);
                }
            }

            return matrix;
        }

        public IReadOnlyList<MatrixWayItem> CalculateShortestWays(int[,] matrix)
        {
            var minSize = MatrixConstants.MinSize;
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var ways = new List<MatrixWayItem>();

            if (rowCount < minSize || columnCount < minSize)
            {
                throw new NotSupportedException();
            }

            CalculateWaysRecursive(
                matrix,
                ways,
                new List<MatrixStepItem> {new MatrixStepItem {Row = 0, Column = 0}},
                new HashSet<string>());

            var minCharge = ways.Min(x => x.TotalCharge);

            return ways
                  .Where(x => x.TotalCharge == minCharge)
                  .ToArray();
        }

        private static void CalculateWaysRecursive(int[,] matrix,
                                                   ICollection<MatrixWayItem> ways,
                                                   IReadOnlyList<MatrixStepItem> passedSteps,
                                                   ICollection<string> blockedSteps)
        {
            var currentStep = passedSteps[passedSteps.Count - 1];
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);

            if (currentStep.Column == columnCount - 1 && currentStep.Row == rowCount - 1)
            {
                var way = new MatrixWayItem {Steps = passedSteps};

                for (var i = 0; i < way.Steps.Count - 1; i++)
                {
                    var step = way.Steps[i];
                    var nextStep = way.Steps[i + 1];
                    var value = matrix[step.Row, step.Column];
                    var nextValue = matrix[nextStep.Row, nextStep.Column];

                    way.TotalCharge += 1 + Math.Abs(value - nextValue);
                }

                ways.Add(way);

                return;
            }

            var availableSteps = new List<MatrixStepItem>();

            if (currentStep.Row > 0)
            {
                TryGetAvailableStep(currentStep.Row - 1, currentStep.Column, blockedSteps, availableSteps);
            }

            if (currentStep.Row < rowCount - 1 && currentStep.Column < columnCount - 1)
            {
                TryGetAvailableStep(currentStep.Row + 1, currentStep.Column, blockedSteps, availableSteps);
            }

            if (currentStep.Column > 0 && currentStep.Row < rowCount - 1)
            {
                TryGetAvailableStep(currentStep.Row, currentStep.Column - 1, blockedSteps, availableSteps);
            }

            if (currentStep.Column < columnCount - 1)
            {
                TryGetAvailableStep(currentStep.Row, currentStep.Column + 1, blockedSteps, availableSteps);
            }

            foreach (var step in availableSteps)
            {
                var nextPassedSteps = new List<MatrixStepItem>(passedSteps) {step};
                var nextBlockedSteps = new HashSet<string>(blockedSteps) {GetMatrixCellKey(currentStep)};

                foreach (var availableStep in availableSteps)
                {
                    nextBlockedSteps.Add(GetMatrixCellKey(availableStep));
                }

                CalculateWaysRecursive(matrix, ways, nextPassedSteps, nextBlockedSteps);
            }
        }

        private static void TryGetAvailableStep(
            int row,
            int column,
            ICollection<string> blockedSteps,
            ICollection<MatrixStepItem> availableSteps)
        {
            if (!blockedSteps.Contains(GetMatrixCellKey(row, column)))
            {
                availableSteps.Add(new MatrixStepItem {Row = row, Column = column});
            }
        }

        private static string GetMatrixCellKey(int row, int column) => $"{row} {column}";
        private static string GetMatrixCellKey(MatrixStepItem step) => GetMatrixCellKey(step.Row, step.Column);
    }
}