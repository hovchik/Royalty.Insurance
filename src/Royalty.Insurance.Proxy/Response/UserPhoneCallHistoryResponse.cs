using System;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Proxy.Response
{
    public class UserPhoneCallHistoryResponse
    {
        public int UserId { get; set; }
        public CallTypeCode CallType { get; set; }
        public string CallNumber { get; set; }
        public int Extension { get; set; }
        public DateTime CreationTime { get; set; }
        public string CallerName { get; set; }
        public string CallId { get; set; }
        public int? Duration { get; set; }
    }
}