
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Proxy.Response
{
    public class ResultResponse
    {
        public string Message { get; }
        public Status Status { get; }

        public ResultResponse(string message, Status status)
        {
            Message = message;
            Status = status;
        }
    }
}
