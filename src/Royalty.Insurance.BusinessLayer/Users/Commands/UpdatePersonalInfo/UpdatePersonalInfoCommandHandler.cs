using Application.Interfaces;
using Core.System.Security.Cryptography;
using Domain;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
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
    public class UpdatePersonalInfoCommandHandler : IRequestHandler<UpdatePersonalInfoCommand, UserResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPersonalUserMapperService _personalUserMapper;
        private readonly IBaseUserProfileMapperService _baseUserMapper;
        private readonly IExpiryQueryParameterCreator _expiryQueryParameterCreator;
        private readonly AppSetting _appSetting;
        private readonly ICurrentUserService _currentUser;

        public UpdatePersonalInfoCommandHandler(IPersonalUserMapperService personalUserMapper, IApplicationDbContext context, IBaseUserProfileMapperService baseUserMapper, IOptions<AppSetting> appSetting, IExpiryQueryParameterCreator expiryQueryParameterCreator, ICurrentUserService currentUser)
        {
            _personalUserMapper = personalUserMapper;
            _context = context;
            _baseUserMapper = baseUserMapper;
            _appSetting = appSetting.Value;
            _expiryQueryParameterCreator = expiryQueryParameterCreator;
            _currentUser = currentUser;
        }

        public async Task<UserResponse> Handle(UpdatePersonalInfoCommand request, CancellationToken cancellationToken)
        {
            User entity = await _context.Users.Where(item => item.Id == _currentUser.UserId).FirstOrDefaultAsync();

            _personalUserMapper.UpdateEntity(entity, request);
            _context.Users.Update(entity);
            if (await _context.SaveChangesAsync(new CancellationToken()) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _baseUserMapper.MapResponse.Invoke(entity, _expiryQueryParameterCreator, _appSetting);
        }
    }
}
