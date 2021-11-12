using Application.Interfaces;
using MediatR;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.Extensions;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class GetNotesByDateRangeQueryHandler : IRequestHandler<GetNotesByDateRangeQuery, PaginationResponse<NoteResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly INoteMapperService _mapper;

        public GetNotesByDateRangeQueryHandler(INoteMapperService mapper, IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _mapper = mapper;
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<PaginationResponse<NoteResponse>> Handle(GetNotesByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Notes
                .Where(note => note.UserId == _currentUser.UserId && note.InsuredId == null && note.CreateDateTime.Date >= request.From.Date && note.CreateDateTime.Date <= request.To.Date)
                .OrderByDescending(x => x.CreateDateTime).ToPaginationAsync(_mapper.MapResponse, request.PageIndex, request.PageSize);
            if (entities.RowCount == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entities;
        }
    }
}
