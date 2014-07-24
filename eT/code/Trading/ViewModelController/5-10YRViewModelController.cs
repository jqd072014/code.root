using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Trading.ViewModelController
{
    public class _5_10YRViewModelController : IViewModelController 
    {
        public System.ComponentModel.INotifyPropertyChanged ViewModel { get; set; }

        public _5_10YRViewModelController(ITransport transport, IAdapter adapter, IScheduler scheduler, IScheduler dispatcher)
        {
            transport.GetTradingObservables()
                .SubscribeOn(scheduler)
                .ObserveOn(dispatcher)
                .Subscribe(fSet => adapter.updater(fSet, ViewModel));
        }
    }
}
