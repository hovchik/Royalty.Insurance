using Application.Interfaces;
using Domain;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Coverages
{
    public class UpdateCoverageCommandHandler : IRequestHandler<UpdateCoverageCommand, CoverageResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICoverageMapperService _mapper;

        public UpdateCoverageCommandHandler(ICoverageMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CoverageResponse> Handle(UpdateCoverageCommand request, CancellationToken cancellationToken)
        {
            Coverage entity = await _context.Coverages.Where(item => item.Id.Equals(request.Id)).FirstOrDefaultAsync(cancellationToken);

            _mapper.UpdateEntity(entity, request.Request);
            _context.Coverages.Update(entity);
            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
