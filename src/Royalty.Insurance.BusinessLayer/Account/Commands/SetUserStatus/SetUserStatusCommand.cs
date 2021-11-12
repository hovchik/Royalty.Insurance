using MediatR;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class SetUserStatusCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public UserStatusCode UserStatus { get; set; }
    }
}
