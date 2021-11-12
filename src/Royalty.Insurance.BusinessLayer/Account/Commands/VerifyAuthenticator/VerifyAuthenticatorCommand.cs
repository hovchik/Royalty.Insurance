using MediatR;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class VerifyAuthenticatorCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
