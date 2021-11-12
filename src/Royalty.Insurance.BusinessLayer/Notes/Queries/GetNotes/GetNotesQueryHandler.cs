using MediatR;
using Royalty.Insurance.BusinessLayer.Extensions;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, PaginationResponse<NoteResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly INoteMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetNotesQueryHandler(INoteMapperService mapper, IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _mapper = mapper;
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<PaginationResponse<NoteResponse>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Notes
                .Where(x => x.UserId == _currentUser.UserId && x.InsuredId == null)
                .OrderByDescending(x => x.CreateDateTime).ToPaginationAsync(_mapper.MapResponse, request.PageIndex, request.PageSize);
            if (entities.RowCount == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entities;
        }
    }
}
