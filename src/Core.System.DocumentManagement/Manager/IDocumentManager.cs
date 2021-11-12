using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Core.System.DocumentManagement.Manager
{
    public interface IDocumentManager
    {
        Task<Stream> GenerateDocumentFromTemplateAsync(Stream template, Func<Dictionary<string, string>> getProperties, CancellationToken cancellationToken);
    }
}
