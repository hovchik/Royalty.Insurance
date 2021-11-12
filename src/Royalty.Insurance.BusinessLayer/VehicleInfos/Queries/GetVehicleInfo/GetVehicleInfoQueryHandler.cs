using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.VehicleInfos
{
    public class GetVehicleInfoQueryHandler : IRequestHandler<GetVehicleInfoQuery, List<VehicleInfoResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IVehicleInfoMapperService _mapper;

        public GetVehicleInfoQueryHandler(IVehicleInfoMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<VehicleInfoResponse>> Handle(GetVehicleInfoQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.VehicleInfos
                 .Select(_mapper.MapResponse)
                   .ToListAsync(cancellationToken);
            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entities;
        }
    }
}
