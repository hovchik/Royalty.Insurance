using Core.System.Delta;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Delta
{
    public class ReturnPremiumEndorsementCommand : ReturnPremiumEndorsementRequest, IRequest<ReturnPremiumEndorsementViewModel>
    {
    }
}
