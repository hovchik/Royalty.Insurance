using System.Threading;
using System.Threading.Tasks;
using Core.System.Delta;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Delta
{
    public class BillingAccountInformationCommandHandler : IRequestHandler<BillingAccountInformationCommand, BillingAccountInformationViewModel>
    {
        private readonly IBillingAccountInformation _billingAccountInformation;

        public BillingAccountInformationCommandHandler(IBillingAccountInformation billingAccountInformation)
        {
            _billingAccountInformation = billingAccountInformation;
        }

        public async Task<BillingAccountInformationViewModel> Handle(BillingAccountInformationCommand request, CancellationToken cancellationToken)
        {
            return await _billingAccountInformation.SetUpAsync(request, cancellationToken);
        }
    }
}
