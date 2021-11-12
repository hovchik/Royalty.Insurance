using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Proxy.Request
{
    public class UserPhoneCallHistoryRequest
    {
        public int UserPhoneId { get; set; }
        public CallTypeCode CallType { get; set; }
        public string CallNumber { get; set; }
        public int Extension { get; set; }
        public string CallId { get; set; }
        public string CallerName { get; set; }
    }
}
