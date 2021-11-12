using Royalty.Insurance.Proxy.APIResponseModels;
using System.Collections.Generic;

namespace Royalty.Insurance.Proxy.Response
{
    public class ProRateResponse
    {
        public Dictionary<CoverageTypeCode, double> CoverageValues { get; set; } = new Dictionary<CoverageTypeCode, double>();
        public double Total { get; set; }
        public double DownPayment { get; set; }
        public double DownToTotalPercentage { get; set; }
    }
}