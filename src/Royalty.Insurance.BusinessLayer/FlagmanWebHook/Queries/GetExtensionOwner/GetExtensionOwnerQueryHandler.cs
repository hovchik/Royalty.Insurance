using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.UserPhoneSettings;
using Application.Interfaces;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.FlagmanWebHook
{
    internal class GetExtensionOwnerQueryHandler : IRequestHandler<GetExtensionOwnerQuery, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserPhoneMapperService _userMapper;

        public GetExtensionOwnerQueryHandler(IApplicationDbContext context, IUserPhoneMapperService userMapper)
        {
            _context = context;
            _userMapper = userMapper;
        }

        public async Task<int> Handle(GetExtensionOwnerQuery request, CancellationToken cancellationToken)
        {
            var userId = (await _context.UserPhones
                .Where(item => item.Extension == request.UserExtensionId)
                .Select(_userMapper.MapResponse)
                .FirstOrDefaultAsync())?.UserOwnerId;
            if (!userId.HasValue)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return userId.Value;
        }
    }
}