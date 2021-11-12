using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class GetNoteByIdQueryhandler : IRequestHandler<GetNoteByIdQuery, NoteResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly INoteMapperService _mapper;

        public GetNoteByIdQueryhandler(INoteMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<NoteResponse> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound,
                   ResourceCommonMessage.EntityNotFound);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
