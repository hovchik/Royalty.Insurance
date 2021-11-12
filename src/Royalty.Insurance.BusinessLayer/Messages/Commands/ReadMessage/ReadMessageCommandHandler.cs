using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class ReadMessageCommandHandler : IRequestHandler<ReadMessageCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public ReadMessageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ReadMessageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.UnreadMessages
                .Where(message => message.MessageId.Equals(request.MessageId) && message.ReadUserId.Equals(request.UserId))
                .FirstOrDefaultAsync(cancellationToken);

            if (entity != null)
            {
                _context.UnreadMessages.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
