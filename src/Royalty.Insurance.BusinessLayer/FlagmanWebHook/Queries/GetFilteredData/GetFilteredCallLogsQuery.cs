using MediatR;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;
using System;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.FlagmanWebHook
{
    public class GetFilteredCallLogsQuery : IRequest<List<UserPhoneCallHistoryResponse>>
    {
        public CallTypeCode? CallType { get; set; }
        public string CallNumber { get; set; }
        public string CallerName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? Extension { get; set; }
    }
}