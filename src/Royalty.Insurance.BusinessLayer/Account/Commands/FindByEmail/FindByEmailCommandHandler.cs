using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class FindByEmailCommandHandler : IRequestHandler<FindByEmailCommand, User>
    {
        private readonly IApplicationDbContext _context;

        public FindByEmailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(FindByEmailCommand request, CancellationToken cancellationToken)
        {
            User user = await _context.Users.FirstOrDefaultAsync(
                item => item.Email.ToLower().Equals(request.Email.ToLower()), cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EmailNotFound);
            }

            if (user.IsBlocked)
            {
                throw new RestApiResponseException((int)HttpStatusCode.BadRequest, ResourceCommonMessage.AccountIsBlocked);
            }

            return user;
        }
    }
}
