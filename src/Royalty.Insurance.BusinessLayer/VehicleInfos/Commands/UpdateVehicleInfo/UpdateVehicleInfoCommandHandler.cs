using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Royalty.Insurance.BusinessLayer.VehicleInfos
{
    public class UpdateVehicleInfoCommandHandler : IRequestHandler<UpdateVehicleInfoCommand, VehicleInfoResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IVehicleInfoMapperService _mapper;

        public UpdateVehicleInfoCommandHandler(IVehicleInfoMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<VehicleInfoResponse> Handle(UpdateVehicleInfoCommand request, CancellationToken cancellationToken)
        {
            VehicleInfo entity = await _context.VehicleInfos.FirstOrDefaultAsync(item => item.Id.Equals(request.Id), cancellationToken);
            _mapper.UpdateEntity(entity, request);
            _context.VehicleInfos.Update(entity);
            if (await _context.SaveChangesAsync(new CancellationToken()) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
