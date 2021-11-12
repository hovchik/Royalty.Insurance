using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.System.MicrosoftGraph.MicrosoftOffice
{
    public interface IGetMailFolder
    {
        Task<IEnumerable<MicrosoftOfficeMailFolderResponse>> Handle(GetUserRequest request, CancellationToken cancellationToken);
    }
}
