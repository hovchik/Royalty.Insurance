using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.FlagmanWebHook;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Royalty.Insurance.Api.Controllers
{
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class FlagmanCallHistoryController : BaseController<FlagmanCallHistoryController>
    {
        public FlagmanCallHistoryController(ILogger<FlagmanCallHistoryController> logger) : base(logger)
        {
        }


        [Authorize]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<UserPhoneCallHistoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{pageIndex?}/{pageSize?}")]
        public async Task<ActionResult<List<UserPhoneCallHistoryResponse>>> Get(int pageIndex = 1, int pageSize = 30)
        {
            Logger.LogDebug("Getting user all call logs");
            var response = await Mediator.Send(new GetCallHistoryQuery { PageIndex = pageIndex, PageSize = pageSize });

            return Ok(response);
        }

        [Authorize]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<UserPhoneCallHistoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost("FilteredData")]
        public async Task<ActionResult<List<UserPhoneCallHistoryResponse>>> GetFiltered(GetFilteredCallLogsQuery request)
        {
            Logger.LogDebug("Getting user all call logs");
            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}
