using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.System.MicrosoftGraph.MicrosoftOffice
{
    public interface IGetConversation
    {
        Task<IEnumerable<MicrosoftOfficeMessageResponse>> Handle(GetConversationRequest request,
            CancellationToken cancellationToken);
    }
}
