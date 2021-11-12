using MediatR;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Proxy.APIResponseModels;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System;
using System.Common.Authentication.Models;
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.ProRateCalculator
{
    public class ProRateCalcQueryHandler : IRequestHandler<GetProRateCalcQuery, ProRateResponse>
    {
        private readonly AppSetting _appSetting;

        public ProRateCalcQueryHandler(IOptions<AppSetting> options)
        {
            _appSetting = options.Value;
        }

        public async Task<ProRateResponse> Handle(GetProRateCalcQuery request, CancellationToken cancellationToken)
        {
            DateTime dec31 = new DateTime(request.ProRateRequest.To.Year, 12, 31);
            var daysCount = request.ProRateRequest.To.Subtract(request.ProRateRequest.From).Days;
            var coefficient = (double)daysCount / (double)dec31.DayOfYear;
            ProRateResponse response = new ProRateResponse();
            double total = 0.0;
            foreach (var coverage in request.ProRateRequest.Coverages)
            {
                var value = coverage.Key == CoverageTypeCode.PdDeductibles ?
                    Math.Ceiling(Math.Ceiling(coverage.Value * request.ProRateRequest.Percentage / 100) * coefficient) :
                    Math.Ceiling(coverage.Value * coefficient);
                total += value;
                response.CoverageValues.TryAdd(coverage.Key, value);
            }

            response.Total = total + request.ProRateRequest.BrokerFee;
            double twentyPercentOfTotal = total * _appSetting.ProRatePercent / 100;
            response.DownPayment = twentyPercentOfTotal + request.ProRateRequest.BrokerFee;
            if (Math.Abs(response.Total) < 0.0001)
            {
                throw new RestApiResponseException((int)HttpStatusCode.BadRequest, ResourceCommonMessage.InvalidCalculation);
            }
            response.DownToTotalPercentage = Math.Ceiling(response.DownPayment * 100 / response.Total);

            return response;
        }
    }
}