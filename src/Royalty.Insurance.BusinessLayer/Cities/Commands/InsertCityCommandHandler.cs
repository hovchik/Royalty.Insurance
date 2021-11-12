using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Cities
{
    public class InsertCityCommandHandler : IRequestHandler<InsertCityCommand, CityResponse>
    {
        private readonly ICityMapperService _mapper;
        private readonly IApplicationDbContext _context;

        public InsertCityCommandHandler(IApplicationDbContext context, ICityMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CityResponse> Handle(InsertCityCommand request, CancellationToken cancellationToken)
        {
            City entity = new City();
            _mapper.UpdateEntity(entity, request);
            var state = await _context.States.FirstOrDefaultAsync(item => item.Name.Equals(request.Name), cancellationToken);
            if (state == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            entity.StateId = state.Id;
            await _context.Cities.AddAsync(entity, cancellationToken);
            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError,
                    ResourceCommonMessage.SaveFailed);
            }

            return await _context.Cities.Select(_mapper.MapResponse)
                                 .FirstOrDefaultAsync(c => c.Id.Equals(entity.Id), cancellationToken);
        }
    }
}
