namespace LandscapeRover.GraphManager.Items
{
    public class MatrixShortestWayItem
    {
        public MatrixCellItem[] Steps { get; internal set; }
        public int TotalCharge { get; internal set; }
    }
}