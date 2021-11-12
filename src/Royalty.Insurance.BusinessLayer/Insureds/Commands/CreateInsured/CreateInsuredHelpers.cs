using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.APIResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Cargo = Domain.Cargo;
using Commodity = Domain.Commodity;
using Coverage = Domain.Coverage;
using DriverInformation = Domain.DriverInformation;
using LossInformation = Domain.LossInformation;

namespace Royalty.Insurance.BusinessLayer.Insureds
{
    public class CreateInsuredHelpers
    {
        #region Populate DB via CAB response
        internal static (Cargo cargo, List<CargoCommodity> cargoCommodities, List<Commodity> commodities) GenerateCargoCommodities(Insured entity, QuoteSheetRequest request, int userId)
        {
            List<CargoCommodity> cargoCommodities = new List<CargoCommodity>();
            List<Commodity> commodities = new List<Commodity>();

            Cargo newCargo = new Cargo
            {
                CreateBy = userId,
                UpdatedBy = userId,
                LastModifiedUtc = DateTime.UtcNow,
                CreateDatetimeUtc = DateTime.UtcNow,
                Insured = entity,
            };

            foreach (var comm in request.Cargo.Commodities.Select(commodity => new Commodity
            {
                CreateDatetimeUtc = DateTime.UtcNow,
                Name = commodity.Name,
                UpdatedBy = userId,
                CommodityValue = Convert.ToInt32(commodity.Maximum),
                LastModifiedUtc = DateTime.UtcNow,
                CreateBy = userId,
                CommodityPercent = commodity.Percent

            }))
            {
                commodities.Add(comm);
                cargoCommodities.Add(new CargoCommodity
                {
                    Cargo = newCargo,
                    Commodity = comm
                });
            }

            return (newCargo, cargoCommodities, commodities);
        }

        internal static async Task<List<InsuredCoverage>> GenerateCoverageInfo(Insured entity, QuoteSheetRequest request, int userId, IApplicationDbContext context)
        {
            List<Coverage> coverages = await context.Coverages.ToListAsync();
            List<InsuredCoverage> insuredCoveragesResult = request.Coverage.Select(cover => new InsuredCoverage
            {
                Coverage = coverages.FirstOrDefault(c => c.Id == (int)cover.CoverageType),
                Limit = coverages.FirstOrDefault(c => c.Id == (int)cover.CoverageType) == null ? default : coverages.FirstOrDefault(c => c.Id == (int)cover.CoverageType).CoverageLimit,
                Insured = entity,
                CreatedBy = userId,
                CreateDatetimeUtc = DateTime.UtcNow,
                LastModifiedUtc = DateTime.UtcNow,
                UpdatedBy = userId
            }).ToList();

            var autoLiabilityLimit = coverages.First(c => c.Id == (int)CoverageTypeCode.AutoLiability);
            var autoLiability = insuredCoveragesResult.First(aLi => aLi.Coverage.Equals(autoLiabilityLimit));

            autoLiability.Coverage = autoLiabilityLimit;
            autoLiability.Limit = Convert.ToInt32(autoLiabilityLimit.CoverageLimit);
            autoLiability.Insured = entity;
            autoLiability.CreatedBy = userId;
            autoLiability.CreateDatetimeUtc = DateTime.UtcNow;
            autoLiability.LastModifiedUtc = DateTime.UtcNow;
            autoLiability.UpdatedBy = userId;

            return insuredCoveragesResult;
        }

        //todo should clarifying 
        internal static (List<VehicleInfo> vehicles, List<InsuredVehicle> insVehilces) GenerateVehicleInfo(Insured entity, QuoteSheetRequest request)
        {
            List<VehicleInfo> vehicles = new List<VehicleInfo>();
            List<InsuredVehicle> insuredVehicles = new List<InsuredVehicle>();

            foreach (var vInfo in request.VehicleInformations.Select(vehicle => new VehicleInfo
            {
                Year = vehicle.Year,
                Make = vehicle.Make,
                Type = vehicle.VehicleType,
                Gvw = Convert.ToInt32(vehicle.GVW),
                ActualValue = Convert.ToInt32(vehicle.Value),
                Radius = vehicle.Radius.ToString(),
                Vin = vehicle.VIN,
                IsTruck = !vehicle.VehicleType.ToString().Contains("Trailer")
            }))
            {
                vehicles.Add(vInfo);
                insuredVehicles.Add(new InsuredVehicle
                {
                    Insured = entity,
                    Vehicle = vInfo
                });
            }

            return (vehicles, insuredVehicles);
        }

        internal static async Task<List<DriverInformation>> GenerateDriverInfo(Insured entity, QuoteSheetRequest request, int userId, IApplicationDbContext context)
        {
            var allStates = await context.States.ToListAsync();

            return (from driver in request.DriverInformation
                    let driverState = allStates.First(state => state.Name.Equals(driver.State))
                    select new DriverInformation
                    {
                        CreateDatetimeUtc = DateTime.UtcNow,
                        Insured = entity,
                        Accidents = driver.AccidentNumber.ToString(),
                        CreatedBy = userId,
                        DateHired = driver.DateHired,
                        DateOfBirth = DateTime.Parse(driver.DOB),
                        LastModifiedUtc = DateTime.UtcNow,
                        DriverName = driver.DriverName,
                        LicenseNumber = driver.LicenseNumber,
                        YearOfExperiance = driver.YearsOfExperience,
                        UpdatedBy = userId,
                        StateId = driverState.Id
                    }).ToList();
        }

        internal static List<LossInformation> GenerateLossInformation(Insured entity, QuoteSheetRequest request)
        {
            return request.LossInformations.Select(req => new LossInformation
            {
                Insured = entity,
                Comments = req.Notes,
                EffectiveDate = DateTime.Parse(req.EffectiveDate),
                ExpireDate = DateTime.Parse(req.EffectiveDate),
                InsuranceName = req.InsuranceName,
                NumberOfClaims = req.NumberOfClaims.ToString(),
                PoliceNumber = req.PoliceNumber,
                LesseeMcnumber = req.LesseeMCNumber, //should clarify
                LesseeName = req.LesseeName
            }).ToList();
        }
        #endregion

        #region predicate checks

        internal static bool StatesAreEquals(QuoteSheetRequest request)
        {
            return IsAddressExists(request, out var mailingAddress, out var garagingAddress) && mailingAddress.State.Equals(garagingAddress.State);
        }

        internal static bool ZipsAreEquals(QuoteSheetRequest request)
        {
            return IsAddressExists(request, out var mailingAddress, out var garagingAddress) && mailingAddress.Zip.Equals(garagingAddress.Zip);
        }

        internal static bool CitiesAreEquals(QuoteSheetRequest request)
        {
            return IsAddressExists(request, out var mailingAddress, out var garagingAddress) && mailingAddress.City.Equals(garagingAddress.City);
        }

        internal static bool IsAddressExists(QuoteSheetRequest request, out QuoteAddress mailingAddress, out QuoteAddress garagingAddress)
        {
            mailingAddress =
                request.InsuredInformation.Addresses.FirstOrDefault(m => m.AddressType == AddressType.MailingAddress);
            garagingAddress =
                request.InsuredInformation.Addresses.FirstOrDefault(m => m.AddressType == AddressType.GaragingAddress);

            return mailingAddress != null && garagingAddress != null;
        }

        #endregion
    }
}
