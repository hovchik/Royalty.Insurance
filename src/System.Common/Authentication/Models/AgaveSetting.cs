namespace System.Common.Authentication.Models
{
    public class AgaveSetting
    {
        public int MerchantId { get; set; }

        public string MerchantKey { get; set; }

        public string SaleUrl { get; set; }

        public string RefundUrl { get; set; }
    }
}