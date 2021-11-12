using MediatR;

namespace Royalty.Insurance.BusinessLayer.GroupMembers
{
    public class MuteCommand : IRequest<Unit>
    {
        public bool Mute { get; set; }

        public int GroupId { get; set; }
        
        public int UserId { get; set; }
    }
}
