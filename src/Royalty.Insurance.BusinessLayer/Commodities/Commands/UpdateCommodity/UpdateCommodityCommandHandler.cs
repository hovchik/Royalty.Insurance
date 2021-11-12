using Application.Interfaces;
using Domain;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Commodities
{
    public class UpdateCommodityCommandHandler : IRequestHandler<UpdateCommodityCommand, CommodityResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICommodityMapperService _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCommodityCommandHandler(ICurrentUserService currentUserService, ICommodityMapperService mapper, IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
            _context = context;
        }
        public async Task<CommodityResponse> Handle(UpdateCommodityCommand request, CancellationToken cancellationToken)
        {
            Commodity entity = await _context.Commodities.Where(item => item.Id.Equals(request.Id)).FirstOrDefaultAsync();
            entity.UpdatedBy = _currentUserService.UserId;
            _mapper.UpdateEntity(entity, request.Request);
            _context.Commodities.Update(entity);
            if (await _context.SaveChangesAsync(new CancellationToken()) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
