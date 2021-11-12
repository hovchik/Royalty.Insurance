using MediatR;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class ForgetPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
