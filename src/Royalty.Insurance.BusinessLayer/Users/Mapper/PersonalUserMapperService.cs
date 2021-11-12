using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using System;
using System.Linq.Expressions;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class PersonalUserMapperService : IPersonalUserMapperService
    {
        public Expression<Func<User, UpdatePersonalInfoCommand>> MapResponse
        {
            get
            {
                return entity => new UpdatePersonalInfoCommand
                {
                    CellPhone = entity.CellPhone,
                    FirstName = entity.FirstName,
                    HomePhone = entity.HomePhone,
                    LastName = entity.LastName,
                    WorkPhone = entity.WorkPhone,
                    PersonalAvatar = entity.PersonalAvatar
                };
            }
        }

        public Expression<Func<User, UserPersonalRequest>> MapResponsePersonal
        {
            get
            {
                return entity => new UserPersonalRequest
                {
                    CellPhone = entity.CellPhone,
                    FirstName = entity.FirstName,
                    HomePhone = entity.HomePhone,
                    LastName = entity.LastName,
                    WorkPhone = entity.WorkPhone,
                    PersonalAvatar = entity.PersonalAvatar
                };
            }
        }

        public void UpdateEntity(User entity, UpdatePersonalInfoCommand request)
        {
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.CellPhone = request.CellPhone;
            entity.WorkPhone = request.WorkPhone;
            entity.HomePhone = request.HomePhone;
            entity.PersonalAvatar = request.PersonalAvatar;
        }

        public void UpdateEntity(User entity, UserPersonalRequest request)
        {
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.CellPhone = request.CellPhone;
            entity.WorkPhone = request.WorkPhone;
            entity.HomePhone = request.HomePhone;
            entity.PersonalAvatar = request.PersonalAvatar;
        }
    }
}