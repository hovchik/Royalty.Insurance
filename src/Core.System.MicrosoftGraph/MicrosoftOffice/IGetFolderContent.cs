using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.System.MicrosoftGraph.MicrosoftOffice
{
    public interface IGetFolderContent
    {
        Task<IEnumerable<MicrosoftOfficeMessageResponse>> Handle(GetFolderContentRequest request,
            CancellationToken cancellationToken);
    }
}
