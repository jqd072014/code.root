using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.UnityExtensions;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using PrismCustomRegionAdapter.Modules;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;
using PrismCustomRegionAdapter.Infrastructure;

namespace PrismCustomRegionAdapter
{
    public class Bootstrapper : UnityBootstrapper
    {

        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.TryResolve<Shell>();
        }

        protected override void InitializeShell()
        {
            (App.Current.MainWindow =(Window) Shell).Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = typeof(Module).Name,
                ModuleType = typeof(Module).AssemblyQualifiedName
            });
            base.ConfigureModuleCatalog();
        }

        protected override Microsoft.Practices.Prism.Regions.RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.TryResolve<StackPanelRegionAdapter>());

            return mappings;
        }
    }
}
