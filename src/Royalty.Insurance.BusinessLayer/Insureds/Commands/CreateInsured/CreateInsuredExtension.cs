using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.APIResponseModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Royalty.Insurance.BusinessLayer.Insureds
{
    public static class CreateInsured
    {
        public static async Task<Insured> CreateZipIfNotExists(this Insured insured, AddressType addressType, IApplicationDbContext context, QuoteSheetRequest request, Predicate<QuoteSheetRequest> predicate = null)
        {
            ZipCode zipCode = new ZipCode
            {
                City = addressType == AddressType.MailingAddress ? insured.MailingCity : insured.GaragingCity,
                Code = request.InsuredInformation.Addresses.FirstOrDefault(addr => addr.AddressType == addressType)?.Zip
            };
            var isZipExists = await context.ZipCodes.Where(zip =>
                    zip.Code.Equals(zipCode.Code))
                .FirstOrDefaultAsync();
            if (isZipExists == null)
            {
                if (predicate != null && predicate.Invoke(request))
                {
                    insured.GaragingZipCode = insured.MailingZipCode;
                    return insured;
                }
                if (addressType == AddressType.MailingAddress)
                {
                    insured.MailingZipCode = zipCode;
                }
                else
                {
                    insured.GaragingZipCode = zipCode;
                }
            }
            else
            {
                if (addressType == AddressType.MailingAddress)
                {
                    insured.MailingZipCodeId = isZipExists.Id;
                }
                else
                {
                    insured.GaragingZipCode = isZipExists;
                    insured.GaragingZipCodeId = isZipExists.Id;
                }
            }

            return insured;
        }

        public static async Task<Insured> CreateStateIfNotExists(this Insured insured, AddressType addressType, IApplicationDbContext context, QuoteSheetRequest request, Predicate<QuoteSheetRequest> predicate = null)
        {
            var requestState = request.InsuredInformation.Addresses.FirstOrDefault(addr => addr.AddressType == addressType).State;
            var isStateExists = await context.States.Where(state =>
                    state.Name.Equals(requestState))
                .FirstOrDefaultAsync();

            if (addressType == AddressType.MailingAddress)
            {
                insured.MailingStateId = isStateExists.Id;
            }
            else
            {
                insured.GaragingStateId = isStateExists.Id;
            }

            return insured;
        }

        public static async Task<Insured> CreateCityIfNotExists(this Insured insured, AddressType addressType, IApplicationDbContext context, QuoteSheetRequest request, Predicate<QuoteSheetRequest> predicate = null)
        {
            City newcity = new City
            {
                StateId = addressType == AddressType.MailingAddress ? insured.MailingStateId : insured.GaragingStateId,
                Name = request.InsuredInformation.Addresses.FirstOrDefault(addr => addr.AddressType == addressType)?.City
            };

            var isCityExists = await context.Cities.Where(city =>
                    city.Name.Equals(newcity.Name))
                .FirstOrDefaultAsync();

            if (isCityExists == null)
            {
                if (predicate != null && predicate.Invoke(request))
                {
                    insured.GaragingCity = insured.MailingCity;
                    return insured;
                }

                if (addressType == AddressType.MailingAddress)
                {
                    insured.MailingCity = newcity;
                }
                else
                {
                    insured.GaragingCity = newcity;
                }
            }
            else
            {
                if (addressType == AddressType.MailingAddress)
                {
                    insured.MailingCityId = isCityExists.Id;
                }
                else
                {
                    insured.GaragingCity = isCityExists;
                    insured.GaragingCityId = isCityExists.Id;
                }
            }

            return insured;
        }

        public static async Task<Insured> AddLegalStatus(this Insured insured, QuoteSheetRequest request, IApplicationDbContext context)
        {
            LegalStatus legalStatus = new LegalStatus
            {
                Name = request.InsuredInformation.LegalStatus,
            };

            var isLegalExists = await context.LegalStatuses.Where(l => l.Name.Equals(legalStatus.Name)).FirstOrDefaultAsync();
            if (isLegalExists == null)
            {
                insured.LegalStatus = legalStatus;
            }
            else
            {
                insured.LegalStatusId = isLegalExists.Id;
            }

            return insured;
        }
    }
}
