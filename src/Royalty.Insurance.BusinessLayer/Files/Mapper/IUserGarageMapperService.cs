using System;
using System.Common.Authentication.Models;
using System.Linq.Expressions;
using Core.System.Security.Cryptography;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Files
{
    public interface IUserGarageMapperService
    {
        void UpdateEntity(UserGarage entity, UploadFileCommand request, string path, int userId);
        Expression<Func<UserGarage, IExpiryQueryParameterCreator, AppSetting, UserFileResponse>> MapResponse { get; }
    }
}
