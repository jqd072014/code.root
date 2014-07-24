using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Trading.ViewModel
{
    public class DurationTraderViewModel : ViewModelBase
    {
        string _cusip;
        public string Cusip {
            get
            {
                return _cusip;
            }
            set
            {
                SetProperty(ref _cusip, value,()=>Cusip);
            }
        }
        public string Alias { get; set; }

        double? _ideal;
        public double? Ideal {
            get
            {
                return _ideal;
            }
            set
            {
                SetProperty(ref _ideal, value, () => Ideal);
            }
        }
        public double? Max { get; set; }
        public double? DV01 { get; set; }
        public int? Priority { get; set; }
        public bool? IsReference { get; set; }
        public double? OrderSize { get; set; }
        public double? OrderPrice { get; set; }



    }
}
