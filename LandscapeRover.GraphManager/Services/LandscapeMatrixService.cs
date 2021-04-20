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
                    matrix[i, j] = random.Next(minValue, maxValue);
                }
            }

            return matrix;
        }

        public MatrixShortestWayItem[] CalculateShortestWays(int[,] matrix) => throw new NotImplementedException();
    }
}