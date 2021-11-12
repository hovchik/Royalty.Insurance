using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, DocumentResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDocumentMapperService _mapper;

        public GetByIdQueryHandler(IApplicationDbContext context, IDocumentMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DocumentResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Documents
                .Where(item => !item.IsDeleted && item.Id.Equals(request.Id))
                .Select(_mapper.MapResponse)
                .FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entity;
        }
    }
}
