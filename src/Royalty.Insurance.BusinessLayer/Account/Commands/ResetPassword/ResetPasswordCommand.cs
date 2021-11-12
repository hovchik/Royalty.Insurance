using MediatR;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public string Code { get; set; }
        public string Password { get; set; }
    }
}
