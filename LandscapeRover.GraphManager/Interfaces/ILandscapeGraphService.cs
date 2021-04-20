using LandscapeRover.GraphManager.Items;

namespace LandscapeRover.GraphManager.Interfaces
{
    public interface ILandscapeGraphService
    {
        byte[,] GenerateMatrix(byte n, byte m);
        MatrixShortestWayItem[] CalculateShortestWays(byte[,] matrix);
    }
}