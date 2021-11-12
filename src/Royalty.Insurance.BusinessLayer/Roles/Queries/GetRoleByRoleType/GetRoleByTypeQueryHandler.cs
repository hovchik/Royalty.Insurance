using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Roles
{
    public class GetRoleByTypeQueryHandler : IRequestHandler<GetRoleByTypeQuery, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IRoleMapperService _mapper;

        public GetRoleByTypeQueryHandler(IRoleMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<string> Handle(GetRoleByTypeQuery request, CancellationToken cancellationToken)
        {
            var entity = (await _context.Roles.FirstOrDefaultAsync(role => role.Type == (int)request.RoleType))?.Name;
            if (string.IsNullOrEmpty(entity))
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entity;
        }
    }
}
