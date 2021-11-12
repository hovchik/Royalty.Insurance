using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.MicrosoftOffice;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.MicrosoftOffice.Queries
{
    public class GetMailFoldersHandler : IRequestHandler<GetMailFoldersQuery, IEnumerable<MicrosoftOfficeMailFolderResponse>>
    {
        private readonly IGetMailFolder _getMailFolders;

        public GetMailFoldersHandler(IGetMailFolder getMailFolders)
        {
            _getMailFolders = getMailFolders;
        }

        public async Task<IEnumerable<MicrosoftOfficeMailFolderResponse>> Handle(GetMailFoldersQuery request,
            CancellationToken cancellationToken) =>
            await _getMailFolders.Handle(request, cancellationToken);
    }
}
