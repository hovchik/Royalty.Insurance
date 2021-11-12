using System;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Core.System.Security.Cryptography;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public ResetPasswordCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await _context.Users.FirstOrDefaultAsync(item => item.ForgetPasswordCode.Equals(request.Code), cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EmailNotFound);
            }

            if (!user.ForgetPasswordDatetimeUtc.HasValue ||
                DateTime.Compare(user.ForgetPasswordDatetimeUtc.Value, DateTime.UtcNow) < 0)
            {
                throw new RestApiResponseException(ResourceCommonMessage.ActivationPeriod);
            }

            user.ForgetPasswordCode = null;
            user.ForgetPasswordDatetimeUtc = null;

            var result = PasswordHasher.Generate(request.Password);
            user.Password = result.PasswordHash;
            user.Salting = result.Salting;

            return await _context.SaveChangesAsync(cancellationToken) == 1;
        }
    }
}
