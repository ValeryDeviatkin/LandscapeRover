using System;
using System.Windows;
using BAJIEPA.Tools.Helpers;
using LandscapeRover.Common.Interfaces;
using LandscapeRover.GraphManager;
using Senticode.Wpf;
using Unity;

namespace LandscapeRover
{
    public partial class App : IDesktopClientManager
    {
        public App() : base(ServiceLocator.Container)
        {
            Container.RegisterInstance<IDesktopClientManager>(this);
        }

        public void ExitApp()
        {
            Shutdown();
        }

        protected override void OnStartup(StartupEventArgs args)
        {
            ExceptionLogHelper.LogCriticalExceptionInRelease = exceptionItem => MessageBox.Show(
                exceptionItem.Message, exceptionItem.Source, MessageBoxButton.OK, MessageBoxImage.Error);

            try
            {
                base.OnStartup(args);
                CreateMainWindow().Show();
            }
            catch (Exception e)
            {
                this.LogCriticalException(e);

                throw;
            }
        }

        protected override void RegisterTypes()
        {
            GraphManagerInitializer.Initialize(Container);
        }
    }
}