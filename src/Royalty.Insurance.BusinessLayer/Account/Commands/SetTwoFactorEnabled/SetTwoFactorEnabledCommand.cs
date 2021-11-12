using MediatR;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class SetTwoFactorEnabledCommand : IRequest<bool>
    {
        public string Token { get; set; }
    }
}
