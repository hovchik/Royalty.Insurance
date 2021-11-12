using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Royalty.Insurance.Proxy.APIModels.Marketing
{
    public class MarketingRequest
    {
        [Required]
        [DisplayName("locationType")]
        public LocationType LocationType { get; set; }
        /// <summary>
        /// The location data for example NJ for state or Lakewood, NJ for city
        /// </summary>
        [Required]
        [DisplayName("location")]
        public List<string> Location { get; set; }
        [DisplayName("radius")]
        public int Radius { get; set; }
        [DisplayName("tot_bus_min")]
        public int TotalBusMin { get; set; }
        [DisplayName("tot_bus_max")]
        public int TotalBusMax { get; set; }
        /// <summary>
        /// Minimum amount of trucks the motor carrier must have registered on the MCS150 form
        /// </summary>
        [DisplayName("tot_truck_min")]
        public int TotalTruckMin { get; set; }
        /// <summary>
        /// Maximum amount of trucks the motor carrier must have registered on the MCS150 form
        /// </summary>
        [DisplayName("tot_truck_max")]
        public int TotalTruckMax { get; set; }
        /// <summary>
        /// Minimum amount of years the company must have been in business
        /// </summary>
        [DisplayName("yib_min")]
        public int YearsInBusinessMin { get; set; }
        /// <summary>
        /// Maximum amount of years the company must have been in business
        /// </summary>
        [DisplayName("yib_max")]
        public int YearsInBusinessMax { get; set; }
        /// <summary>
        /// a comma separated list of the references for the carrier types gotten from the filter option service to include
        /// </summary>
        [DisplayName("car_type_inc")]
        public List<string> CarTypeInclude { get; set; }
        /// <summary>
        /// a comma separated list of the references for the carrier types gotten from the filter option service to exclude
        /// </summary>
        [DisplayName("car_type_exc")]
        public List<string> CarTypeExclude { get; set; }
        /// <summary>
        /// a comma separated list of the references for the cargo gotten from the filter option service to include
        /// </summary>
        [DisplayName("cargo_type_inc")]
        public List<string> CargoTypeInclude { get; set; }
        /// <summary>
        /// a comma separated list of the references for the cargo types gotten from the filter option service to exclude
        /// </summary>
        [DisplayName("cargo_type_exc")]
        public List<string> CargoTypeExclude { get; set; }
        /// <summary>
        /// a comma separated list of the references for the operations gotten from the filter option service to include
        /// </summary>
        [DisplayName("op_type_inc")]
        public List<string> OperationInclude { get; set; }
        /// <summary>
        /// a comma separated list of the references for the operations gotten from the filter option service to exclude
        /// </summary>
        [DisplayName("op_type_exc")]
        public List<string> OperationExclude { get; set; }

        /// <summary>
        /// a comma separated list of the references for the trailer body types gotten from the filter option service where the motor carrier has used this type of equipment in the past 24 months
        /// </summary>
        [DisplayName("trailer_types")]
        public List<string> TrailerTypes { get; set; }
        /// <summary>
        /// a comma separated list of the GVWR class numbers to include where the motor carrier has used this type of equipment in the past 24 months
        /// </summary>
        [DisplayName("GVWR")]
        public List<string> GVWR { get; set; }
        /// <summary>
        /// Comma separated list of the ID#s of the BASICs that have to be in alert to be included
        /// (1 - Unsafe Driving,2 - Hours of Service,3 - Driver Fitness,4 - Controlled Substances,5 - Vehicle Maintenance,6 - Hazmat Related,7 - Crash Indicator)
        /// </summary>
        [DisplayName("basic_Alert")]
        public List<string> BasicAlert { get; set; }
        /// <summary>
        /// Comma separated list of the ID#s of the BASICs that have to NOT be in alert to be included
        /// (1 - Unsafe Driving,2 - Hours of Service,3 - Driver Fitness,4 - Controlled Substances,5 - Vehicle Maintenance,6 - Hazmat Related,7 - Crash Indicator)
        /// </summary>
        [DisplayName("basic_noAlert")]
        public List<string> BasicNoAlert { get; set; }
        [DisplayName("revocation_from")]
        public string RevocationFrom { get; set; }
        [DisplayName("rad_min")]
        public string RadiusMin { get; set; }
        [DisplayName("rad_max")]
        public string RadiusMax { get; set; }
        [DisplayName("Common_auth")]
        public string CommonAuth { get; set; } //Active/Pending=a Inactive=i  None =n
        [DisplayName("Contract_auth")]
        public string ContractAuth { get; set; }
        /// <summary>
        /// a comma separated list of the references for the power unit makes gotten from the filter option service where the motor carrier has used this type of equipment in the past 24 months
        /// </summary>
        [DisplayName("pu_makes")]
        public List<string> Equipment { get; set; }

        [DisplayName("ins_type")]
        public string InspectionType { get; set; }

        [DisplayName("docpre_inc")]
        public string OperatingAuthorityInclude { get; set; }

        [DisplayName("docpre_exc")]
        public string OperatingAuthorityExclude { get; set; }

        //HHG = hhold
        //Property = Frt
        // Passenger = pas
        //fedAuth_OpTypeInclude=hhold%2CFrt%2Cpas& %2C =,
        [DisplayName("fedAuth_OpTypeInclude")]
        public string OperationType { get; set; }

        [DisplayName("revocation_from")]
        public string RevocationActions { get; set; }

        [DisplayName("rad_min")]
        public string FurthestDistanceInspectedMin { get; set; }

        [DisplayName("rad_max")]
        public string FurthestDistanceInspectedMax { get; set; }

        [DisplayName("inscomps")]
        public List<string> InsuranceCompanies { get; set; }

        public override string ToString()
        {
            StringBuilder requestBuilder = new StringBuilder();
            var properties = typeof(MarketingRequest).GetProperties();
            foreach (var property in properties)
            {

                var propertyValue = property.GetValue(this);
                if (property.PropertyType.IsGenericType)
                {
                    if (propertyValue is List<string> prop && prop.Count > 0)
                    {
                        requestBuilder.Append(
                            $"{property.GetCustomAttribute<DisplayNameAttribute>().DisplayName}={string.Join(",", prop)}&");
                    }
                }
                else
                {
                    if ((propertyValue is string strVal && !string.IsNullOrEmpty(strVal))
                        || propertyValue is int intVal && intVal != 0
                        || propertyValue is LocationType _)
                    {
                        requestBuilder.Append(
                            $"{property.GetCustomAttribute<DisplayNameAttribute>().DisplayName}={property.GetValue(this)}&");
                    }
                }

            }

            return requestBuilder.ToString().TrimEnd('&');
        }
    }

    public enum LocationType
    {
        City = 1,
        State = 2,
        Zip = 3,
        Country = 4,
        AreaCode = 5
    }

    public enum BasicAlert
    {
        UnsafeDriving = 1,
        HoursOfService = 2,
        DriverFitness = 3,
        ControlledSubstances = 4,
        VehicleMaintenance = 5,
        HazmatRelated = 6,
        CrashIndicator = 7
    }
}