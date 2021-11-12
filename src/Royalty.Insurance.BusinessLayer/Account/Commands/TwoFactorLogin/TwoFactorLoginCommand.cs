using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class TwoFactorLoginCommand : IRequest<LoginResponse>
    {
        public string Code { get; set; }

        public string Token { get; set; }

        public string UserIpAddress { get; set; }
    }
}
