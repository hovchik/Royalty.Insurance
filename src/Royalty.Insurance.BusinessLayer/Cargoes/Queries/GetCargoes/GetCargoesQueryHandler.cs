using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Cargoes.Queries
{
    public class GetCargoesQueryHandler : IRequestHandler<GetCargoesQuery, CargoListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICargoMapperService _mapper;

        public GetCargoesQueryHandler(IApplicationDbContext context, ICargoMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CargoListViewModel> Handle(GetCargoesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Cargos.Select(_mapper.MapResponse).ToListAsync(cancellationToken);
            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }


            return new CargoListViewModel
            {
                Cargos = entities
            };
        }
    }
}