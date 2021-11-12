using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.SavedRequests
{
    public class DeleteSavedRequestCommandHandler : IRequestHandler<DeleteSavedRequestCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public DeleteSavedRequestCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<Unit> Handle(DeleteSavedRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.SavedMarketingRequests.FirstOrDefaultAsync(item => item.Id==request.Id && item.UserId==_currentUser.UserId);
            if (entity == null)
            {
                throw new RestApiResponseException(ResourceCommonMessage.EntityNotFound);
            }

            _context.SavedMarketingRequests.Remove(entity);

            await _context.SaveChangesAsync(new CancellationToken());

            return Unit.Value;
        }
    }
}