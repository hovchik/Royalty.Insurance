using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Files
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteFileCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _context.UserGarages
                .FirstOrDefaultAsync(
                    item => item.Id.Equals(request.Id) && item.UserId.Equals(_currentUserService.UserId),
                    cancellationToken);
            if (file == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            var messageAttachments = await _context.Attachments
                .Where(item => item.UserGarageId.Value.Equals(request.Id))
                .ToListAsync(cancellationToken);
            messageAttachments.ForEach(item => item.IsDeleted = true);
            _context.UserGarages.Remove(file);

            if (await _context.SaveChangesAsync(cancellationToken) < 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError,
                    ResourceCommonMessage.SaveFailed);
            }
            return Unit.Value;
        }
    }
}
