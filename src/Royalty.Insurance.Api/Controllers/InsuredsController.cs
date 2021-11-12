using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.Insureds;
using Royalty.Insurance.BusinessLayer.Insureds.Queries;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;
using System.Collections.Generic;
using System.Common.Attributes;
using System.Threading.Tasks;

namespace Royalty.Insurance.Api.Controllers
{
    [Authorize]
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class InsuredsController : BaseController<InsuredsController>
    {

        public InsuredsController(ILogger<InsuredsController> logger) : base(logger)
        {
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST insureds
        ///     {
        ///         TODO BODY
        ///     }
        ///
        /// </remarks>
        /// <summary>
        /// Create insured
        /// </summary>
        /// <param name="request">request Body</param>
        /// <response code="200">Returns OK status and create insured and return the one</response>
        /// <response code="400">Failed to create</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(InsuredResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost()]
        public async Task<ActionResult<InsuredResponse>> Create(CreateInsuredCommand request)
        {
            Logger.LogDebug("Creating Insured");
            //to do hard coded user id, update when login is implemented
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PUT  insureds
        ///     {
        ///         TODO BODY
        ///     }
        ///
        /// </remarks>
        /// <summary>
        /// Update insured
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="request">request Body</param>
        /// <response code="200">Returns OK status and update insured and return the one</response>
        /// <response code="400">Failed to update</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(InsuredResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public async Task<ActionResult<InsuredResponse>> Update(int id, UpdateInsuredCommand request)
        {
            Logger.LogDebug("Updating Insured");
            if (id != request.Id)
            {
                return BadRequest();
            }
            //to do hard coded user id, update when login is implemented
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Get Insureds
        /// </summary>
        /// <response code="200">Returns OK status and list of insureds</response>
        /// <response code="400">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<InsuredResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{pageIndex}/{pageSize}")]
        public async Task<ActionResult<InsuredResponse>> Get(int pageIndex, int pageSize)
        {
            Logger.LogDebug("Getting Insureds");
            var response = await Mediator.Send(new GetInsuredsQuery { PageIndex = pageIndex, PageSize = pageSize });

            return Ok(response);
        }

        /// <summary>
        /// search  Insureds by name
        /// </summary>
        /// <response code="200">Returns OK status and list of insureds</response>
        /// <response code="400">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(InsuredListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("search")]
        public async Task<ActionResult<InsuredListViewModel>> Search([FromQuery] SearchInsuredByNameQuery query)
        {
            Logger.LogDebug("Getting Insureds by search");

            return await Mediator.Send(query);
        }

        /// <summary>
        /// Get Insured by Id
        /// </summary>
        /// <param name="id">Insured id</param>
        /// <response code="200">Returns OK status and insured </response>
        /// <response code="400">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(InsuredResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<InsuredResponse>> Get(int id)
        {
            Logger.LogDebug("Getting Insured");
            var response = await Mediator.Send(new GetInsuredByIdQuery { Id = id });

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(InsuredsNotesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("Notes/{pageIndex}/{pageSize}")]
        public async Task<ActionResult<InsuredsNotesResponse>> GetNotes(int pageIndex, int pageSize)
        {
            Logger.LogDebug("Getting Insureds Notes");
            var response = await Mediator.Send(new GetInsuredsNotesQuery { PageSize = pageSize, PageIndex = pageIndex });

            return Ok(response);
        }
    }
}