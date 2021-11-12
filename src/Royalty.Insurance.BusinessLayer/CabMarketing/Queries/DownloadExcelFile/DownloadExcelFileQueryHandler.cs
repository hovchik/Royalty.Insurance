using ClosedXML.Excel;
using MediatR;
using Royalty.Insurance.Proxy.APIModels.Marketing;
using System;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Common.Storage.Response;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class DownloadExcelFileQueryHandler : IRequestHandler<DownloadExcelFileQuery, FileResponse>
    {
        public async Task<FileResponse> Handle(DownloadExcelFileQuery request, CancellationToken cancellationToken)
        {
            var excel = await Task.Run(() => ExportToExcel(request.Data, request.Columns), cancellationToken);

            return new FileResponse
            {
                ContentType = @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                DataInBytes = excel
            };
        }
        public byte[] ExportToExcel(List<DetailedSearch> data, List<string> columns)
        {
            try
            {
                var model = EqualizeObjects(data);
                using DataTable dataTable = CreateDataTable(model, columns);
                using XLWorkbook wb = new XLWorkbook();
                wb.Worksheets.Add(dataTable, "Cab Marketing Report");
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                using MemoryStream memoryStream = new MemoryStream();
                wb.SaveAs(memoryStream);

                return memoryStream.ToArray();
            }
            catch (ArgumentException exception)
            {
                throw new RestApiResponseException(exception.Message);
            }
        }
        public DataTable CreateDataTable(IEnumerable<CabExcelModel> list, List<string> columns)
        {
            Type type = typeof(CabExcelModel);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable
            {
                TableName = typeof(CabExcelModel).FullName ?? string.Empty
            };

            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (CabExcelModel entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity, null);
                }

                dataTable.Rows.Add(values);
            }

            return columns.Any() ? dataTable.DefaultView.ToTable(false, columns.ToArray()) : dataTable;
        }

        public IEnumerable<CabExcelModel> EqualizeObjects(List<DetailedSearch> data)
        {
            var licensedInsurance = data.SelectMany(x => (x.L_I ?? new List<L_I>()).SelectMany(y => (y.insurance ?? new List<Proxy.APIModels.Marketing.Insurance>()).Select(z => new
            {
                Dot = x.dot,
                Inspection_Radius = x?.events?.inspection_Radius,
                Legal_Name = x?.contact?.name?.leg,
                DBA = x.contact?.name?.dba,
                Company_Rep = x?.contact?.reps,
                CellPhone = x.contact?.phone?.cell,
                Phone = x.contact?.phone?.phone,
                Fax = x.contact?.phone?.fax,
                Mailing_Street = x.contact?.address?.mailing?.street,
                Mailing_City = x.contact?.address?.mailing?.city,
                Mailing_Country = x.contact?.address?.mailing?.county,
                Mailing_State = x.contact?.address?.mailing?.state,
                Mailing_Zip = x.contact?.address?.mailing?.ZIP,
                Business_Street = x.contact?.address?.business?.street,
                Business_City = x.contact?.address?.business?.city,
                Business_Country = x.contact?.address?.business?.county,
                Business_State = x.contact?.address?.business?.state,
                Business_Zip = x.contact?.address?.business?.ZIP,
                Email = x.contact?.email,
                Power_Units = x.units?.pu,
                Trucks = x.units?.trucks?.all,
                TrucksOwn = x.units?.trucks?.own,
                TrucksLease = x.units?.trucks?.lease,
                TrailersOwn = x.units?.trailers?.own,
                TrailersLease = x.units?.trailers?.lease,
                Insurer = z?.insurer,
                Policy_Number = z?.polNum,
                Policy_Effective_Date = z?.effDt,
                Filling_Locale = z?.locale,
                TypeCd = z?.typeCd,
                Insurance_Type = z?.insType,
                BOC3 = z?.boc3,
                PolExpMonth = z?.polExpMonth,
                PolExpDay = z?.polExpDay,
                PolExpDate = z?.polExpDate,
                Prefix = y?.pre,
                Docket = y?.doc,
                Common = y?.common,
                Contract = y?.contract,
                Broker = y?.broker,
                Pass = y?.pass,
                Hhold = y?.hHold,
                Bipd_Required = y?.bipdReq,
                Drivers = x?.drivers?.total,
                Drivers_CDL = x?.drivers?.CDL,
                Mlg150 = x?.mileage?.Mlg150,
                MCS150MileageYear = x?.mileage?.MCS150MileageYear,
                YearInBusiness = x?.MCS150?.yib,
                DOTAddDate = x?.MCS150?.DOTAddDate,
                Date = x?.MCS150?.date,
                Hazmat = x?.MCS150?.hazmat

            }))).ToList();

            var scores = data.SelectMany(x => x.scores.Select(y => new
            {
                Dot = x.dot,
                Dot_Rating = y?.DOTRating?.rating,
                Dot_Date = y?.DOTRating?.date,
                ISS_Score = y?.ISS?.score,
                ISS_Src = y?.ISS?.src,
                UnSafe_alert = y?.BASICS?._unsafe?.alert,
                UnSafe_Score = y?.BASICS?._unsafe?.score,
                HOS_alert = y?.BASICS?.HOS?.alert,
                HOS_Score = y?.BASICS?.HOS?.score,
                Drfit_alert = y?.BASICS?.drFit?.alert,
                Drfit_score = y?.BASICS?.drFit?.score,
                Contrsubst_alert = y?.BASICS?.contrSubst?.alert,
                Contrsubst_score = y?.BASICS?.contrSubst?.score,
                Vm_alert = y?.BASICS?.vm?.alert,
                Vm_score = y?.BASICS?.vm?.score,
                Hazmat_alert = y?.BASICS?.hazmat?.alert,
                Crash_alert = y?.BASICS?.crash?.alert,
                Crash_score = y?.BASICS?.crash?.score
            })).ToList();

            var returnModel = from lic in licensedInsurance
                              join score in scores on lic.Dot equals score.Dot into licWithScoreMap
                              from licWithScore in licWithScoreMap.DefaultIfEmpty()
                              select new CabExcelModel
                              {
                                  Dot = lic.Dot,
                                  Inspection_Radius = lic.Inspection_Radius,
                                  Legal_Name = lic.Legal_Name,
                                  DBA = lic.DBA,
                                  CellPhone = lic.CellPhone,
                                  Phone = lic.Phone,
                                  Fax = lic.Fax,
                                  Company_Rep1 = lic.Company_Rep?.name1,
                                  Company_Rep2 = lic.Company_Rep?.name2,
                                  Mailing_Street = lic.Mailing_Street,
                                  Mailing_City = lic.Mailing_City,
                                  Mailing_Country = lic.Mailing_Country,
                                  Mailing_State = lic.Mailing_State,
                                  Mailing_Zip = lic.Mailing_Zip,
                                  Business_Street = lic.Business_Street,
                                  Business_City = lic.Business_City,
                                  Business_Country = lic.Business_Country,
                                  Business_State = lic.Business_State,
                                  Business_Zip = lic.Business_Zip,
                                  Email = lic.Email,
                                  Power_Units = lic.Power_Units,
                                  Trucks = lic.Trucks,
                                  TrucksOwn = lic.TrucksOwn,
                                  TrucksLease = lic.TrucksLease,
                                  TrailersOwn = lic.TrailersOwn,
                                  TrailersLease = lic.TrailersLease,
                                  Insurer = lic.Insurer,
                                  Policy_Number = lic.Policy_Number,
                                  Policy_Effective_Date = lic.Policy_Effective_Date,
                                  Filling_Locale = lic.Filling_Locale,
                                  TypeCd = lic.TypeCd,
                                  Insurance_Type = lic.Insurance_Type,
                                  BOC3 = lic.BOC3,
                                  PolExpMonth = lic.PolExpMonth,
                                  PolExpDay = lic.PolExpDay,
                                  PolExpDate = lic.PolExpDate,
                                  Prefix = lic.Prefix,
                                  Docket = lic.Docket,
                                  Common = lic.Common,
                                  Contract = lic.Contract,
                                  Broker = lic.Broker,
                                  Pass = lic.Pass,
                                  Hhold = lic.Hhold,
                                  Bipd_Required = lic.Bipd_Required,
                                  Drivers = lic.Drivers,
                                  Drivers_CDL = lic.Drivers_CDL,
                                  Mlg150 = lic.Mlg150,
                                  MCS150MileageYear = lic.MCS150MileageYear,
                                  YearInBusiness = lic.YearInBusiness,
                                  DOTAddDate = lic.DOTAddDate,
                                  Date = lic.Date,
                                  Hazmat = lic.Hazmat,
                                  ////scores
                                  Dot_Rating = licWithScore.Dot_Rating,
                                  Dot_Date = licWithScore.Dot_Date,
                                  ISS_Score = licWithScore.ISS_Score,
                                  ISS_Src = licWithScore.ISS_Src,
                                  UnSafe_alert = licWithScore.UnSafe_alert,
                                  UnSafe_Score = licWithScore.UnSafe_Score,
                                  HOS_alert = licWithScore.HOS_alert,
                                  HOS_Score = licWithScore.HOS_Score,
                                  Drfit_alert = licWithScore.Drfit_alert,
                                  Drfit_score = licWithScore.Drfit_score,
                                  Contrsubst_alert = licWithScore.Contrsubst_alert,
                                  Contrsubst_score = licWithScore.Contrsubst_score,
                                  Vm_alert = licWithScore.Vm_alert,
                                  Vm_score = licWithScore.Vm_score,
                                  Hazmat_alert = licWithScore.Hazmat_alert,
                                  Crash_alert = licWithScore.Crash_alert,
                                  Crash_score = licWithScore.Crash_score,
                              };

            return returnModel.Distinct(new CabExcelModelComparer());
        }
    }
}
