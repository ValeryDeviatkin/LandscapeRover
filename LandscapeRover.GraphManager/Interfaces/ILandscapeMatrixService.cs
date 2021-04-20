using LandscapeRover.GraphManager.Items;

namespace LandscapeRover.GraphManager.Interfaces
{
    public interface ILandscapeMatrixService
    {
        int[,] GenerateMatrix(int matrixSize, int minValue, int maxValue);
        MatrixShortestWayItem[] CalculateShortestWays(int[,] matrix);
    }
}