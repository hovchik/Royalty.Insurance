using System;
using System.Collections.Generic;
using Royalty.Insurance.Proxy.APIResponseModels;

namespace Royalty.Insurance.Proxy.Request
{
    public class ProRateRequest
    {
        public Dictionary<CoverageTypeCode, double> Coverages { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public double Percentage { get; set; }
        public double BrokerFee { get; set; }
    }
}