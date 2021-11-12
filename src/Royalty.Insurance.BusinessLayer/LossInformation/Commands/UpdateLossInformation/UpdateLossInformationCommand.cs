using System;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.LossInfo
{
    public class UpdateLossInformationCommand : IRequest<LossInfoResponse>
    {
        public int Id { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string InsuranceName { get; set; }
        public string LesseeName { get; set; }
        public string PoliceNumber { get; set; }
        public string LesseeMCNumber { get; set; }
        public string NumberOfClaims { get; set; }
        public string Comments { get; set; }
    }
}