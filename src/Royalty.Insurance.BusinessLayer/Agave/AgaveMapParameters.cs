using Royalty.Insurance.Proxy.Request;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class AgaveMapParameters
    {
        public AgaveRoyaltyRefundRequest RequestModel { get; set; }
        public int MerchantId { get; set; }
        public string MerchantKey { get; set; }
    }
}
