using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using PrismCustomRegionAdapter.Views;

namespace PrismCustomRegionAdapter.Modules
{
    public class Module : IModule
    {
        IRegionManager _regMgr;
        IUnityContainer _container;
        public Module(IUnityContainer container, IRegionManager regMgr)
        {
            _regMgr = regMgr;
            _container = container;
        }
        public void Initialize()
        {
            IRegion reg = _regMgr.Regions[RegionNames.PricingGridRegion];
            reg.Add(_container.Resolve<SampleView>());
            reg.Add(_container.Resolve<SampleView>());
            reg.Add(_container.Resolve<SampleView>());
            reg.Add(_container.Resolve<SampleView>());
            reg.Add(_container.Resolve<SampleView>());
        }
    }
}
