using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.States.Queries.Mapper;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.States.Queries.GetStateByName
{
    public class GetStateByNameQueryHandler : IRequestHandler<GetStateByNameQuery, StateResponse>
    {
        private readonly IStateMapperService _mapper;
        private readonly IApplicationDbContext _context;

        public GetStateByNameQueryHandler(IApplicationDbContext context, IStateMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StateResponse> Handle(GetStateByNameQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.States
                .Select(_mapper.MapResponse)
                .FirstOrDefaultAsync(st => st.Name.Equals(request.StateName), cancellationToken);
            if (entity == null)
            {
                throw new RestApiResponseException((int) HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);

            }

            return entity;
        }
    }
}
