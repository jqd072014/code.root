using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Linq;

namespace Trading.Transport
{
    public class TradingTransport : ITransport
    {
        public IObservable<IFieldDataSet> GetTradingObservables()
        {
            return Observable.Create<IFieldDataSet>((obsr) =>
            {
                return Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1)).Select(i => new FieldDataSet()).Subscribe(obsr);
            });
        }
    }
}
