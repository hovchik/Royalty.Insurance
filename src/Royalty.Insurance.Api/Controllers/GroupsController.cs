using System.Collections.Generic;
using System.Common.Attributes;
using System.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Threading.Tasks;
using Royalty.Insurance.BusinessLayer.Groups;
using Royalty.Insurance.BusinessLayer.Messages;

namespace Royalty.Insurance.Api.Controllers
{
    
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class GroupsController : BaseController<GroupsController>
    {
        public GroupsController(ILogger<GroupsController> logger): base(logger)
        {
        }

        /// <summary>
        /// Get Groups
        /// </summary>
        /// <response code="200">Returns OK status and Groups </response>
        /// <response code="400">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<GroupResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<List<GroupResponse>>> Get()
        {
            Logger.LogDebug("Getting Groups");
            var response = await Mediator.Send(new GetGroupByMemberIdQuery());

            return Ok(response);
        }

        [AuthorizeByRoles]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<GroupResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("adminGet")]
        public async Task<ActionResult<List<GroupResponse>>> GetByAdmin()
        {
            Logger.LogDebug("Getting Groups by Admin");
            var response = await Mediator.Send(new GetGroupsQuery());

            return Ok(response);
        }

        /// <summary>
        /// Get Group Conversation
        /// </summary>
        /// <response code="200">Returns OK status and Groups </response>
        /// <response code="400">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<MessageResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("conversation/{groupId}/{from}/{to}/{pageIndex?}/{pageSize?}")]
        public async Task<ActionResult<PaginationResponse<MessageResponse>>> GetConversation([FromQuery] GetGroupConversationQuery request)
        {
            Logger.LogDebug("Getting Groups conversation");
            request.To = request.To.AddDays(1);
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST groups
        ///     {
        ///         "name: "name"
        ///     }
        ///
        /// </remarks>
        /// <summary>
        /// Create group
        /// </summary>
        /// <param name="request">request Body</param>
        /// <response code="200">Returns OK status and create group and return the one</response>
        /// <response code="400">Failed to create</response>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<List<GroupResponse>>> Create(CreateGroupCommand request)
        {
            Logger.LogDebug("Getting Groups");
            var response = await Mediator.Send(request);

            return Ok(response);
        }


        /// <remarks>
        /// Sample request:
        ///
        ///     POST groups/individual 
        ///     {
        ///         "memberId: 1
        ///     }
        ///
        /// </remarks>
        /// <summary>
        /// Create group
        /// </summary>
        /// <param name="request">request Body</param>
        /// <response code="200">Returns OK status and create group and return the one</response>
        /// <response code="400">Failed to create</response>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost("individual")]
        public async Task<ActionResult<List<GroupResponse>>> Create(CreateIndividualGroupCommand request)
        {
            Logger.LogDebug("Getting Groups");
            var response = await Mediator.Send(request);

            return Ok(response);
        }


        /// <remarks>
        /// Sample request:
        ///
        ///     PUT groups/1
        ///     {
        ///         "name: "name"
        ///     }
        ///
        /// </remarks>
        /// <summary>
        /// Update group
        /// </summary>
        /// <param name="id">id of the group</param>
        /// <param name="request">request Body</param>
        /// <response code="200">Returns OK status and Update group and return the one</response>
        /// <response code="400">Failed to create</response>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public async Task<ActionResult<List<GroupResponse>>> Update(int id, UpdateGroupCommand request)
        {
            Logger.LogDebug("Getting Groups");
            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}