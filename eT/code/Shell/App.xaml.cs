using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Modularity;
using Shell.Bootstrappers;

namespace Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string bootstrapperName = ConfigurationManager.AppSettings.Get("bootstrapperName");
            if (bootstrapperName == "DemoUnityBootstrapper")
            {
                DemoUnityBootstrapper b = new DemoUnityBootstrapper();
                b.Run(runWithDefaultConfiguration: true);
            }
        }
    }
}
