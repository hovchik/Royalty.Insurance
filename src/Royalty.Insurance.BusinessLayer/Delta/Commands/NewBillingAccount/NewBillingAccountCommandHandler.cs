using System.Threading;
using System.Threading.Tasks;
using Core.System.Delta;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Delta
{
    public class NewBillingAccountCommandHandler : IRequestHandler<NewBillingAccountCommand, DeltaBillingAccountViewModel>
    {
        private readonly INewBillingAccount _billingAccount;

        public NewBillingAccountCommandHandler(INewBillingAccount billingAccount)
        {
            _billingAccount = billingAccount;
        }

        public async Task<DeltaBillingAccountViewModel> Handle(NewBillingAccountCommand request, CancellationToken cancellationToken)
        {
            return await _billingAccount.SetUpAsync(request, cancellationToken);
        }
    }
}
