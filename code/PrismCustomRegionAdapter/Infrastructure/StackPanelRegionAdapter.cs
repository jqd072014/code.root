using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Specialized;

namespace PrismCustomRegionAdapter.Infrastructure
{
    public class StackPanelRegionAdapter : RegionAdapterBase<StackPanel>
    {
        public StackPanelRegionAdapter(RegionBehaviorFactory factory) : base(factory)
        {

        }


        protected override void Adapt(IRegion region, StackPanel regionTarget)
        {
            region.Views.CollectionChanged+=(s,ea)=>
                {
                    if(ea.Action== NotifyCollectionChangedAction.Add)
                    {
                        foreach(FrameworkElement fe in ea.NewItems)
                        {
                         regionTarget.Children.Add(fe);
                        }
                    }
                };
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
          //  return new SingleActiveRegion(); // if onely one item
        }

    }
}
