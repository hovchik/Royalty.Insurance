using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Core.System.MicrosoftGraph
{
    public interface IDownloadDocument
    {
        Task<Stream> Handle(DownloadDocumentRequest request, CancellationToken cancellationToken);
    }
}
