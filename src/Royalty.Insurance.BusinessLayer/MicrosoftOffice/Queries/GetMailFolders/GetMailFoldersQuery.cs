using System.Collections.Generic;
using Core.System.MicrosoftGraph.MicrosoftOffice;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.MicrosoftOffice.Queries
{
    public class GetMailFoldersQuery : GetUserRequest, IRequest<IEnumerable<MicrosoftOfficeMailFolderResponse>>
    {
    }
}
