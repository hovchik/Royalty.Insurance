using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.MicrosoftOffice;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.MicrosoftOffice.Queries.GetConversation
{
    public class GetConversationQueryHandler : IRequestHandler<GetConversationQuery, IEnumerable<MicrosoftOfficeMessageResponse>>
    {
        private readonly IGetConversation _getConversation;

        public GetConversationQueryHandler(IGetConversation getConversation)
        {
            _getConversation = getConversation;
        }

        public async Task<IEnumerable<MicrosoftOfficeMessageResponse>> Handle(GetConversationQuery request,
            CancellationToken cancellationToken) =>
            await _getConversation.Handle(request, cancellationToken);
    }
}
