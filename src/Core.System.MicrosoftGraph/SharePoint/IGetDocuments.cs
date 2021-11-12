using System.Threading;
using System.Threading.Tasks;

namespace Core.System.MicrosoftGraph
{
    public interface IGetDocuments
    {
        Task<DocumentListViewModel> Handle(GetDocumentsRequest request, CancellationToken cancellationToken);
    }
}
