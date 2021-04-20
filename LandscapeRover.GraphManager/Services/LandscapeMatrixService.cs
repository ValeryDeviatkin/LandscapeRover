using System;
using LandscapeRover.GraphManager.Interfaces;
using LandscapeRover.GraphManager.Items;

namespace LandscapeRover.GraphManager.Services
{
    internal class LandscapeMatrixService : ILandscapeMatrixService
    {
        public int[,] GenerateMatrix(int matrixSize, int minValue, int maxValue)
        {
            if (matrixSize < 0 || minValue > maxValue)
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

        public MatrixWayItem[] CalculateShortestWays(int[,] matrix)
        {
            return new[]
            {
                new MatrixWayItem
                {
                    Steps = new[]
                    {
                        new MatrixStepItem {Row = 0, Column = 0},
                        new MatrixStepItem {Row = 1, Column = 0},
                        new MatrixStepItem {Row = 1, Column = 1},
                        new MatrixStepItem {Row = 1, Column = 2},
                        new MatrixStepItem {Row = 1, Column = 3},
                        new MatrixStepItem {Row = 2, Column = 3},
                        new MatrixStepItem {Row = 2, Column = 4},
                        new MatrixStepItem {Row = 3, Column = 4},
                        new MatrixStepItem {Row = 4, Column = 4},
                        new MatrixStepItem {Row = 4, Column = 3},
                        new MatrixStepItem {Row = 4, Column = 2}
                    }
                }
            };
        }
    }
}