using MediatR;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Proxy.VINModel;
using Royalty.Insurance.Settings;
using System;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Common.Network;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.VinCheck.Queries.GetVinInfo
{
    public class GetVinInfoQueryHandler : IRequestHandler<GetVinInfoQuery, List<VinCheckResponse>>
    {
        private readonly IVinMapperService _mapper;
        private readonly IHttpHelper _httpHelper;


        public GetVinInfoQueryHandler(IVinMapperService mapper, IHttpHelper httpHelper)
        {
            _mapper = mapper;
            _httpHelper = httpHelper;
        }

        public async Task<List<VinCheckResponse>> Handle(GetVinInfoQuery request, CancellationToken cancellationToken)
        {
            var apiResponse = await _httpHelper.Get<VinModel>($@"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVin/{request.VinNumber}?format=json");
            
            ParsingResponse(apiResponse);

            return _mapper.Map(apiResponse).ToList();
        }

        private void ParsingResponse(VinModel apiResponse)
        {
            if (apiResponse == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.BadRequest, ResourceCommonMessage.ErrorOccurred);
            }
            if (apiResponse.Results == null || apiResponse.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            //var value = apiResponse["Error Code"];
            //if (!string.IsNullOrEmpty(value) && !value.StartsWith("0"))
            //{
            //    //throw new RestApiResponseException((int)HttpStatusCode.BadRequest, ResourceCommonMessage.VinCheckFailed );
            //}

        }
    }
}