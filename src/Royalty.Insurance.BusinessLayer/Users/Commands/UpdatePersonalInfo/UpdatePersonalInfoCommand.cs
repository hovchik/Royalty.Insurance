using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class UpdatePersonalInfoCommand : UserProfileBaseRequest, IRequest<UserResponse>
    {
        public int UserId { get; set; }
        public string PersonalAvatar { get; set; }
    }
}
