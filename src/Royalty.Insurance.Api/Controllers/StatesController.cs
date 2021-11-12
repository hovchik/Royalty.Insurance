using System.Common.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Threading.Tasks;
using Royalty.Insurance.BusinessLayer.States.Queries;
using Royalty.Insurance.BusinessLayer.States.Queries.GetState;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Api.Controllers
{
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class StatesController : BaseController<StatesController>
    {
        public StatesController(ILogger<StatesController> logger): base(logger)
        {
        }

        /// <summary>
        /// Get States
        /// </summary>
        /// <response code="200">Returns OK status and states </response>
        /// <response code="404">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(StateListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<StateListViewModel>> Get()
        {
            Logger.LogDebug("Getting states");
            var response = await Mediator.Send(new GetStateQuery());

            return Ok(response);
        }
    }
}