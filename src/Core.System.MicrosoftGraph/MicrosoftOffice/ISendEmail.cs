using System.Threading;
using System.Threading.Tasks;

namespace Core.System.MicrosoftGraph.MicrosoftOffice
{
    public interface ISendEmail
    {
        Task Handle(MicrosoftOfficeMessageRequest request, CancellationToken cancellationToken);
    }
}
