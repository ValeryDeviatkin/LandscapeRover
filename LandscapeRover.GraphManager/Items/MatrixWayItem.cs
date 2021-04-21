using System.Collections.Generic;

namespace LandscapeRover.GraphManager.Items
{
    public class MatrixWayItem
    {
        public IReadOnlyList<MatrixStepItem> Steps { get; internal set; }
        public int TotalCharge { get; internal set; }
    }
}