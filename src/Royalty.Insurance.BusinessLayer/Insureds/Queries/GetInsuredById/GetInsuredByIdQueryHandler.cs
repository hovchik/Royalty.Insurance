using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class GetInsuredByIdQueryHandler : IRequestHandler<GetInsuredByIdQuery, InsuredResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IInsuredMapperService _mapper;

        public GetInsuredByIdQueryHandler(IInsuredMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<InsuredResponse> Handle(GetInsuredByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Insureds
                .Where(item => item.Id.Equals(request.Id))
                .Select(_mapper.MapResponse)
                .FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entity;
        }
    }
}
