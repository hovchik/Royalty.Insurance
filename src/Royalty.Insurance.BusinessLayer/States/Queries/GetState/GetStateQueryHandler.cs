using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.States.Queries.Mapper;
using Application.Interfaces;
using Royalty.Insurance.MapperService;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.States.Queries.GetState
{
    public class GetStateQueryHandler : IRequestHandler<GetStateQuery, StateListViewModel>
    {
        private readonly IStateMapperService _mapper;
        private readonly IApplicationDbContext _context;

        public GetStateQueryHandler(IStateMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<StateListViewModel> Handle(GetStateQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.States
                .Select(_mapper.MapResponse)
                .ToListAsync(cancellationToken);
            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);

            }

            return new StateListViewModel
            {
                States = entities
            };
        }
    }
}
