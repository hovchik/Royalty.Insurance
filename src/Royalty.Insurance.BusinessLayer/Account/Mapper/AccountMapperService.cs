using System;
using System.Common.Authentication.Models;
using System.Linq.Expressions;
using Core.System.Security.Cryptography;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class AccountMapperService :  IAccountMapperService
    {

        public Expression<Func<User, IExpiryQueryParameterCreator, AppSetting, LoginResponse>> MapResponse
        {
            get
            {
                return (entity, expiryQueryParameterCreator, appSetting) => new LoginResponse(expiryQueryParameterCreator, appSetting)
                {
                    FullName = $"{entity.FirstName} {entity.LastName}",
                    UserId = entity.Id,
                    PersonalAvatar = entity.PersonalAvatar
                };
            }
        }

        public void UpdateEntity(User entity, UserRequest request)
        {
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Email = request.Email.ToLower();
            entity.CellPhone = request.CellPhone;
            entity.WorkPhone = request.WorkPhone;
            entity.HomePhone = request.HomePhone;
            entity.Iteration = 10000;
            var passwordResult  = PasswordHasher.Generate(request.Password, entity.Iteration);
            entity.Password = passwordResult.PasswordHash;
            entity.Salting = passwordResult.Salting;
        }

    }
}
