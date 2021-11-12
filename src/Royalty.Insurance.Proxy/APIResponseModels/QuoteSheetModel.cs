using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Royalty.Insurance.Proxy.APIModels;

namespace Royalty.Insurance.Proxy.APIResponseModels
{
    public class QuoteSheetModel
    {
        public QuoteSheetModel()
        {

        }
        public QuoteSheetModel(int dotNumber)
        {
            DotNumber = dotNumber;
        }
        public InsuredInformation InsuredInformation { get; set; }
        public List<DriverInformation> DriverInformation { get; set; }
        public List<VehicleInformation> VehicleInformations { get; set; }
        public List<LossInformation> LossInformations { get; set; }
        public List<Coverage> Coverage { get; set; }
        public Cargo Cargo { get; set; }
        public int DotNumber { get; set; }
    }

    public class Cargo
    {
        public List<Commodity> Commodities { get; set; } = new List<Commodity>();
        public string Comments { get; set; }

    }

    public class Commodity
    {
        [Required]
        public string Name { get; set; }

        public int Percent { get; set; }
        public decimal Maximum { get; set; }
        public decimal Average { get; set; }
    }

    public class Coverage
    {
        [Required]
        public CoverageTypeCode CoverageType { get; set; }
        [Required]
        public double Limit { get; set; }
    }

    public enum CoverageTypeCode
    {
        AutoLiability = 1,
        UnInsuredMotorist = 2,
        CargoLimit = 3,
        ReeferBreakdown = 4,
        PdDeductibles = 5,
        TrailerInterchange = 6,
        GeneralLiability = 7,
        HiredAuto = 8,
        NonTrackingLiability = 9,
        Other = 10
    }

    public class VehicleInformation
    {
        [Required]
        public string Make { get; set; }
        [Required]
        public string VehicleType { get; set; }
        [Required]
        public int Year { get; set; }
        public string GVW { get; set; }
        [Required]
        public decimal Value { get; set; }
        public int Radius { get; set; }
        [Required]
        public string VIN { get; set; }
    }

    public enum VehicleType
    {
        PowerUnit = 0,
        Bus = 1,
        DollyConverter = 2,
        FullTrailer = 3,
        Limousine = 4,
        MotorCoach = 5,
        Other = 6,
        PoleTrailer = 7,
        SchoolBus = 8,
        SemiTrailer = 9,
        StraightTruck = 10,
        TruckTractor = 11,
        Van = 12,
        Unknown = 13,
        IntermodalChassis = 14,
        CribLogTrailer = 15
    }

    public class DriverInformation
    {
        [Required]
        public string DriverName { get; set; }
        public string DOB { get; set; }
        [Required]
        public string LicenseNumber { get; set; }
        public string State { get; set; }
        [Required]
        public DateTime DateHired { get; set; }
        [Required]
        public int YearsOfExperience { get; set; }
        public int MoveViolationsNumber { get; set; }
        public int AccidentNumber { get; set; }
    }

    public class InsuredInformation
    {
        [Required]
        public string InsuredName { get; set; }
        public string Name { get; set; }
        public string DBA { get; set; }
        [Required]
        public List<QuoteAddress> Addresses { get; set; }
        [Required]
        public List<QuotePhone> Phones { get; set; }
        [Required]
        public List<Email> Emails { get; set; }
        public int FarthestStateTraveledTo { get; set; }
        [Required]
        public int YearsInsured { get; set; }
        [Required]
        public bool Filings { get; set; }
        public int MC { get; set; }
        public int? DOT { get; set; }
        [Required]
        public string StateName { get; set; }
        [Required]
        public string StateNumber { get; set; }
        [Required]
        public string LegalStatus { get; set; }
        public string TaxOrSSN { get; set; }
    }

    public class Email
    {
        public string PEmail { get; set; }
        public EmailType EmailType { get; set; }
    }

    public enum EmailType
    {
        MailingEmail = 1,
        GaragingEmail = 2
    }

    public class QuotePhone
    {
        public string PhoneNumber { get; set; }
        public PhoneType PhoneType { get; set; }
    }

    public enum PhoneType
    {
        MailingPhone = 1,
        Fax = 2
    }

    public class QuoteAddress
    {
        public string PAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public AddressType AddressType { get; set; }

    }

    public enum AddressType
    {
        MailingAddress = 1,
        GaragingAddress = 2
    }
    public class LossInformation
    {
        [Required]
        public string InsuranceName { get; set; }
        [Required]
        public string PoliceNumber { get; set; }
        [Required]
        public int NumberOfClaims { get; set; }
        public string Notes { get; set; }
        [Required]
        public string EffectiveDate { get; set; }
        [Required]
        public string ExpireDate { get; set; }
        public string LesseeMCNumber { get; set; }
        public string LesseeName { get; set; }
    }
}
