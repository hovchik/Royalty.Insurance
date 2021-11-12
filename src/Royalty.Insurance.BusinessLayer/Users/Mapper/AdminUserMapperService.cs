using Domain;
using Royalty.Insurance.Proxy.Request;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class AdminUserMapperService :  IAdminUserMapperService
    {

        public void UpdateEntity(User entity, UpdateUserByAdminCommand request)
        {
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.CellPhone = request.CellPhone;
            entity.WorkPhone = request.WorkPhone;
            entity.HomePhone = request.HomePhone;
            entity.UserRoleId = (int)request.Role;
        }
    }
}