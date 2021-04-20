using Senticode.Wpf.Base;

namespace LandscapeRover.ViewModels.ViewModels
{
    public enum StepDirection : byte
    {
        None,
        Left,
        Right,
        Top,
        Bottom
    }

    public class MatrixCellViewModel : ObservableObject
    {
        public MatrixCellViewModel(byte row, byte column)
        {
            Row = row;
            Column = column;
        }

        public byte Row { get; }
        public byte Column { get; }

        #region StepDirection: StepDirection

        public StepDirection StepDirection
        {
            get => _stepDirection;
            set => SetProperty(ref _stepDirection, value);
        }

        private StepDirection _stepDirection;

        #endregion
    }
}