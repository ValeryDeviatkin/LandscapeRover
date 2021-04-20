using System;
using LandscapeRover.GraphManager.Interfaces;
using LandscapeRover.GraphManager.Items;

namespace LandscapeRover.GraphManager.Services
{
    internal class GraphService : ILandscapeGraphService
    {
        public byte[,] GenerateMatrix(byte n, byte m) => throw new NotImplementedException();

        public MatrixShortestWayItem[] CalculateShortestWays(byte[,] matrix) => throw new NotImplementedException();
    }
}