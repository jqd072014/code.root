using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Trading.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetProperty<T>(ref T field, T value, Expression<Func<T>> exp)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            if (PropertyChanged != null)
            {
                MemberExpression me = exp.Body as MemberExpression;
                if (me != null && me.Member != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(me.Member.Name));
            }
        }
    }
}
