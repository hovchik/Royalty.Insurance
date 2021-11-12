using Royalty.Insurance.Proxy.APIModels;
using Royalty.Insurance.Proxy.APIModels.Core;
using Royalty.Insurance.Proxy.APIResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Address = Royalty.Insurance.Proxy.APIResponseModels.QuoteAddress;
using Coverage = Royalty.Insurance.Proxy.APIResponseModels.Coverage;
using Email = Royalty.Insurance.Proxy.APIResponseModels.Email;
using InsHist = Royalty.Insurance.Proxy.APIModels.Core.InsHist;
using Inspection = Royalty.Insurance.Proxy.APIModels.Core.Inspection;
using Phone = Royalty.Insurance.Proxy.APIResponseModels.QuotePhone;

namespace Royalty.Insurance.BusinessLayer.Cab
{
    public class DotCoreMapperService : IDotCoreMapperService
    {

        public Expression<Func<DotCoreResponse, QuoteSheetModel>> MapResponse => entity => new QuoteSheetModel
        {

        };

        public void UpdateEntity(QuoteSheetModel entity, DotCoreResponse coreRequest, DOTResponse request,List<CabVinResponse> vinCodeAndVehicleInfo)
        {
            entity.InsuredInformation = GenerateInsuredInfo(coreRequest);
            entity.DriverInformation = GenerateDriverInfo(coreRequest.inspections);
            entity.LossInformations = GenerateLossInfo(coreRequest.licensingAndInsurance);
            entity.Coverage = GenerateCoverage(coreRequest);
            entity.VehicleInformations = GenerateVehicleInfo(coreRequest.inspections,vinCodeAndVehicleInfo);
            entity.Cargo = GenerateCargo(coreRequest, request);
        }

        private Cargo GenerateCargo(DotCoreResponse request, DOTResponse cargo)
        {
            Cargo result = new Cargo();

            List<string> cargos = cargo.companyInfo.cargo.Split(',').ToList();
            result.Commodities.AddRange(cargos.Select(cg => new Commodity
            {
                Name = cg,
                Average = default,
                Maximum = default,
                Percent = default

            }));


            return result;
        }

        private List<VehicleInformation> GenerateVehicleInfo(List<Inspection> request, List<CabVinResponse> vinCodeAndVehicleInfo)
        {
            List<VehicleInformation> vehicleInformation = new List<VehicleInformation>();
            List<Unit> listUnits = request.Where(x => x.units != null).SelectMany(x => x.units).ToList();

            vehicleInformation.AddRange(listUnits.Select(unit => new VehicleInformation
            {
                GVW = unit.utGVWR.ToString(),
                Make = vinCodeAndVehicleInfo.FirstOrDefault(x=> unit.vin !=null && x.Vin.Equals(unit.vin))?.Make,
                VIN = unit.vin,
                Year = unit.utYear,
                VehicleType = vinCodeAndVehicleInfo.FirstOrDefault(x => unit.vin != null && x.Vin.Equals(unit.vin))?.Type,
                Value = 0,
                Radius = GetStatesCount(request)
            }).Where(vehicleInfo => !vehicleInformation.Contains(vehicleInfo, new VehicleComparer())));


            return vehicleInformation;
        }

        private int GetStatesCount(List<Inspection> request)
        {
            var listOfDistinctState = new List<string>();
            listOfDistinctState.AddRange(request.Where(inspection => !listOfDistinctState.Contains(inspection.cntyCdSt)).Select(state => state.cntyCdSt));


            return listOfDistinctState.Count;
        }

        private List<Coverage> GenerateCoverage(DotCoreResponse request)
        {
            List<Coverage> result = new List<Coverage>();
            result.AddRange(from CoverageTypeCode cType in Enum.GetValues(typeof(CoverageTypeCode)) select new Coverage { CoverageType = cType, Limit = default });
            var autoLiability = result.First(x => x.CoverageType == CoverageTypeCode.AutoLiability);
            autoLiability.Limit = request.licensingAndInsurance?.FirstOrDefault() != null
                ? request.licensingAndInsurance?.FirstOrDefault()?.actPendIns?.FirstOrDefault().bipdMax * 1000 ??
                  default
                : default;


            return result;
        }

        private List<LossInformation> GenerateLossInfo(List<LicensingAndInsurance> request)
        {
            List<LossInformation> lossInformation = new List<LossInformation>();
            List<InsHist> insuranceHistory = new List<InsHist>();
            foreach (var licensingAndInsurance in request)
            {
                licensingAndInsurance.insHist.ToList().ForEach(insHist => insuranceHistory.Add(insHist));
            }
            insuranceHistory.Sort(new InsuranceComparer());

            lossInformation.AddRange(insuranceHistory.Select(insHist => new LossInformation
            {
                PoliceNumber = insHist.pol,
                EffectiveDate = insHist.effDt,
                ExpireDate = insHist.effDtTo,
                InsuranceName = insHist.insCompNm,
                Notes = string.Empty,
                NumberOfClaims = 0
            }).Where(lossInfo => !lossInformation.Contains(lossInfo)));

            return lossInformation;
        }

        private List<DriverInformation> GenerateDriverInfo(List<Inspection> request)
        {
            var driverInfo = new List<DriverInformation>();
            var driversList = request.Where(x => x.drivers != null).SelectMany(y => y.drivers);
            driverInfo.AddRange(driversList.Select(driver => new DriverInformation
            {
                DOB = driver.dob,
                DriverName = $"{driver.fNm} {driver.lNm}",
                State = driver.licSt,
                DateHired = default,
                AccidentNumber = 0,
                LicenseNumber = string.Empty,
                MoveViolationsNumber = 0,
                YearsOfExperience = 0
            }).Where(qDriver => !driverInfo.Contains(qDriver, new DriverComparer())));


            return driverInfo;
        }

        private InsuredInformation GenerateInsuredInfo(DotCoreResponse request)
        {
            return new InsuredInformation
            {
                Addresses = new List<Address> { new Address { PAddress = request.census.phStr, AddressType = AddressType.GaragingAddress, City = request.census.phCty, State = request.census.phSt, Zip = request.census.phZip },
                                                new Address { PAddress=request.census.mStr,AddressType=AddressType.MailingAddress,City=request.census.mCty,State=request.census.mSt,Zip=request.census.mZip } },
                DBA = request.census.dbaNm,
                DOT = request.census.dot,
                TaxOrSSN = string.Empty,
                InsuredName = request.census.legNm,
                Name = GetNameFromFullName(request.census.legNm.Trim()),
                MC = request.census.doc1,
                Emails = new List<Email> { new Email { EmailType = EmailType.MailingEmail, PEmail = request.census.emailAddr } }, //multiple emails ?
                Filings = !string.IsNullOrEmpty(request.census.doc1.ToString()),
                LegalStatus = string.Empty,
                Phones = new List<Phone> { new Phone { PhoneType = PhoneType.MailingPhone, PhoneNumber = request.census.telNo } },
                StateName = GetStateName(request.inspections),
                StateNumber = GetStateNumber(request.inspections),
                FarthestStateTraveledTo = GetStatesCount(request.inspections),
                YearsInsured = GetInsuredYears(request.licensingAndInsurance)
            };
        }

        private string GetNameFromFullName(string censusLegNm)
        {
            var names = censusLegNm.Split(' ');
            if (names.Length >= 2)
            {
                return names[0];
            }

            return censusLegNm;
        }

        private int GetInsuredYears(List<LicensingAndInsurance> licensingAndInsurance)
        {
            var sortedDates = licensingAndInsurance.FirstOrDefault()?.insHist;
            sortedDates?.Sort(new InsuranceHistoryComparer());
            List<InsHist> realInsurance = new List<InsHist>();
            for (var i = 1; i < sortedDates.Count; i++)
            {
                if (DateTime.Parse(sortedDates[i].effDt).Subtract(DateTime.Parse(sortedDates[i - 1].effDtTo)).Days <= 10) //should be customizable
                {
                    realInsurance.Add(sortedDates[i]);
                }
                else
                {
                    break;
                }
            }

            return DateTime.Parse(realInsurance[0].effDtTo)
                            .Subtract(DateTime.Parse(realInsurance[^1].effDt)).Days / 365;
        }

        private string GetStateNumber(List<Inspection> inspections)
        {
            List<CarrierDetail> details = new List<CarrierDetail>();
            details.AddRange(inspections.SelectMany(num => num.carrierDetails));
            if (details.Any(x => x.stID != null))
            {
                return details.First(x => x.stID != null).stID;
            }
            return string.Empty;
        }

        private string GetStateName(List<Inspection> inspections)
        {
            var carrierDetails = inspections.Select(num => num.carrierDetails).ToList().FirstOrDefault();
            if (carrierDetails != null)
            {
                return carrierDetails.FirstOrDefault()?.st;
            }
            return string.Empty;
        }
    }

    internal class InsuranceComparer : IComparer<InsHist>
    {
        public int Compare(InsHist x, InsHist y)
        {
            if (DateTime.Parse(x?.effDtTo ?? string.Empty) < DateTime.Parse(y?.effDtTo ?? string.Empty))
            {
                return 1;
            }
            else if (DateTime.Parse(x.effDtTo) > DateTime.Parse(y.effDtTo))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    internal class InsuranceHistoryComparer : IComparer<InsHist>
    {
        public int Compare(InsHist x, InsHist y)
        {
            if (DateTime.Parse(x?.effDt ?? string.Empty) < DateTime.Parse(y?.effDt ?? string.Empty))
            {
                return 1;
            }
            else if (DateTime.Parse(x.effDt) > DateTime.Parse(y.effDt))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    internal class VehicleComparer : IEqualityComparer<VehicleInformation>
    {
        public bool Equals(VehicleInformation x, VehicleInformation y)
        {
            return x.VIN == y.VIN;
        }

        public int GetHashCode(VehicleInformation obj)
        {
            return obj.VIN.GetHashCode();
        }
    }

    internal class DriverComparer : IEqualityComparer<DriverInformation>
    {
        public bool Equals(DriverInformation x, DriverInformation y)
        {
            return x.DOB == y.DOB && x.DriverName == y.DriverName;
        }

        public int GetHashCode(DriverInformation obj)
        {
            return obj.DOB.GetHashCode() ^ obj.DriverName.GetHashCode();
        }
    }
}
