using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.UserPhoneSettings
{
    public class CreateUserPhoneCommand : IRequest<UserPhoneResponse>
    {
        public UserPhoneRequest Request { get; set; }
    }
}