using System.Collections.Generic;
using System.IO;

namespace Core.System.DocumentManagement.GenerateResolver
{
    public interface IGenerateResolver
    {
        Dictionary<string, string> GetProperties();

        Stream TemplateStream { get; }
    }
}
