using Application.Interfaces;
using Core.System.Security.Cryptography;
using Domain;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Authentication.Models;
using System.Common.Exceptions;
using System.Common.Storage;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class UploadUserAvatarCommandHandler : IRequestHandler<UploadUserAvatarCommand, UserResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPersonalUserMapperService _personalUserMapper;
        private readonly IBaseUserProfileMapperService _baseUserMapper;
        private readonly IExpiryQueryParameterCreator _expiryQueryParameterCreator;
        private readonly AppSetting _appSetting;
        private readonly ICurrentUserService _currentUser;
        private readonly IStorageManager _storageManager;

        public UploadUserAvatarCommandHandler(ICurrentUserService currentUser, IOptions<AppSetting> appSetting, IExpiryQueryParameterCreator expiryQueryParameterCreator, IBaseUserProfileMapperService baseUserMapper, IPersonalUserMapperService personalUserMapper, IApplicationDbContext context, IStorageManager storageManager)
        {
            _currentUser = currentUser;
            _appSetting = appSetting.Value;
            _expiryQueryParameterCreator = expiryQueryParameterCreator;
            _baseUserMapper = baseUserMapper;
            _personalUserMapper = personalUserMapper;
            _context = context;
            _storageManager = storageManager;
        }

        public async Task<UserResponse> Handle(UploadUserAvatarCommand Request, CancellationToken cancellationToken)
        {
            UserPersonalRequest request = await _context.Users.Where(x => x.Id.Equals(_currentUser.UserId)).Select(_personalUserMapper.MapResponsePersonal).FirstOrDefaultAsync().ConfigureAwait(false);
            request.PersonalAvatar = (await _storageManager.UploadAsync(Request.File, Request.FileContainer, _currentUser.UserId.ToString(), $"avatar_user_{_currentUser.UserId}{Path.GetExtension(Request.File.FileName)}")).FileName;

            User entity = await _context.Users.Where(x => x.Id.Equals(_currentUser.UserId)).FirstOrDefaultAsync();
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
