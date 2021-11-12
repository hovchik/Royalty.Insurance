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

namespace Royalty.Insurance.BusinessLayer.Insureds
{
    public class GetInsuredsQueryHandler : IRequestHandler<GetInsuredsQuery, PaginationResponse<InsuredResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IInsuredMapperService _mapper;

        public GetInsuredsQueryHandler(IInsuredMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaginationResponse<InsuredResponse>> Handle(GetInsuredsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Insureds.OrderByDescending(x => x.CreateDatetimeUtc).ToPaginationAsync(_mapper.MapResponse, request.PageIndex, request.PageSize);
            if (entities.RowCount == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entities;
        }
    }
}
