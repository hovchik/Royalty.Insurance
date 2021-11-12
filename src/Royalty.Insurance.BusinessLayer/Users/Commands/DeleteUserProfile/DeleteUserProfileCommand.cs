
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class DeleteUserProfileCommand : IRequest<Unit>
    {
        public int UserId { get; set; }

        public string FileContainer { get; set; }
    }
}
