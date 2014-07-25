using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Trading.ViewModel
{
    public class _5_10YRViewModel : ViewModelBase
    {
        string _cusip;
        public string Cusip
        {
            get
            {
                return _cusip;
            }
            set
            {
                SetProperty(ref _cusip, value, () => Cusip);
            }
        }
    }
}
