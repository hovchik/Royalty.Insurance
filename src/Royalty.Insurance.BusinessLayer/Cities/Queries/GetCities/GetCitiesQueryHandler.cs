using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Cities.Queries
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, CityListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICityMapperService _mapper;

        public GetCitiesQueryHandler(IApplicationDbContext context, ICityMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CityListViewModel> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Cities.Select(_mapper.MapResponse).ToListAsync(cancellationToken);
            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }


            return new CityListViewModel
            {
                Cities = entities
            };
        }
    }
}
