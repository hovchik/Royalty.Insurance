using Domain;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class CheckPasswordCommand : IRequest<User>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
