using MediatR;
using Royalty.Insurance.Proxy.Request;

namespace Royalty.Insurance.BusinessLayer.UserActivityLogs
{
    public class CreateLogInActivityQuery : IRequest<bool>
    {
        public UserActivityLogRequest Request { get; set; }
    }
}
