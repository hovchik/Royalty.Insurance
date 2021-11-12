using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Cities.Queries
{
    public class GetOrCreateCityQueryHandler : IRequestHandler<GetOrCreateCityQuery, CityResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICityMapperService _mapper;
        private readonly IRequestHandler<InsertCityCommand, CityResponse> _insertCityCommandHandler;

        public GetOrCreateCityQueryHandler(IApplicationDbContext context, ICityMapperService mapper, IRequestHandler<InsertCityCommand, CityResponse> insertCityCommandHandler)
        {
            _context = context;
            _mapper = mapper;
            _insertCityCommandHandler = insertCityCommandHandler;
        }

        public async Task<CityResponse> Handle(GetOrCreateCityQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Cities.Where(item => item.Name.Equals(request.Name))
                .Select(_mapper.MapResponse).FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                return await _insertCityCommandHandler.Handle(
                    new InsertCityCommand
                    {
                        Name = request.Name,
                        State = request.StateName
                    }, cancellationToken);
            }

            return entity;
        }
    }
}
