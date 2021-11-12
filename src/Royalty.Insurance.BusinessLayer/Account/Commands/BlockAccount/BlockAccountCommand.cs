using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class BlockAccountCommand : IRequest<BaseResponse<bool>>
    {
        public string Email { get; set; }
    }
}
