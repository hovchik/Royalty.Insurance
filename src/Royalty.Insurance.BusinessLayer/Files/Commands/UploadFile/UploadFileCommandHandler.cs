using System.Common.Authentication.Models;
using System.Common.Constants;
using System.Common.Exceptions;
using System.Common.Storage;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Core.System.Security.Cryptography;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Domain;

namespace Royalty.Insurance.BusinessLayer.Files
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, UserFileResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IStorageManager _storageManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly AppSetting _appSetting;
        private readonly IUserGarageMapperService _mapper;
        private readonly IExpiryQueryParameterCreator _expiryQueryParameterCreator;

        public UploadFileCommandHandler(IApplicationDbContext context, IStorageManager storageManager, IOptions<AppSetting> options, IUserGarageMapperService mapper, ICurrentUserService currentUserService, IExpiryQueryParameterCreator expiryQueryParameterCreator)
        {
            _context = context;
            _storageManager = storageManager;
            _appSetting = options.Value;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _expiryQueryParameterCreator = expiryQueryParameterCreator;
        }

        public async Task<UserFileResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            if (!await SpaceOk(request.File, _currentUserService.UserId))
            {
                throw new RestApiResponseException((int)HttpStatusCode.UnsupportedMediaType,
                    ResourceCommonMessage.LargeFileUploading);
            }
            var isRecordExists =
                await _context.UserGarages.FirstOrDefaultAsync(x => x.Path == request.File.FileName && x.UserId == _currentUserService.UserId,
                    cancellationToken); // check if file is the same and owner is current user
            if (isRecordExists != null && !request.OverWriteExisting) // do not store if already exists and OverWriteExisting is false
            {
                throw new RestApiResponseException(ResourceCommonMessage.FileAlreadyExists);
            }
            var uploadedFilePath = await _storageManager.UploadAsync(request.File, Constants.Garage,
                _currentUserService.UserId.ToString(), request.File.FileName);
            UserGarage entity = new UserGarage();
            _mapper.UpdateEntity(entity, request, uploadedFilePath.FileName, _currentUserService.UserId);


            if (isRecordExists == null || isRecordExists.Path != request.File.FileName)
            {
                await _context.UserGarages.AddAsync(entity, cancellationToken);

                if (await _context.SaveChangesAsync(cancellationToken) != 1)
                {
                    throw new RestApiResponseException((int)HttpStatusCode.InternalServerError,
                        ResourceCommonMessage.SaveFailed);
                }

            }
            else
            {
                entity.Id = isRecordExists.Id;
            }

            var record = await _context.UserGarages.Where(item => item.Id.Equals(entity.Id)
                                                                  && item.UserId.Equals(_currentUserService.UserId))
                .Include(item => item.AssignedInsured)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.MapResponse.Invoke(record, _expiryQueryParameterCreator, _appSetting);
        }

        private async Task<bool> SpaceOk(IFormFile requestFile, int userId)
        {
            long fileSize = requestFile.Length;
            long? alreadyUsedSize = await _storageManager.GetContainerSizeAsync(userId, Constants.Garage);
            if (!alreadyUsedSize.HasValue)
            {
                return false;
            }
            return fileSize + alreadyUsedSize.Value < _appSetting.GarageSize;
        }
    }
}
