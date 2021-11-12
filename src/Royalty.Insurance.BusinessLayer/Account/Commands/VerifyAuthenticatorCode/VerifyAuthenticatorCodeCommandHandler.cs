using System.Common.Authentication.TwoFactor;
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class VerifyAuthenticatorCodeCommandHandler : IRequestHandler<VerifyAuthenticatorCodeCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ITotpHelper _totpHelper;

        public VerifyAuthenticatorCodeCommandHandler(IApplicationDbContext context, ITotpHelper totpHelper)
        {
            _context = context;
            _totpHelper = totpHelper;
        }

        public async Task<bool> Handle(VerifyAuthenticatorCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Email.Equals(request.Email), cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.UserNotFound);
            }

            return _totpHelper.Validate(_totpHelper.GenerateSecret(user.Email, user.CreateDatetimeUtc.Ticks), request.Code);
        }
    }
}
