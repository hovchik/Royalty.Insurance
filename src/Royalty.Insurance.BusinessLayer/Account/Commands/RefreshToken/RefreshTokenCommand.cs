using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class RefreshTokenCommand : IRequest<LoginResponse>
    {
        public string Token { get; set; }
        public string  ExpiredAccessToken { get; set; }
        public string UserIpAddress { get; set; }
    }
}
