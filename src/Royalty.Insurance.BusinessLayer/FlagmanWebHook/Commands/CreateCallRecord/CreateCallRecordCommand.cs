using MediatR;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.FlagmanWebHook
{
    public class CreateCallRecordCommand:IRequest<BaseResponse<bool>>
    {
        public int UserPhoneId { get; set; }
        public CallTypeCode CallType { get; set; }
        public string CallNumber { get; set; }
        public int Extension { get; set; }
        public string CallId { get; set; }
        public string CallerName { get; set; }
    }
}