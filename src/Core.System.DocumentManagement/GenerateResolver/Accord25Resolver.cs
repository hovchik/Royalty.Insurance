using System.Collections.Generic;
using System.IO;


namespace Core.System.DocumentManagement.GenerateResolver
{
    public class Accord25Resolver : IGenerateResolver
    {
        public Accord25FormRequest Request { get; }

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
                { TemplateFieldConstants.AgencyZip, Request.AgencyZip}
            };
        }

        public Stream TemplateStream { get; }

        public Accord25Resolver(Accord25FormRequest request, Stream templateStream)
        {
            Request = request;
            TemplateStream = templateStream;
        }
    }
}
