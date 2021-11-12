using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.ILogic;
using Royalty.Insurance.Proxy.APIModels.Marketing;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;
using System.Common.Attributes;
using System.Common.Extensions;
using System.Threading.Tasks;
using Royalty.Insurance.BusinessLayer.CabMarketing;

namespace Royalty.Insurance.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CabMarketingController : BaseController<CabMarketingController>
    {
        public CabMarketingController(ILogger<CabMarketingController> logger) : base(logger)
        {

        }

        [AuthorizeByRoles(nameof(UserRoleType.Marketing))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(CabMarketingOptions), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("Options")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60 * 60 * 24)]//talk to front
        public async Task<ActionResult<CabMarketingOptions>> Get()
        {
            Logger.LogDebug("Getting Marketing info list");
            var response = await Mediator.Send(new GetOptionsQuery());

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Marketing))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<DetailedSearch>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost("Carriers/{pageIndex}/{pageSize}/{cabIndex}")]
        public async Task<ActionResult<List<DetailedSearch>>> GetCarriers(MarketingRequest request, int pageIndex, int pageSize, int cabIndex = 0)
        {
            Logger.LogDebug("Getting Carriers by model");
            var response = await Mediator.Send(new GetQuery
            {
                Request = request,
                PageIndex = pageIndex,
                CabIndex = cabIndex,
                PageSize = pageSize
            });

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Marketing))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<DetailedSearch>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost("CarriersByRequestString/{pageIndex}/{pageSize}/{cabIndex}")]
        public async Task<ActionResult<List<DetailedSearch>>> GetCarriersByRequest(string request, int pageIndex, int pageSize, int cabIndex = 0)
        {
            Logger.LogDebug("Getting Carriers by request");
            var response = await Mediator.Send(new GetByRequestQuery
            {
                CabIndex = cabIndex,
                PageIndex = pageIndex,
                Request = request,
                PageSize = pageSize,
            });

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Marketing))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<BasicAlertResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("BasicAlert")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60 * 60 * 24)]//talk to front
        public async Task<ActionResult<List<BasicAlertResponse>>> GetBasicAlert()
        {
            Logger.LogDebug("Getting Basic Alert list");
            var response = await Mediator.Send(new GetBasicAlertQuery());

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Marketing))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<LocationTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("LocationType")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60 * 60 * 24)]//talk to front
        public async Task<ActionResult<List<LocationTypeResponse>>> GetLocationType()
        {
            Logger.LogDebug("Getting Location type list");
            var response = await Mediator.Send(new GetLocationTypeQuery());

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Marketing))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<GvwrResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("Gvwr")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60 * 60 * 24)]//talk to front
        public async Task<ActionResult<List<GvwrResponse>>> GetGvwr()
        {
            Logger.LogDebug("Getting gvwr list");
            var response = await Mediator.Send(new GetGvwrQuery());

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Marketing))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<OperationTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("OperationType")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60 * 60 * 24)]//talk to front
        public async Task<ActionResult<List<OperationTypeResponse>>> GetOperationType()
        {
            Logger.LogDebug("Getting OperationTypes list");
            var response = await Mediator.Send(new GetOperationTypesQuery());

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Marketing))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<CommonAuthTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("CommonAuthParams")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60 * 60 * 24)]//talk to front
        public async Task<ActionResult<List<CommonAuthTypeResponse>>> GetCommonAuthParams()
        {
            Logger.LogDebug("Getting CommonAuthTypeResponse list");
            var response = await Mediator.Send(new GetCommonAuthParamsQuery());

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Marketing))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("ExcelFilters")]
        public async Task<ActionResult<List<string>>> ExcelFilters()
        {
            Logger.LogDebug("Getting Excel report column list");
            var response = await Mediator.Send(new GetFiltersQuery());

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Marketing))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(IFormFile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost("ExcelReport")]
        public async Task<ActionResult<IFormFile>> ExcelReport(DownloadExcelFileQuery excelModel)
        {
            Logger.LogDebug("Getting Excel report File");
            var response = await Mediator.Send(excelModel);

            return File(response.DataInBytes, response.ContentType);
        }
    }
}
