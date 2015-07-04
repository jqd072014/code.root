using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Trading.View;
using Trading.ViewModel;
using Trading.ViewModelController;
using System.Reactive.Concurrency;

namespace Trading
{
    public class DockingViewManager : IDockingViewManager
    {

        ITransport _transport;
        IAdapter _adapter;
        IScheduler _scheduler;
        LocalScheduler _dispatcher;
        public DockingViewManager(ITransport transport, IAdapter adapter, IScheduler scheduler, LocalScheduler dispatcher)
        {
            _transport = transport;
            _adapter = adapter;
            _scheduler = scheduler;
            _dispatcher = dispatcher;
        }

        public UserControl GetDockingView(WellknowViewName viewName)
        {
            FluentFactory f = new FluentFactory();
            if (viewName == WellknowViewName.DurationTraderView)
            {
                DurationTraderView durView = new DurationTraderView();

                f.ViewModel(() => new DurationTraderViewModel())
                .ViewModelController(() => CreateDurationTraderViewModelController())
                .View(() => durView);
                return durView;
            }

            if (viewName == WellknowViewName._5_10Yr)
            {
                _5_10YRView view = new _5_10YRView();

                f.ViewModel(() => new _5_10YRViewModel())
                .ViewModelController(() => Create_5_10YRViewModelController())
                .View(() => view);
                return view;
            }
            return new UserControl();
        }

        #region Create VM Controller

        public DurationTraderViewModelController CreateDurationTraderViewModelController()
        {
            return new DurationTraderViewModelController(_transport,_adapter,_scheduler,_dispatcher);
        }

        public _5_10YRViewModelController Create_5_10YRViewModelController()
        {

            return new _5_10YRViewModelController(_transport, _adapter, _scheduler, _dispatcher);
        }
        #endregion
    }
}
