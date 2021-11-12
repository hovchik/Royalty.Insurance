using Application.Interfaces;
using Domain;
using LinqKit;
using MediatR;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Coverages
{
    public class CreateCoverageCommandHandler : IRequestHandler<CreateCoverageCommand, CoverageResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICoverageMapperService _mapper;

        public CreateCoverageCommandHandler(ICoverageMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CoverageResponse> Handle(CreateCoverageCommand request, CancellationToken cancellationToken)
        {
            Coverage entity = new Coverage();
            _mapper.UpdateEntity(entity, request.Request);
            await _context.Coverages.AddAsync(entity, cancellationToken);

            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
