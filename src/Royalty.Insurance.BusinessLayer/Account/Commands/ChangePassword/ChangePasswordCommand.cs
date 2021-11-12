using MediatR;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public string NewPassword { get; set; }

        public string Password { get; set; }
    }
}
