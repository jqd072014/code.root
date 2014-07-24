using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Controls;
using System.Diagnostics;

namespace Trading
{
    public class FluentFactory
    {
        public FluentFactory ViewModelController(Func<IViewModelController> viewModelControllerFactory)
        {
            var f = viewModelControllerFactory.Invoke();
            f.ViewModel = _viewModel;
            return this;
        }

        INotifyPropertyChanged _viewModel=null;
        public FluentFactory ViewModel(Func<INotifyPropertyChanged> viewModelFactory)
        {
            _viewModel = viewModelFactory.Invoke();
            return this;
        }

        public void View(Func<FrameworkElement> viewFactory)
        {
            FrameworkElement fe = viewFactory.Invoke();
            fe.DataContext = _viewModel;
        }
    }
}
