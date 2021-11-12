using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class ReadGroupMessageCommandHandler : IRequestHandler<ReadGroupMessageCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public ReadGroupMessageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ReadGroupMessageCommand request, CancellationToken cancellationToken)
        {
            var entities = await _context.UnreadMessages
                .Where(message => message.GroupId.Equals(request.GroupId) && message.ReadUserId.Equals(request.UserId))
                .ToListAsync(cancellationToken);
            _context.UnreadMessages.RemoveRange(entities);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
