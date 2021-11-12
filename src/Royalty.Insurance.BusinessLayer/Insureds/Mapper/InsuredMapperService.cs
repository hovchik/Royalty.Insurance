using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Insureds
{
    public class InsuredMapperService : IInsuredMapperService
    {
        public void UpdateEntity(Insured entity, InsuredRequest request)
        {
            entity.FartherState = request.FartherState;

            #region Garage Mapping

            entity.GaragingCityId = request.GaragingCityId;
            entity.GaragingStateId = request.GaragingStateId;
            entity.GaragingZipCodeId = request.GaragingZipCodeId;
            entity.GaragingStreetAddress = request.GaragingStreetAddress;
            entity.GaragingPhone = request.GaragingPhone;
            entity.GaragingName = request.GaragingName;
            entity.GaragingEmail = request.GaragingEmail;

            #endregion

            #region Mailing Mapping

            entity.MailingCityId = request.MailingCityId;
            entity.MailingStateId = request.MailingStateId;
            entity.MailingZipCodeId = request.MailingZipCodeId;
            entity.MailingStreetAddress = request.MailingStreetAddress;
            entity.MailingPhone = request.MailingPhone;
            entity.MailingName = request.MailingName;
            entity.MailingEmail = request.MailingEmail;

            #endregion

            entity.IsFilings = request.IsFilings;
            entity.LegalStatusId = request.LegalStatusId;
            entity.SocialSecurityNumber = request.SocialSecurityNumber;
            entity.StateNumber = request.StateNumber;
            entity.MotorCarrierNumber = request.MotorCarrierNumber;
            entity.YearsInsured = request.YearsInsured;

        }

        public Expression<Func<Insured, InsuredResponse>> MapResponse
        {
            get
            {
                return entity => new InsuredResponse
                {
                    Id=entity.Id,
                    //Garaging mapping
                    GaragingCityId = entity.GaragingCityId,
                    GaragingStateId = entity.GaragingStateId,
                    GaragingZipCodeId = entity.GaragingZipCodeId,
                    GaragingStreetAddress = entity.GaragingStreetAddress,
                    GaragingPhone = entity.GaragingPhone,
                    GaragingName = entity.GaragingName,
                    GaragingEmail = entity.GaragingEmail,

                    //Mailing Mapping
                    MailingCityId = entity.MailingCityId,
                    MailingStateId = entity.MailingStateId,
                    MailingZipCodeId = entity.MailingZipCodeId,
                    MailingStreetAddress = entity.MailingStreetAddress,
                    MailingPhone = entity.MailingPhone,
                    MailingName = entity.MailingName,
                    MailingEmail = entity.MailingEmail,

                    IsFilings = entity.IsFilings,
                    LegalStatusId = entity.LegalStatusId.Value,
                    SocialSecurityNumber = entity.SocialSecurityNumber,
                    StateNumber = entity.StateNumber,
                    MotorCarrierNumber = entity.MotorCarrierNumber,
                    YearsInsured = entity.YearsInsured
                };
            }
        }
    }
}
