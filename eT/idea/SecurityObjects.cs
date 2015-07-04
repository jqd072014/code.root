using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class MktPicture
    {
       public  MktDefinition MktDef;
       public string PriceType;
       public string[] BidLevels;
       public string[] OfferLevels;
       public string TradingState;
       public string TradingPrice;
       public string TradedSize;
       public string TradingSellSide;
       public string TradingBuySide;
       public string LastTradeSize;
       public string LastTradePrice;
       public string LastTradeTime;
       public string Volume;
       public string OpenPrice;
       public string HighPrice;
       public string LowPrice;
       public string ClosePrice;
       public string Benchmark;
    }

    public class MktDefinition
    {
     public string Instrument;
     public string  Name;
     public string Exchange;
     public string PriceIncrement;
     public string Source;
     public string Mode;
     public string PricesStatus;
     public string OrdersStatus;
     public string PostTradeStatus;
     public string[] SupportedStips;
    }

    public class  MtkLevel
    {
        public string Price;
        public string TotalSize;
        public string[] Participant;
    }

    public class Participant
    {
        public string UserCode;
        public string Size;
        public string ClearedUser;
    }
    public class Instrument
    {
        public string Flavor;
        public string LongName;
        public string TickerSymbol;
        public string Ticker;
        public string PriceDisplayFormat;
        public string StandardId;
        public string StandardIdSource;
        public string NotionalAmount;
        public string ScalePrice;
        public string Currency;
        public string DefaultMarkSet;
        public string DefaultPriceType;
        public string DefaultSettleOffset;
        public string ReviewStatus;
        public string DataSource;
        public string IssuedStatus;
        public string Board;
        public string Sector;
        public string Tag;
        public string Alias;
    }

    public class TreasuryBasis
    {
        public string Bond;
        public string Future;
        public string ConvFactor;
        public string CTD;
    }


    public class TreasuarySwap
    {
        public string Instrument; // 1st leg, shorter
        public string Benchmark;  // 2nd leg, OTR,
    }

    public class Future
    {
        public string Underlying;
        public string LastDateTraded;
        public string Expiration;
    }

    public class FixedIncome
    {
        public string IssueDate;
        public string DatedDate;
        public string FirstCouponDate;
        public string Coupon;
        public string CouponFrequency;
        public string Maturity;
        public string RedemptionValue;
    }

    /// <summary>
    /// History Price
    /// </summary>
    public class Mark
    {
        public string MarkSet;
        public string Instrument;
        public string Bid;
        public string Ask;
        public string TradedOnBid;
        public string TradedOnAsk;
        public string MarkTime;
    }

    public class Trade
    {
        public string Instrument;
        public string Size;
        public string Side;
        public string Price;
        public string TradedPrice;
        public string TradedPriceType;  //(P = price, Y = yield, D = discount, rate quoted vs Price Quoted).
        public string PriceDisplayFormat;
        public string Action;
        public string TradeTime;
        public string SettlementDate;
        public string Contra;    // counter party
        public string Exchange;
        public string ExchangeUserAccount;
        public string ExchangeTradeId;
        public string OrderGroup;
        public string TradeId;
        public string SubAccount;
        public string ClearingAccount;
        public string ClearingRecord;
        public string MatchStatus;
        public string BrokerCommission;
        public string SalesCommission;
        public string ReviewStatus;  //I = Imported, R = needs Refresh, ! = flagged, C = Checked, U = Unchecked, E = Error
        public string TradeStatus;
        public string Notes;
        public string ReasonCode;
        public string Stip;   // eg. stipulation, Geographics of MBS pool,  # of Pool, restricition on Seasoning, etc. not on standard feature.
    }

    public class Position
    {
        public string SubAccount;
        public string Instrument;
        public string Position;
        public string PositionTime;
        public string OpenPosition;
        public string SettledPosition;
        public string RealizedPL;
        public string AverageCost;
        public string OpenMark;
        public string CloseMark;
        public string TotalPL;
        public string TotalCommission;
        public string AccruedInterest;
        public string Descriptor;
        public string ScopeType;  //book, sub-account or trader
        public string ScopeId;
        public string TradeCount;
        public string NominalValue;
        public string AcquisitionCost;
        public string LeftOverCost;
    }
}
