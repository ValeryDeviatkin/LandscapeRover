using System.Windows.Input;
using System.Windows.Shapes;
using LandscapeRover.ViewModels.ViewModels;
using Senticode.Wpf;
using Senticode.Wpf.Base;
using Unity;

namespace LandscapeRover
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region DrawWay command

        public ICommand DrawWayCommand => _drawWayCommand ??=
                                              new SyncCommand(ExecuteDrawWay);

        private SyncCommand _drawWayCommand;

        private void ExecuteDrawWay(object parameter)
        {
            Canvas.Children.Clear();

            var mainViewModel = ServiceLocator.Container.Resolve<MainViewModel>();
            var way = mainViewModel.SelectedWay;

            if (way == null)
            {
                return;
            }

            var lineLength = Canvas.ActualHeight / mainViewModel.MatrixSize;
            var offset = lineLength * 0.5d;

            for (var i = 0; i < way.Steps.Count - 1; i++)
            {
                var step = way.Steps[i];
                var nextStep = way.Steps[i + 1];

                Canvas.Children.Add(new Line
                {
                    Y1 = offset + lineLength * step.Row,
                    Y2 = offset + lineLength * nextStep.Row,
                    X1 = offset + lineLength * step.Column,
                    X2 = offset + lineLength * nextStep.Column
                });
            }
        }

        #endregion
    }
}