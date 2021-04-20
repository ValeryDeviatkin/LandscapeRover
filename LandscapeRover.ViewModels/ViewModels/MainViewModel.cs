using System;
using System.Windows.Input;
using LandscapeRover.Common.Constants;
using LandscapeRover.Common.Helpers;
using LandscapeRover.GraphManager.Interfaces;
using LandscapeRover.GraphManager.Items;
using Senticode.Wpf.Base;
using Senticode.Wpf.Collections;
using Unity;

namespace LandscapeRover.ViewModels.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel() : base(null)
        {
            throw new NotImplementedException();
        }

        public MainViewModel(IUnityContainer container) : base(container)
        {
            MatrixSize = MatrixConstants.Size;
        }

        public ObservableRangeCollection<MatrixShortestWayItem> ShortestWays { get; } =
            new ObservableRangeCollection<MatrixShortestWayItem>();

        public ObservableRangeCollection<int> MatrixCells { get; } =
            new ObservableRangeCollection<int>();

        #region MatrixSize: int

        public int MatrixSize
        {
            get => _matrixSize;
            set => SetProperty(ref _matrixSize, value);
        }

        private int _matrixSize;

        #endregion

        #region SelectedWay: MatrixShortestWayItem

        public MatrixShortestWayItem SelectedWay
        {
            get => _selectedWay;
            set => SetProperty(ref _selectedWay, value);
        }

        private MatrixShortestWayItem _selectedWay;

        #endregion

        #region GenerateMatrix command

        public ICommand GenerateMatrixCommand => _generateMatrixCommand ??=
                                                     new SyncCommand(ExecuteGenerateMatrix);

        private SyncCommand _generateMatrixCommand;

        private void ExecuteGenerateMatrix(object parameter)
        {
            MatrixCells.Clear();

            var matrix = Container.Resolve<ILandscapeMatrixService>().GenerateMatrix(
                MatrixSize, MatrixConstants.MinValue, MatrixConstants.MaxValue);

            MatrixCells.ReplaceAll(matrix.ToEnumerable<int>());
        }

        #endregion
    }
}