using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Common.Storage;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IStorageManager _storageManager;

        public DeleteUserProfileCommandHandler(IApplicationDbContext context, IStorageManager storageManager)
        {
            _context = context;
            _storageManager = storageManager;
        }

        public async Task<Unit> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Id.Equals(request.UserId), cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            if (string.IsNullOrEmpty(user.PersonalAvatar))
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            await _storageManager.DeleteAsync(request.FileContainer, request.UserId.ToString(), user.PersonalAvatar,
                request.UserId);
            user.PersonalAvatar = null;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
