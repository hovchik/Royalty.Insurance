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

namespace Royalty.Insurance.BusinessLayer.UserPhoneSettings
{
    internal class GetUserPhoneQueryHandler : IRequestHandler<GetUserPhoneQuery, List<UserPhoneResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserPhoneMapperService _mapper;


        public GetUserPhoneQueryHandler(IApplicationDbContext context, IUserPhoneMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserPhoneResponse>> Handle(GetUserPhoneQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.UserPhones
                .Select(_mapper.MapResponse)
                .ToListAsync(cancellationToken);

            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entities;
        }
    }
}