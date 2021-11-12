using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.MicrosoftOffice;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.MicrosoftOffice.Queries
{
    public class GetFolderContentQueryHandler : IRequestHandler<GetFolderContentQuery, IEnumerable<MicrosoftOfficeMessageResponse>>
    {
        private readonly IGetFolderContent _getFolderContent;

        public GetFolderContentQueryHandler(IGetFolderContent getFolderContent)
        {
            _getFolderContent = getFolderContent;
        }

        public async Task<IEnumerable<MicrosoftOfficeMessageResponse>> Handle(GetFolderContentQuery request,
            CancellationToken cancellationToken) =>
            await _getFolderContent.Handle(request, cancellationToken);
    }
}
