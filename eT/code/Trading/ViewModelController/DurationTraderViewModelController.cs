using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Diagnostics;

namespace Trading.ViewModelController
{
    public class DurationTraderViewModelController : IViewModelController
    {
        public INotifyPropertyChanged ViewModel { get; set; }

        public DurationTraderViewModelController(ITransport transport, IAdapter adapter, IScheduler scheduler, LocalScheduler dispatcher)
        {
            transport.GetTradingObservables()
                .SubscribeOn(scheduler)
                .ObserveOn(dispatcher)
                .Subscribe(fSet => adapter.updater(fSet, ViewModel));
        }
    }
}
