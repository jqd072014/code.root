using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Trading;
using Trading.View;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using Trading.ViewModel;
using Trading.ViewModelController;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Threading;
using System.Diagnostics;
using Trading.Transport;
using Trading.Adapter;

namespace Modules
{
    public class TradingModule : IModule
    {
        IUnityContainer _container;
        public TradingModule(IUnityContainer container)
        {
            _container = container;
        }
        public void Initialize()
        {
            _container.RegisterInstance<ITransport>(new TradingTransport());
            _container.RegisterInstance<IAdapter>(new TradingAdapter());
            _container.RegisterInstance<IScheduler>(new NewThreadScheduler());
            _container.RegisterInstance<LocalScheduler>(DispatcherScheduler.Current);
        }
    }





}

