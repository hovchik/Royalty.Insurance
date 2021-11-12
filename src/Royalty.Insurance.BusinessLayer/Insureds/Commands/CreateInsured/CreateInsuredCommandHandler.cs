using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Domain;
using Application.Interfaces;
using Royalty.Insurance.Proxy.APIResponseModels;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Insureds
{
    public class CreateInsuredCommandHandler : IRequestHandler<CreateInsuredCommand, InsuredResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IInsuredMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public CreateInsuredCommandHandler(ICurrentUserService currentUser, IInsuredMapperService mapper, IApplicationDbContext context)
        {
            _currentUser = currentUser;
            _mapper = mapper;
            _context = context;
        }

        public async Task<InsuredResponse> Handle(CreateInsuredCommand request, CancellationToken cancellationToken)
        {
            User user = await _context.Users
                .Where(item => item.Id.Equals(_currentUser.UserId))
               .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new RestApiResponseException(ResourceCommonMessage.EmailNotFound);
            }

            Insured entity = new Insured
            {
                CreateBy = user.Id,
                UpdatedBy = user.Id,
                IsFilings = request.Request.InsuredInformation.Filings,
                MotorCarrierNumber = request.Request.InsuredInformation.MC.ToString(),
                SocialSecurityNumber = request.Request.InsuredInformation.TaxOrSSN,
                StateNumber = request.Request.InsuredInformation.StateNumber,
                YearsInsured = request.Request.InsuredInformation.YearsInsured,
                FartherState = byte.Parse(request.Request.InsuredInformation.FarthestStateTraveledTo.ToString()),
                GaragingEmail = request.Request.InsuredInformation.Emails.FirstOrDefault(email => email.EmailType == EmailType.GaragingEmail)?.PEmail,
                MailingEmail = request.Request.InsuredInformation.Emails.FirstOrDefault(email => email.EmailType == EmailType.MailingEmail)?.PEmail,
                GaragingPhone = request.Request.InsuredInformation.Phones.FirstOrDefault(ph => ph.PhoneType == PhoneType.Fax)?.PhoneNumber,
                MailingPhone = request.Request.InsuredInformation.Phones.FirstOrDefault(ph => ph.PhoneType == PhoneType.MailingPhone)?.PhoneNumber,
                Dba = request.Request.InsuredInformation.DBA,
                DotNumber = request.Request.InsuredInformation.DOT,
                GaragingName = request.Request.InsuredInformation.Name,
                MailingName = request.Request.InsuredInformation.InsuredName,
                GaragingStreetAddress = request.Request.InsuredInformation.Addresses.FirstOrDefault(street => street.AddressType == AddressType.GaragingAddress)?.PAddress,
                MailingStreetAddress = request.Request.InsuredInformation.Addresses.FirstOrDefault(street => street.AddressType == AddressType.MailingAddress)?.PAddress,
            };

            await entity.CreateStateIfNotExists(AddressType.MailingAddress, _context, request.Request).Result
                    .CreateCityIfNotExists(AddressType.MailingAddress, _context, request.Request).Result
                    .CreateZipIfNotExists(AddressType.MailingAddress, _context, request.Request);

            await entity.CreateStateIfNotExists(AddressType.GaragingAddress, _context, request.Request, CreateInsuredHelpers.StatesAreEquals).Result
                .CreateCityIfNotExists(AddressType.GaragingAddress, _context, request.Request, CreateInsuredHelpers.CitiesAreEquals).Result
                .CreateZipIfNotExists(AddressType.GaragingAddress, _context, request.Request, CreateInsuredHelpers.ZipsAreEquals);

            await entity.AddLegalStatus(request.Request, _context);

            List<Domain.LossInformation> lossInformation = CreateInsuredHelpers.GenerateLossInformation(entity, request.Request);
            List<Domain.DriverInformation> driverInformations = await CreateInsuredHelpers.GenerateDriverInfo(entity, request.Request, _currentUser.UserId, _context);
            var vehicleInfos = CreateInsuredHelpers.GenerateVehicleInfo(entity, request.Request);
            var coverageInfo = await CreateInsuredHelpers.GenerateCoverageInfo(entity, request.Request, _currentUser.UserId, _context);
            var cargoCommodity = CreateInsuredHelpers.GenerateCargoCommodities(entity, request.Request, _currentUser.UserId);

            await _context.Insureds.AddAsync(entity, cancellationToken);

            await _context.LossInformations.AddRangeAsync(lossInformation, cancellationToken);
            await _context.DriverInformations.AddRangeAsync(driverInformations, cancellationToken);
            await _context.VehicleInfos.AddRangeAsync(vehicleInfos.vehicles, cancellationToken);
            await _context.InsuredVehicles.AddRangeAsync(vehicleInfos.insVehilces, cancellationToken);
            await _context.InsuredCoverages.AddRangeAsync(coverageInfo, cancellationToken);
            await _context.Cargos.AddAsync(cargoCommodity.cargo, cancellationToken);
            await _context.Commodities.AddRangeAsync(cargoCommodity.commodities, cancellationToken);
            await _context.CargoCommodities.AddRangeAsync(cargoCommodity.cargoCommodities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
