using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.Cab;
using Royalty.Insurance.Proxy.APIResponseModels;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;
using System.Common.Attributes;
using System.Threading.Tasks;

namespace Royalty.Insurance.Api.Controllers
{

    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class CabController : BaseController<CabController>
    {

        public CabController(ILogger<CabController> logger) : base(logger)
        {
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(QuoteSheetModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<QuoteSheetModel>> Get(int id)
        {
            Logger.LogDebug("Getting from CAB");
            var response = await Mediator.Send(new GetCabDataQuery { DotNumber = id });

            return Ok(response);
        }
    }
}
