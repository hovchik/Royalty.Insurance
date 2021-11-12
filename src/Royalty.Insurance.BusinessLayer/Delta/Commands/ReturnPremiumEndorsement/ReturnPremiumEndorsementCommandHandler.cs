using System.Threading;
using System.Threading.Tasks;
using Core.System.Delta;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Delta
{
    public class ReturnPremiumEndorsementCommandHandler : IRequestHandler<ReturnPremiumEndorsementCommand, ReturnPremiumEndorsementViewModel>
    {
        private readonly IReturnPremiumEndorsement _returnPremiumEndorsement;

        public ReturnPremiumEndorsementCommandHandler(IReturnPremiumEndorsement returnPremiumEndorsement)
        {
            _returnPremiumEndorsement = returnPremiumEndorsement;
        }

        public async Task<ReturnPremiumEndorsementViewModel> Handle(ReturnPremiumEndorsementCommand request, CancellationToken cancellationToken)
        {
            return await _returnPremiumEndorsement.SetUpAsync(request, cancellationToken);
        }
    }
}
