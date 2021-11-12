using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.UserPhoneSettings
{
    public class GetUserPhoneByIdQuery:IRequest<UserPhoneResponse>
    {
        public int Id { get; set; }
    }
}