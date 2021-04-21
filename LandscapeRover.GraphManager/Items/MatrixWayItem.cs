using System.Collections.Generic;

namespace LandscapeRover.GraphManager.Items
{
    public class MatrixWayItem
    {
        public IReadOnlyList<MatrixCellItem> Cells { get; internal set; }
        public int TotalCharge { get; internal set; }
    }
}