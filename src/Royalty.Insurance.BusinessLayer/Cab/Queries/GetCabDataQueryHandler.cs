using MediatR;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Proxy.APIModels.Core;
using Royalty.Insurance.Proxy.APIModels;
using Royalty.Insurance.Proxy.APIResponseModels;
using System;
using System.Common.Authentication.Models;
using System.Common.Network;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Royalty.Insurance.BusinessLayer.VinCheck;
using Royalty.Insurance.Proxy.Response;
using System.Collections.Generic;
using Humanizer;

namespace Royalty.Insurance.BusinessLayer.Cab
{
    public class GetCabDataQueryHandler : IRequestHandler<GetCabDataQuery, QuoteSheetModel>
    {
        private readonly IDotCoreMapperService _cabMapper;
        private readonly IHttpHelper _client;
        private readonly AppSetting _appSetting;
        private readonly IRequestHandler<GetVinInfoQuery, List<VinCheckResponse>> _vinHandler;
        private const string MakeField = "Make";
        private const string TypeField = "Vehicle Type";

        public GetCabDataQueryHandler(IDotCoreMapperService cabMapper, IOptions<AppSetting> options, IHttpHelper client, IRequestHandler<GetVinInfoQuery, List<VinCheckResponse>> vinHandler)
        {
            _cabMapper = cabMapper;
            _appSetting = options.Value;
            _client = client;
            _vinHandler = vinHandler;
        }

        public async Task<QuoteSheetModel> Handle(GetCabDataQuery request, CancellationToken cancellationToken)
        {
            if (request.DotNumber != 0)
            {
                var dotCabModel = await _client.Get<DotCoreResponse>($@"https://ws.cabadvantage.com/rest/services/core/dot/{request.DotNumber}?key={_appSetting.CabKey}");
                var dotCarrierModel = await _client.Get<DOTResponse>($@"https://ws.cabadvantage.com/rest/services/carrier/{request.DotNumber}?key={_appSetting.CabKey}");
                QuoteSheetModel result = new QuoteSheetModel(request.DotNumber);

                var vehicleVinCodes = dotCabModel.inspections.SelectMany(v => v.units).Where(y => y.vin != null).Select(x => x.vin).Distinct().ToList();
                List<CabVinResponse> vinCodeAndVehicleInfo = new List<CabVinResponse>();
                var vehicleMakeTasks = vehicleVinCodes.Select(async vin =>
                {
                    await GenerateVehilceInfos(vinCodeAndVehicleInfo, vin, cancellationToken);
                });

                await Task.WhenAll(vehicleMakeTasks);

                _cabMapper.UpdateEntity(result, dotCabModel, dotCarrierModel, vinCodeAndVehicleInfo);

                return result;
            }

            return null;
        }

        private async Task GenerateVehilceInfos(List<CabVinResponse> vinCodeAndVehicleInfo, string vin, CancellationToken cancellationToken)
        {
            var resp = await _vinHandler.Handle(new GetVinInfoQuery { VinNumber = vin }, cancellationToken);
            vinCodeAndVehicleInfo.Add(new CabVinResponse
            {
                Vin = vin,
                Make = resp.FirstOrDefault(x => x.Field.Equals(MakeField))?.Value?.Trim()?.Transform(To.LowerCase, To.TitleCase),
                Type = resp.FirstOrDefault(x => x.Field.Equals(TypeField))?.Value?.Trim()?.Transform(To.LowerCase, To.TitleCase)
            });
        }
    }
}
