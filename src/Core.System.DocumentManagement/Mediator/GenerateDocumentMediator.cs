using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.System.DocumentManagement.GenerateResolver;
using Core.System.DocumentManagement.Manager;

namespace Core.System.DocumentManagement.Mediator
{
    public class GenerateDocumentMediator : IGenerateDocumentMediator
    {
        private readonly IDocumentManager _manager;

        public GenerateDocumentMediator(IDocumentManager manager)
        {
            _manager = manager;
        }

        public async Task<Stream> SendAsync(IGenerateResolver resolver, CancellationToken cancellationToken)
        {
            return await _manager.GenerateDocumentFromTemplateAsync(resolver.TemplateStream, resolver.GetProperties, cancellationToken);
        }
    }
}
