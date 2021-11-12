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
    public class GetCargoesByIdQueryHandler : IRequestHandler<GetCargoesByIdQuery, CargoListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICargoMapperService _mapper;

        public GetCargoesByIdQueryHandler(IApplicationDbContext context, ICargoMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CargoListViewModel> Handle(GetCargoesByIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Cargos.Where(cargo => cargo.Id == request.CargoId).Select(_mapper.MapResponse).ToListAsync(cancellationToken);
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