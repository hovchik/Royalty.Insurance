using System.Collections.Generic;
using System.IO;

namespace Core.System.DocumentManagement.GenerateResolver
{
    public class Accord101Resolver : IGenerateResolver
    {
        public Accord101Resolver(Accord101FormRequest request, Stream templateStream)
        {
            Request = request;
            TemplateStream = templateStream;
        }

        public Dictionary<string, string> GetProperties()
        {
            return new Dictionary<string, string>
            {
                { TemplateFieldConstants.AgencyName, Request.AgencyName },
                { TemplateFieldConstants.InsuranceNameCarrier, Request.InsuranceNameCarrier },
                { TemplateFieldConstants.InsuredAdress, Request.InsuredAddress},
                { TemplateFieldConstants.InsuredCompanyName, Request.InsuredCompanyName},
                { TemplateFieldConstants.InsuredCity, Request.InsuredCity},
                { TemplateFieldConstants.InsuredState, Request.InsuredState},
                { TemplateFieldConstants.InsuredZip, Request.InsuredZip },
                { TemplateFieldConstants.ProducerFullName, Request.ProducerFullName },
            };
        }

        public Stream TemplateStream { get; }

        public Accord101FormRequest Request { get; }
    }
}
