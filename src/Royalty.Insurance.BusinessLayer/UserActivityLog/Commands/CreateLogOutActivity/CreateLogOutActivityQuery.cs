using MediatR;
using Royalty.Insurance.Proxy.Request;

namespace Royalty.Insurance.BusinessLayer.UserActivityLogs
{
    public class CreateLogOutActivityQuery : IRequest<bool>
    {
        public UserActivityLogRequest Request { get; set; }
    }
}
