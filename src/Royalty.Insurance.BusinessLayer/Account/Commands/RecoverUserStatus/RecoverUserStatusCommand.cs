using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class RecoverUserStatusCommand : IRequest<UserStatusResponse>
    {
    }
}
