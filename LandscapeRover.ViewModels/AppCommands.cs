using System.Windows.Input;
using LandscapeRover.Common.Interfaces;
using Senticode.Wpf;
using Senticode.Wpf.Base;
using Unity;

namespace LandscapeRover.ViewModels
{
    public static class AppCommands
    {
        private static readonly IUnityContainer Container = ServiceLocator.Container;

        #region ExitApp command

        public static ICommand ExitAppCommand => _exitAppCommand ??=
                                                     new SyncCommand(ExecuteExitApp);

        private static SyncCommand _exitAppCommand;

        private static void ExecuteExitApp(object parameter)
        {
            Container.Resolve<IDesktopClientManager>().ExitApp();
        }

        #endregion
    }
}