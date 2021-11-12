using System.Threading;
using System.Threading.Tasks;

namespace Core.System.MicrosoftGraph
{
    public interface IUploadDocument
    {
        Task<UploadDocumentResponse> Handle(UploadDocumentRequest request, CancellationToken cancellationToken);
    }
}
