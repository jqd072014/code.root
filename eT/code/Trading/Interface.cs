using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Subjects;
using System.ComponentModel;
using System.Windows.Controls;
using System.Reactive.Linq;
using System.Reactive.Concurrency;

namespace Trading
{
    public interface ITransport
    {
        IObservable<IFieldDataSet> GetTradingObservables();
    }

    public interface IAdapter
    {
        Action<IFieldDataSet, INotifyPropertyChanged> updater { get;  }
    }

    public interface IViewModelController
    {
        INotifyPropertyChanged ViewModel { get; set; }
    }

    public interface IFieldDataSet
    {
        double? GetDouble(string fieldName);
        DateTime? GetDateTime(string fieldName);
        string GetString(string fieldName);
        FieldStatus GetFieldStatus(string fieldName);
    }
    
    public enum FieldStatus
    {
        Ok=1,
        Stale=2,
        Error=3,
        Unknown=-1
    }

    public enum WellknowViewName
    { 
        DurationTraderView,
        _5_10Yr
    }

    public interface IDockingViewManager
    {
        UserControl GetDockingView(WellknowViewName viewName);
    }

    public interface IDispatcherScheduler : IScheduler
    {
    }
}
