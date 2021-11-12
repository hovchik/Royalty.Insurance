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

namespace Royalty.Insurance.BusinessLayer.VehicleInfos
{
    public class GetVehicleInfoByIdQueryHandler : IRequestHandler<GetVehicleInfoByIdQuery, VehicleInfoResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IVehicleInfoMapperService _mapper;

        public GetVehicleInfoByIdQueryHandler(IVehicleInfoMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<VehicleInfoResponse> Handle(GetVehicleInfoByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.VehicleInfos
                         .Where(item => item.Id.Equals(request.Id))
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
