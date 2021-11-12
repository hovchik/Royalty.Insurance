using LinqKit;
using MediatR;
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
    public class CreateVehicleInfoCommandHandler : IRequestHandler<CreateVehicleInfoCommand, VehicleInfoResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IVehicleInfoMapperService _mapper;

        public CreateVehicleInfoCommandHandler(IVehicleInfoMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<VehicleInfoResponse> Handle(CreateVehicleInfoCommand request, CancellationToken cancellationToken)
        {
            VehicleInfo entity = new VehicleInfo();
            _mapper.UpdateEntity(entity, request);
            await _context.VehicleInfos.AddAsync(entity);

            if (await _context.SaveChangesAsync(new CancellationToken()) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
