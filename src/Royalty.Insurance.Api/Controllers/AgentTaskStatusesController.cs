using System.Collections.Generic;
using System.Common.Attributes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.AgentTaskStatuses;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentTaskStatusesController : BaseController<AgentTaskStatusesController>
    {
        public AgentTaskStatusesController(ILogger<AgentTaskStatusesController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Get Agent task statuses
        /// </summary>
        /// <response code="200">Returns OK status and Agent task statuses </response>
        /// <response code="400">Failed to get</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<AgentTaskStatusResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{pageIndex}/{pageSize}")]
        public async Task<ActionResult<List<AgentTaskStatusResponse>>> GetAsync(int pageIndex, int pageSize)
        {
            Logger.LogDebug("Getting Agent Task Statuses");
            var response = await Mediator.Send(new GetAgentTaskStatusesQuery { PageIndex = pageIndex, PageSize = pageSize });

            return Ok(response);
        }

        /// <summary>
        /// Get Agent task status by Id
        /// </summary>
        /// <response code="200">Returns OK status and Agent task statuses </response>
        /// <response code="400">Failed to get</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(AgentTaskStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<AgentTaskStatusResponse>> GetAsync(int id)
        {
            Logger.LogDebug($"Getting Agent Task Statuses by Id {id}");
            var response = await Mediator.Send(new GetAgentTaskStatusByIdQuery { Id = id });

            return Ok(response);
        }

        /// <summary>
        /// Create Agent task status
        /// </summary>
        /// <response code="200">Returns OK status and Agent task status</response>
        /// <response code="400">Failed to create</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.SuperAdmin))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(AgentTaskStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [HttpPost]
        public async Task<ActionResult<AgentTaskStatusResponse>> CreateAsync(CreateAgentTaskStatusCommand request)
        {
            Logger.LogDebug($"Creating Agent Task Statuses, name is {request.Name}");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Update Agent task status
        /// </summary>
        /// <response code="200">Returns OK status and Agent task status </response>
        /// <response code="400">Failed to update</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.SuperAdmin))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(AgentTaskStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [HttpPut("{id}")]
        public async Task<ActionResult<AgentTaskStatusResponse>> UpdateAsync(int id, UpdateAgentTaskStatusCommand request)
        {
            Logger.LogDebug($"Updating Agent Task Statuses, name is {request.Name}");
            if (id != request.Id)
            {
                return BadRequest();
            }
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Delete Agent task status
        /// </summary>
        /// <response code="200">Returns OK status </response>
        /// <response code="400">Failed to delete</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.SuperAdmin))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Logger.LogDebug($"Deleting Agent Task Statuses, id is {id}");
            await Mediator.Send(new DeleteAgentTaskStatusCommand { Id = id });

            return Ok();
        }
    }
}
