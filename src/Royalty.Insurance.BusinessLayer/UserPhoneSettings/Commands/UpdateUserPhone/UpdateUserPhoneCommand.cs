using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.UserPhoneSettings
{
    public class UpdateUserPhoneCommand : IRequest<UserPhoneResponse>
    {
        public UserPhoneRequest Request { get; set; }
    }
}