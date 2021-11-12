using Domain;
using Royalty.Insurance.Proxy.Request;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public interface IUserMapperService : IBaseUserProfileMapperService
    {
        void UpdateEntity(User entity, CreateUserProfileCommand request);
    }
}