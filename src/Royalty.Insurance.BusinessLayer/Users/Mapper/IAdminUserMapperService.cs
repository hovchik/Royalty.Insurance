using Domain;
using Royalty.Insurance.Proxy.Request;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public interface IAdminUserMapperService
    {
        void UpdateEntity(User entity, UpdateUserByAdminCommand request);
        //TODo: maybe need to removed or can inherit from IBaseUserProfileMapperService
        //Expression<Func<User, IExpiryQueryParameterCreator, AppSetting, UserResponse>> MapResponse { get; }
    }
}