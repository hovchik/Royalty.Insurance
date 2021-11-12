using Core.System.MicrosoftGraph.MicrosoftOffice;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.MicrosoftOffice.Commands
{
    public class SendEmailCommand : MicrosoftOfficeMessageRequest, IRequest<Unit>
    {
    }
}
