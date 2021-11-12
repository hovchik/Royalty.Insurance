using System;
using System.Common.Authentication.Models;
using System.Linq.Expressions;
using Core.System.Security.Cryptography;
using Domain;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Files
{
    public class UserGarageMapperService : IUserGarageMapperService
    {
        public void UpdateEntity(UserGarage entity, UploadFileCommand request, string path, int userId)
        {
            entity.AssignedInsuredId = request.AssignedTo;
            entity.FileFormatId = request.FileFormatId;
            entity.FileName = request.File.FileName;
            entity.UserId = userId;
            entity.CreateDatetimeUtc = DateTime.UtcNow;
            entity.Path = path;
        }

        public Expression<Func<UserGarage, IExpiryQueryParameterCreator, AppSetting,  UserFileResponse>> MapResponse
        {
            get
            {
                return (entity, expiryQueryParameterCreator, appSetting) => 
                    new UserFileResponse(expiryQueryParameterCreator, appSetting)
                {
                    UserId = entity.UserId,
                    Id = entity.Id,
                    Path = entity.Path,
                    FileName = entity.FileName,
                    AssignToFullName = entity.AssignedInsured != null
                        ? (entity.AssignedInsured.LegalStatusId == (int)LegalStatusType.Individual
                            ? entity.AssignedInsured.GaragingName
                            : entity.AssignedInsured.MailingName)
                        : string.Empty,
                    AssignToId =  entity.AssignedInsuredId,
                        FileFormatId = entity.FileFormatId,
                        CreateDateTime = entity.CreateDatetimeUtc,
                };
            }
        }
    }
}
