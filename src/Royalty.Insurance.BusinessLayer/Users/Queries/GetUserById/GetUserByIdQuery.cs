using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public int Id { get; set; }
    }
}
