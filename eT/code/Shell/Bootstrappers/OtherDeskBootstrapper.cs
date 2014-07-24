using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.UnityExtensions;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;

namespace Shell.Bootstrappers
{
    class OtherDeskBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            // TODO: OtherDesk Shell Window
            return Container.Resolve(typeof(ShellWindow), "OtherDeskShellWindow") as ShellWindow;
        }
        protected override void InitializeShell()
        {
            (Application.Current.MainWindow = (Window)Shell).Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
    }
}
