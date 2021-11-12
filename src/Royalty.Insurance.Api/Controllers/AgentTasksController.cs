using System.Collections.Generic;
using System.Common.Attributes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.AgentTasks;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentTasksController : BaseController<AgentTasksController>
    {
        public AgentTasksController(ILogger<AgentTasksController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Get Agent tasks
        /// </summary>
        /// <response code="200">Returns OK status and Agent tasks </response>
        /// <response code="400">Failed to get</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<AgentTaskResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{pageIndex}/{pageSize}")]
        public async Task<ActionResult<List<AgentTaskResponse>>> GetAsync(int pageIndex, int pageSize)
        {
            Logger.LogDebug("Getting Agent Tasks");
            var response = await Mediator.Send(new GetAgentTasksQuery {PageIndex = pageIndex, PageSize = pageSize});

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(AgentTaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<AgentTaskResponse>> GetAsync(int id)
        {
            Logger.LogDebug($"Getting Agent Tasks by Id {id}");
            var response = await Mediator.Send(new GetAgentTaskByIdQuery() { Id= id});

            return Ok(response);
        }



        /// <summary>
        /// Get Agent tasks by task status
        /// </summary>
        /// <response code="200">Returns OK status and Agent tasks </response>
        /// <response code="400">Failed to get</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<AgentTaskResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("/taskstatus/{taskStatusId}/{pageIndex}/{pageSize}")]
        public async Task<ActionResult<AgentTaskResponse>> GetAgentTaskByStatusAsync(int taskStatusId, int pageIndex, int pageSize)
        {
            Logger.LogDebug($"Getting Agent Tasks by task status Id {taskStatusId}");
            var response = await Mediator.Send(new GetAgentTasksByStatusQuery {PageIndex = pageIndex, PageSize =  pageSize, AgentTaskStatusId =  taskStatusId});

            return Ok(response);
        }

        /// <summary>
        /// Create Agent task
        /// </summary>
        /// <response code="200">Returns OK status and Agent task </response>
        /// <response code="400">Failed to create</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(AgentTaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [HttpPost]
        public async Task<ActionResult<AgentTaskResponse>> CreateAsync(CreateAgentTaskCommand request)
        {
            Logger.LogDebug($"Creating Agent Task, title is {request.Title}");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Update Agent task
        /// </summary>
        /// <response code="200">Returns OK status and Agent task</response>
        /// <response code="400">Failed to update</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(AgentTaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [HttpPut("{id}")]
        public async Task<ActionResult<AgentTaskResponse>> UpdateAsync(int id, UpdateAgentTaskCommand request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }
            Logger.LogDebug($"Updating Agent Task, title is {request.Title}");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Update Agent task's status
        /// </summary>
        /// <response code="200">Returns OK status and Agent task</response>
        /// <response code="400">Failed to update</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(AgentTaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [HttpPut("{id}/status")]
        public async Task<ActionResult<AgentTaskResponse>> UpdateAgentTaskStatusAsync(int id, UpdateAgentTaskTaskStatusCommand request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }
            Logger.LogDebug($"Updating Agent Task's status,  status is {request.AgentTaskStatusId}");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Update Agent task's status
        /// </summary>
        /// <response code="200">Returns OK status and Agent task</response>
        /// <response code="400">Failed to update</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(AgentTaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [HttpPut("{id}/assignee")]
        public async Task<ActionResult<AgentTaskResponse>> UpdateAgentTaskStatusAsync(int id, UpdateAgentTaskAssigneeCommand request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }
            Logger.LogDebug($"Updating Agent Task's assignee,  assignee is {request.AssigneeId}");
            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}
