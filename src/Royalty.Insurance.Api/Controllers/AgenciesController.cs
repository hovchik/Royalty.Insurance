using System.Common.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Royalty.Insurance.BusinessLayer.Agencies;
using Royalty.Insurance.BusinessLayer.Agencies.Queries;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Api.Controllers
{
    [Authorize]
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class AgenciesController : BaseController<AgenciesController>
    {

        public AgenciesController(ILogger<AgenciesController> logger): base(logger)
        {
        }

        /// <summary>
        /// Update Agency
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT Agencies
        ///     {
        ///         "Name": "Name",
        ///         "FaxNumber": "FaxNumber",
        ///         "FullAddress": "FullAddress",
        ///         "PhoneNumber": "PhoneNumber"
        ///     }
        ///
        /// </remarks>
        /// <summary>
        /// Update existing Agency
        /// </summary>
        /// <param name="id">Agency id</param>
        /// <param name="request">request Body</param>
        /// <response code="200">Returns OK status and update agency and return updated one</response>
        /// <response code="400">Failed to update</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(AgencyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public async Task<ActionResult<AgencyResponse>> Update(int id, UpdateAgencyCommand request)
        {
            if (id != request.Id)
            {
                return BadRequest(ResourceCommonMessage.BodyIdQueryIdMatch);
            }
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Get Agency by Id
        /// </summary>
        /// <param name="id">Agency id</param>
        /// <response code="200">Returns OK status and agency </response>
        /// <response code="400">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(AgencyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<AgencyResponse>> Get(int id) =>
            await Mediator.Send(new GetAgencyByIdQuery{Id =  id});
    }
}