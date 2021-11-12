using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.System.DocumentManagement.GenerateResolver;

namespace Core.System.DocumentManagement.Mediator
{
    public interface IGenerateDocumentMediator
    {
        Task<Stream> SendAsync(IGenerateResolver resolver, CancellationToken cancellationToken);
    }
}
