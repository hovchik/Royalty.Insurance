using System.Common.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Threading.Tasks;
using Royalty.Insurance.BusinessLayer.Cities.Queries;
using Royalty.Insurance.BusinessLayer.ILogic;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Api.Controllers
{
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class CitiesController : BaseController<CitiesController>
    {
        public CitiesController(ILogger<CitiesController> logger): base(logger)
        {
        }

        /// <summary>
        /// Get Cities by state
        /// </summary>
        /// <param name="stateId">State id</param>
        /// <response code="200">Returns OK status and cities </response>
        /// <response code="400">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(CityListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{stateId}")]
        public async Task<ActionResult<CityListViewModel>> Get(int stateId)
        {
            Logger.LogDebug("Getting cities");
            var response = await Mediator.Send(new GetCitiesByStateIdQuery { StateId = stateId});

            return Ok(response);
        }

        /// <summary>
        /// Get Cities
        /// </summary>
        /// <response code="200">Returns OK status and cities </response>
        /// <response code="400">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(CityListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<CityListViewModel>> Get()
        {
            Logger.LogDebug("Getting Cities");
            var response = await Mediator.Send(new GetCitiesQuery());

            return Ok(response);
        }
    }
}