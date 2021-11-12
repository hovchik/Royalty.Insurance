using System.Collections.Generic;
using System.IO;

namespace Core.System.DocumentManagement.GenerateResolver
{
    public class Accord36Resolver : IGenerateResolver
    {
        public Accord36Resolver(Accord36FormRequest request, Stream templateStream)
        {
            Request = request;
            TemplateStream = templateStream;
        }

        public Dictionary<string, string> GetProperties()
        {
            return new Dictionary<string, string>
            {
                { TemplateFieldConstants.AgencyName, Request.AgencyName },
                { TemplateFieldConstants.InsuredAdress, Request.InsuredAddress},
                { TemplateFieldConstants.InsuredCompanyName, Request.InsuredCompanyName},
                { TemplateFieldConstants.InsuredCity, Request.InsuredCity},
                { TemplateFieldConstants.InsuredState, Request.InsuredState},
                { TemplateFieldConstants.InsuredZip, Request.InsuredZip },
                { TemplateFieldConstants.AgencyAdress, Request.AgencyAddress },
                { TemplateFieldConstants.AgencyState, Request.AgencyState},
                { TemplateFieldConstants.AgencyEmail, Request.AgencyEmail},
                { TemplateFieldConstants.AgencyPhoneNumber, Request.AgencyPhoneNumber},
                { TemplateFieldConstants.AgencyFaxNumber, Request.AgencyFaxNumber},
                { TemplateFieldConstants.AgencyCity, Request.AgencyCity},
                { TemplateFieldConstants.AgencyZip, Request.AgencyZip},
                {TemplateFieldConstants.ProducerFullName, Request.ProducerFullName}
            };
        }

        public Accord36FormRequest Request { get; }
        public Stream TemplateStream { get; }
    }
}
