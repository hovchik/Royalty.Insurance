using Application.Interfaces;
using Domain;
using LinqKit;
using MediatR;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Commodities
{
    public class CreateCommodityCommandHandler : IRequestHandler<CreateCommodityCommand, CommodityResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICommodityMapperService _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateCommodityCommandHandler(ICurrentUserService currentUserService, ICommodityMapperService mapper, IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
            _context = context;
        }

        public async Task<CommodityResponse> Handle(CreateCommodityCommand request, CancellationToken cancellationToken)
        {
            Commodity entity = new Commodity { CreateBy = _currentUserService.UserId, UpdatedBy = _currentUserService.UserId };
            _mapper.UpdateEntity(entity, request.Request);
            await _context.Commodities.AddAsync(entity, cancellationToken);
            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
