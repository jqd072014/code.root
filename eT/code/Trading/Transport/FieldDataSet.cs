using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trading.Transport
{
    public class FieldDataSet : IFieldDataSet
    {
        public double? GetDouble(string fieldName)
        {
            return 1.1;
        }

        public DateTime? GetDateTime(string fieldName)
        {
            return DateTime.Now;
        }

        public string GetString(string fieldName)
        {
            return "91282UI7";
        }

        public FieldStatus GetFieldStatus(string fieldName)
        {
            return FieldStatus.Ok;
        }
    }
}
