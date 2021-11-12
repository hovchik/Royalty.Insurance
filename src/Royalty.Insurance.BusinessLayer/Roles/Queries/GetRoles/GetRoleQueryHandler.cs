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

namespace Royalty.Insurance.BusinessLayer.Roles.Queries.GetRoles
{
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, List<RoleResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IRoleMapperService _mapper;

        public GetRoleQueryHandler(IRoleMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<RoleResponse>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Roles.Select(_mapper.MapResponse).ToListAsync();
            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entities;
        }
    }
}
