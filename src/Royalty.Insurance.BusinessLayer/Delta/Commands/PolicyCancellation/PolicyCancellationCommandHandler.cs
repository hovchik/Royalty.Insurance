using System.Threading;
using System.Threading.Tasks;
using Core.System.Delta;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Delta.Commands.PolicyCancellation
{
    public class PolicyCancellationCommandHandler : IRequestHandler<PolicyCancellationCommand, PolicyCancellationViewModel>
    {
        private readonly IPolicyCancellation _policyCancellation;

        public PolicyCancellationCommandHandler(IPolicyCancellation policyCancellation)
        {
            _policyCancellation = policyCancellation;
        }

        public async Task<PolicyCancellationViewModel> Handle(PolicyCancellationCommand request, CancellationToken cancellationToken)
        {
            return await _policyCancellation.SetUpAsync(request, cancellationToken);
        }
    }
}
