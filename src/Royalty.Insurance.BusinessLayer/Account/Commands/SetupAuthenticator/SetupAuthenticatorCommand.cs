using System.Common.Authentication.Models;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class SetupAuthenticatorCommand : IRequest<TotpResult>
    {
        public string Token { get; set; }
    }
}
