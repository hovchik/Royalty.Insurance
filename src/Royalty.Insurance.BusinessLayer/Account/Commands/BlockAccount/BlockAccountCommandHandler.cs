using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class BlockAccountCommandHandler : IRequestHandler<BlockAccountCommand, BaseResponse<bool>>
    {
        private readonly IApplicationDbContext _context;

        public BlockAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<bool>> Handle(BlockAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Email.Equals(request.Email.ToLower()), cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EmailNotFound);
            }

            user.IsBlocked = true;
            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError , ResourceCommonMessage.SaveFailed);
            }

            return new BaseResponse<bool>( true);
        }
    }
}
