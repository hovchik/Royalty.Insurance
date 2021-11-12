using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Royalty.Insurance.BusinessLayer.Extensions;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class GetByInsuredIdQueryHandler : IRequestHandler<GetByInsuredId, PaginationResponse<DocumentResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDocumentMapperService _mapper;

        public GetByInsuredIdQueryHandler(IApplicationDbContext context, IDocumentMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<DocumentResponse>> Handle(GetByInsuredId request, CancellationToken cancellationToken)
        {
            var entities = await _context.Documents
                .Where(item => !item.IsDeleted && item.InsuredId == request.InsuredId)
                .OrderByDescending(item => item.CreateDatetimeUtc)
                .ToPaginationAsync(_mapper.MapResponse, request.PageIndex, request.PageSize);

            return entities;
        }
    }
}
