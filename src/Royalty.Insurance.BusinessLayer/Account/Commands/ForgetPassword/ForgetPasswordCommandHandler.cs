using System;
using System.Common.Authentication.Models;
using System.Common.EmailSender;
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Core.System.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public ForgetPasswordCommandHandler(IApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public async Task<bool> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Email.Equals(request.Email.ToLower()), cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EmailNotFound);
            }

            if (user.IsBlocked)
            {
                throw new RestApiResponseException((int)HttpStatusCode.BadRequest, ResourceCommonMessage.AccountIsBlocked);
            }
            user.ForgetPasswordCode = Generator.Generate6DigitNumber();
            user.ForgetPasswordDatetimeUtc = DateTime.UtcNow.AddHours(48);
            await _emailSender.Send(new EmailMessage(user.Email, ResourceCommonMessage.EmailForgetPasswordSubject,
                string.Format(ResourceCommonMessage.EmailForgetPasswordBody, user.ForgetPasswordCode)));

            return await _context.SaveChangesAsync(cancellationToken) == 1;
        }
    }
}
