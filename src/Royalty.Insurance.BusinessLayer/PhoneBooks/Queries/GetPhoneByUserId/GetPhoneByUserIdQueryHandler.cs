using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.PhoneBooks
{
    public class GetPhoneByUserIdQueryHandler : IRequestHandler<GetPhoneByUserIdQuery, List<PhoneBookResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPhoneBookMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetPhoneByUserIdQueryHandler(IApplicationDbContext context, IPhoneBookMapperService mapper, ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<List<PhoneBookResponse>> Handle(GetPhoneByUserIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.PhoneBooks
                .Where(num => num.UserId == _currentUser.UserId)
                .Select(_mapper.MapResponse)
                .ToListAsync();
            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound,ResourceCommonMessage.EntityNotFound);
            }

            return entities;
        }
    }
}
