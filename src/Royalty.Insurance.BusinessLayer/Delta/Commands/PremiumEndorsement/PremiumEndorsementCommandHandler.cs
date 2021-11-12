using System.Threading;
using System.Threading.Tasks;
using Core.System.Delta;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Delta
{
    public class PremiumEndorsementCommandHandler : IRequestHandler<PremiumEndorsementCommand, PremiumEndorsementViewModel>
    {
        private readonly IAdditionalPremiumEndorsement _premiumEndorsement;

        public PremiumEndorsementCommandHandler(IAdditionalPremiumEndorsement premiumEndorsement)
        {
            _premiumEndorsement = premiumEndorsement;
        }

        public async Task<PremiumEndorsementViewModel> Handle(PremiumEndorsementCommand request, CancellationToken cancellationToken)
        {
            return await _premiumEndorsement.SetUpAsync(request, cancellationToken);
        }
    }
}
