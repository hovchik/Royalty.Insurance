using System;
using System.Linq.Expressions;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.LossInfo
{
    public class LossInfoMapperService : ILossInfoMapperService
    {
        public void UpdateEntity(Domain.LossInformation entity, UpdateLossInformationCommand request)
        {
            entity.Comments = request.Comments;
            entity.EffectiveDate = request.EffectiveDate;
            entity.ExpireDate = request.ExpireDate;
            entity.InsuranceName = request.InsuranceName;
            entity.LesseeMcnumber = request.LesseeMCNumber;
            entity.LesseeName = request.LesseeName;
            entity.NumberOfClaims = request.NumberOfClaims;
            entity.PoliceNumber = request.PoliceNumber;
        }

        public Expression<Func<Domain.LossInformation, LossInfoResponse>> MapResponse
        {
            get
            {
                return entity => new LossInfoResponse
                {
                    LesseeMCNumber = entity.LesseeMcnumber,
                    LesseeName = entity.LesseeName,
                    InsuranceName = entity.InsuranceName,
                    PoliceNumber = entity.PoliceNumber,
                    NumberOfClaims = entity.NumberOfClaims,
                    Comments = entity.Comments,
                    ExpireDate = entity.ExpireDate,
                    EffectiveDate = entity.EffectiveDate,
                    Id = entity.Id
                };
            }
        }
    }
}