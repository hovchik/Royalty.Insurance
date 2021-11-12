using MediatR;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class ActivateUserCommand : IRequest<bool>
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
