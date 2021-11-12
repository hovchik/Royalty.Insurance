using System.Threading;
using System.Threading.Tasks;
using Core.System.Delta;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Delta.Commands.PolicyReinstatement
{
    public class PolicyReinstatementCommandHandler : IRequestHandler<PolicyReinstatementCommand, PolicyReinstatementViewModel>
    {
        private readonly IPolicyReinstatement _policyReinstatement;

        public PolicyReinstatementCommandHandler(IPolicyReinstatement policyReinstatement)
        {
            _policyReinstatement = policyReinstatement;
        }

        public async Task<PolicyReinstatementViewModel> Handle(PolicyReinstatementCommand request, CancellationToken cancellationToken)
        {
            return await _policyReinstatement.SetUpAsync(request, cancellationToken);
        }
    }
}
