using Application.Interfaces;
using Core.System.Security.Cryptography;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Authentication.Models;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserMapperService _mapper;
        private readonly AppSetting _appSetting;
        private readonly IExpiryQueryParameterCreator _expiryQueryParameterCreator;

        public GetUserByIdQueryHandler(IExpiryQueryParameterCreator expiryQueryParameterCreator, IOptions<AppSetting> appSetting, IUserMapperService mapper, IApplicationDbContext context)
        {
            _expiryQueryParameterCreator = expiryQueryParameterCreator;
            _appSetting = appSetting.Value;
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(item => item.Id == request.Id && item.IsActive);
            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return _mapper.MapResponse.Compile().Invoke(entity, _expiryQueryParameterCreator, _appSetting);
        }
    }
}
