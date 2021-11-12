using Royalty.Insurance.Proxy.Request;
using Core.System.Security.Cryptography;
using Domain;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class UserMapperService : BaseUserProfileMapperService, IUserMapperService
    {

        public void UpdateEntity(User entity, CreateUserProfileCommand request)
        {
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Email = request.Email.ToLower();
            entity.CellPhone = request.CellPhone;
            entity.WorkPhone = request.WorkPhone;
            entity.UserRoleId = (int)request.Role;
            entity.HomePhone = request.HomePhone;
            entity.Iteration = 10000;
            var passwordResult = PasswordHasher.Generate(request.Password, entity.Iteration);
            entity.Password = passwordResult.PasswordHash;
            entity.Salting = passwordResult.Salting;
            entity.UserRoleId = (int)request.Role;
        }
    }
}