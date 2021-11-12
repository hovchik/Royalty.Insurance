using System.Collections.Generic;
using System.IO;

namespace Core.System.DocumentManagement.GenerateResolver
{
    public class CreditCArdAuthorizationResolver : IGenerateResolver
    {
        private const string InsuredName = "royalty.Insured_Name";

        public CreditCArdAuthorizationResolver(InsuredFullNameRequest request, Stream templateStream)
        {
            Request = request;
            TemplateStream = templateStream;
        }

        public InsuredFullNameRequest Request { get; }

        public Stream TemplateStream { get; }

        public Dictionary<string, string> GetProperties()
        { 
            return new Dictionary<string, string>{ { InsuredName, Request.Name }};
        }
    }
}
