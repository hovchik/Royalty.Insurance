using System.Common.Authentication.Models;
using System.Common.EmailSender;
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Core.System.Security.Cryptography;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class CheckPasswordCommandHandler : IRequestHandler<CheckPasswordCommand, User>
    {
        private readonly IApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly AppSetting _appSetting;

        public CheckPasswordCommandHandler(IApplicationDbContext context, IEmailSender emailSender, IOptions<AppSetting> options)
        {
            _context = context;
            _emailSender = emailSender;
            _appSetting = options.Value;
        }

        public async Task<User> Handle(CheckPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException(ResourceCommonMessage.EmailNotFound);
            }

            if (user.IsBlocked)
            {
                throw new RestApiResponseException((int)HttpStatusCode.BadRequest, ResourceCommonMessage.AccountIsBlocked);
            }

            if (!PasswordHasher.IsValid(user.Password, request.Password, user.Salting))
            {
                user.FailedLoginCount++;
                if (user.FailedLoginCount == _appSetting.FailedMaxCount)
                {
                    await _emailSender.Send(new EmailMessage(_appSetting.AdminEmail,
                        ResourceCommonMessage.AccountBlocked,
                        string.Format(ResourceCommonMessage.UserAccountIsBlocked, user.Email)));
                    user.IsBlocked = true;
                }

                await _context.SaveChangesAsync(new CancellationToken());
                throw new RestApiResponseException(ResourceCommonMessage.UserOrPassword);
            }
            if (user.TemporaryPassword)
            {
                throw new RestApiResponseException((int)HttpStatusCode.Unauthorized, ResourceCommonMessage.UserTemporaryPassword);
            }

            if (!user.IsActive)
            {
                throw new RestApiResponseException(ResourceCommonMessage.UserNotActive);
            }

            if (user.FailedLoginCount > 0)
            {
                user.FailedLoginCount = 0;
                await _context.SaveChangesAsync(cancellationToken);
            }

            return user;
        }
    }
}
