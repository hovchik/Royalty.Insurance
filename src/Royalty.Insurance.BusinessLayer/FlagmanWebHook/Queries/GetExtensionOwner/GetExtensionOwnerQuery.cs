using MediatR;

namespace Royalty.Insurance.BusinessLayer.FlagmanWebHook
{
    public class GetExtensionOwnerQuery:IRequest<int>
    {
        public int UserExtensionId { get; set; }
    }
}   