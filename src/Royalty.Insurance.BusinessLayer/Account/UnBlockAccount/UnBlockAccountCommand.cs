using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class UnBlockAccountCommand : IRequest<BaseResponse<bool>>
    {
        public string Email { get; set; }
    }
}
