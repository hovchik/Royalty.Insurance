using System.Common.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.AgentTaskStatuses
{
    public class DeleteAgentTaskStatusCommandHandler : IRequestHandler<DeleteAgentTaskStatusCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAgentTaskStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteAgentTaskStatusCommand request, CancellationToken cancellationToken)
        {
            //user can not delete system agent status tasks
            if (request.Id < (int)SystemAgentTaskStatus.Completed)
            {
                throw new RestApiResponseException(ResourceCommonMessage.CanNotDeleteSystemTaskStatus);
            }
            var entity = await _context.AgentTaskStatuses.Where(item => item.Id.Equals(request.Id))
                .FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new RestApiResponseException(ResourceCommonMessage.EntityNotFound);
            }
            var existingTask = await _context.AgentTasks.Where(item => item.AgentTaskStatusId.Equals(request.Id))
                .AnyAsync();
            if (existingTask)
            {
                throw new RestApiResponseException(ResourceCommonMessage.CanNotDeleteTaskStatus);
            }
            _context.AgentTaskStatuses.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
