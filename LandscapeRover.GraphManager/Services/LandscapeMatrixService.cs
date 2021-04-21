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
                new List<MatrixCellItem> {new MatrixCellItem {Row = 0, Column = 0}},
                new HashSet<string>());

            var minCharge = ways.Min(x => x.TotalCharge);

            return ways
                  .Where(x => x.TotalCharge == minCharge)
                  .ToArray();
        }

        private static void CalculateWaysRecursive(int[,] matrix,
                                                   ICollection<MatrixWayItem> ways,
                                                   IReadOnlyList<MatrixCellItem> passedCells,
                                                   ICollection<string> blockedCells)
        {
            var currentCell = passedCells[passedCells.Count - 1];
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);

            if (currentCell.Column == columnCount - 1 && currentCell.Row == rowCount - 1)
            {
                var way = new MatrixWayItem {Cells = passedCells};

                //
                var x = matrix[0, 0];

                for (var i = 0; i < columnCount; i++)
                {
                    matrix[0, i] = x;
                    matrix[i, columnCount - 1] = x;
                }
                //

                for (var i = 0; i < way.Cells.Count - 1; i++)
                {
                    var cell = way.Cells[i];
                    var nextCell = way.Cells[i + 1];
                    var value = matrix[cell.Row, cell.Column];
                    var nextValue = matrix[nextCell.Row, nextCell.Column];

                    way.TotalCharge += 1 + Math.Abs(value - nextValue);
                }

                ways.Add(way);

                return;
            }

            var availableCells = new List<MatrixCellItem>();

            if (currentCell.Row > 0)
            {
                TryGetAvailableCell(currentCell.Row - 1, currentCell.Column, blockedCells, availableCells);
            }

            if (currentCell.Row < rowCount - 1 && currentCell.Column < columnCount - 1)
            {
                TryGetAvailableCell(currentCell.Row + 1, currentCell.Column, blockedCells, availableCells);
            }

            if (currentCell.Column > 0 && currentCell.Row < rowCount - 1)
            {
                TryGetAvailableCell(currentCell.Row, currentCell.Column - 1, blockedCells, availableCells);
            }

            if (currentCell.Column < columnCount - 1)
            {
                TryGetAvailableCell(currentCell.Row, currentCell.Column + 1, blockedCells, availableCells);
            }

            foreach (var availableCell in availableCells)
            {
                blockedCells.Add(GetMatrixCellKey(availableCell));
            }

            foreach (var cell in availableCells)
            {
                var nextPassedCells = new List<MatrixCellItem>(passedCells) {cell};
                var nextBlockedCells = new HashSet<string>(blockedCells) {GetMatrixCellKey(currentCell)};

                CalculateWaysRecursive(matrix, ways, nextPassedCells, nextBlockedCells);
            }
        }

        private static void TryGetAvailableCell(
            int row,
            int column,
            ICollection<string> blockedCells,
            ICollection<MatrixCellItem> availableCells)
        {
            if (!blockedCells.Contains(GetMatrixCellKey(row, column)))
            {
                availableCells.Add(new MatrixCellItem {Row = row, Column = column});
            }
        }

        private static string GetMatrixCellKey(int row, int column) => $"{row} {column}";
        private static string GetMatrixCellKey(MatrixCellItem cell) => GetMatrixCellKey(cell.Row, cell.Column);
    }
}