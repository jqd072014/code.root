using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;

namespace DataGridWithColumnStyle
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DemoUnityBootstrapper b = new DemoUnityBootstrapper();
            b.Run(runWithDefaultConfiguration : true);
        }
    }

    public class DemoUnityBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve(typeof(MainWindow), "mw") as MainWindow;
        }
        protected override void InitializeShell()
        {
            (Application.Current.MainWindow = (Window)Shell).Show();
        }
    }
}
