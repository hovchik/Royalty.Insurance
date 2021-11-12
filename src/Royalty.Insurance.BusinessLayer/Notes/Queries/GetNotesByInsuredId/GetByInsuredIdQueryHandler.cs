using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class GetByInsuredIdQueryHandler : IRequestHandler<GetByInsuredIdQuery, NoteResponseListView>
    {
        private readonly IApplicationDbContext _context;

        public GetByInsuredIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<NoteResponseListView> Handle(GetByInsuredIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Notes.Where(note => note.InsuredId != null && note.InsuredId == request.InsuredId)
                .Select(item => new NoteResponse
                {
                    Id = item.Id,
                    CreatedDateTime = item.CreateDateTime,
                    InsuredId = item.InsuredId,
                    Note = item.Description,
                    UserId = item.UserId
                }
                )
                .ToListAsync(cancellationToken);

            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound,
                   ResourceCommonMessage.EntityNotFound);
            }

            return new NoteResponseListView
            {
                Notes = entities
            };
        }
    }
}
