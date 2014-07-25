using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Trading.ViewModel;
using System.Diagnostics;

namespace Trading.Adapter
{
    public class TradingAdapter : IAdapter
    {
        public Action<IFieldDataSet, INotifyPropertyChanged> updater {
            get
            {
                return
                    (fs, vm) =>
                    {
                        if (vm is DurationTraderViewModel) Adapt(fs, vm as DurationTraderViewModel);
                        if (vm is _5_10YRViewModel) Adapt(fs, vm as _5_10YRViewModel);
                    };
            }
        }

        void Adapt(IFieldDataSet fds,DurationTraderViewModel vm )
        {
            vm.Cusip = fds.GetString(Map.Cusip)+DateTime.Now ;
        }

        void Adapt(IFieldDataSet fds, _5_10YRViewModel vm)
        {
            vm.Cusip = fds.GetString(Map.Cusip) + DateTime.Now;
        }
    }
}
