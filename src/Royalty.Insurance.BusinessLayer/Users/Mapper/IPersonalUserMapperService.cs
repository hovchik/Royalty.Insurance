using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using System;
using System.Linq.Expressions;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public interface IPersonalUserMapperService
    {
        void UpdateEntity(User entity, UpdatePersonalInfoCommand request);
        Expression<Func<User, UpdatePersonalInfoCommand>> MapResponse { get; }
        Expression<Func<User, UserPersonalRequest>> MapResponsePersonal { get; }
        void UpdateEntity(User entity, UserPersonalRequest request);
    }
}