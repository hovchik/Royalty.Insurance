using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.MicrosoftOffice;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.MicrosoftOffice.Commands
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Unit>
    {
        private readonly ISendEmail _sendEmail;

        public SendEmailCommandHandler(ISendEmail sendEmail)
        {
            _sendEmail = sendEmail;
        }

        public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            await _sendEmail.Handle(request, cancellationToken);

            return Unit.Value;
        }
    }
}
