using System;
using System.Collections.Generic;

namespace Royalty.Insurance.Proxy.APIModels.Marketing
{
    public class CabExcelModelComparer : IEqualityComparer<CabExcelModel>
    {
        public bool Equals(CabExcelModel x, CabExcelModel y)
        {
            return x != null && y != null && x.Email == y.Email && x.Insurer == y.Insurer &&
                   x.Mailing_Street == y.Mailing_Street && x.Dot == y.Dot && x.Policy_Number == y.Policy_Number
                   && x.Insurance_Type == y.Insurance_Type && x.BOC3 == y.BOC3 && x.PolExpDate == y.PolExpDate;
        }

        public int GetHashCode(CabExcelModel obj)
        {
            return HashCode.Combine(obj.Email, obj.Insurance_Type, obj.Insurer, obj.BOC3, obj.Dot, obj.Mailing_Street, obj.Policy_Number, obj.PolExpDay);
        }
    }
}