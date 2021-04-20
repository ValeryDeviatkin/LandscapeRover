using System;
using System.Windows.Input;
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
        }

        public ObservableRangeCollection<MatrixCellViewModel> MatrixCells { get; } =
            new ObservableRangeCollection<MatrixCellViewModel>();

        public ObservableRangeCollection<MatrixShortestWayItem> ShortestWays { get; } =
            new ObservableRangeCollection<MatrixShortestWayItem>();

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
            // TODO: Handle command logic here
        }

        #endregion
    }
}