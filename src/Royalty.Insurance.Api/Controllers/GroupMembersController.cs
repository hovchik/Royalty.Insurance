using System.Collections.Generic;
using System.Common.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.GroupMembers;

namespace Royalty.Insurance.Api.Controllers
{
    [Authorize]
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class GroupMembersController : BaseController<GroupMembersController>
    {
        private readonly ICurrentUserService _currentUserService;

        public GroupMembersController(ILogger<GroupMembersController> logger, ICurrentUserService currentUserService): base(logger)
        {
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// Get Group members
        /// </summary>
        /// <response code="200">Returns OK status and Groups </response>
        /// <response code="400">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<GroupResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet()]
        public async Task<ActionResult<List<GroupMemberResponse>>> Get()
        {
            Logger.LogDebug("Getting Group members");
            var response = await Mediator.Send(new GetGroupMemberByUserIdQuery { UserRequestedId = _currentUserService .UserId});

            return Ok(response);
        }

        /// <summary>
        /// Get Group members
        /// </summary>
        /// <response code="200">Returns OK status and Groups </response>
        /// <response code="400">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<GroupResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("groups/{groupId}")]
        public async Task<ActionResult<List<GroupMemberResponse>>> Get(int groupId)
        {
            Logger.LogDebug("Getting Group members");
            var response = await Mediator.Send(new GetUserGroupMemberByGroupIdQuery {UserRequestedId = _currentUserService.UserId, GroupId =  groupId});

            return Ok(response);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST groupMembers
        ///     {
        ///         "memberId: 1
        ///     }
        ///
        /// </remarks>
        /// <summary>
        /// Create group
        /// </summary>
        /// <param name="groupId">group Id</param>
        /// <param name="request">request Body</param>
        /// <response code="200">Returns OK status and create group and return the one</response>
        /// <response code="400">Failed to create</response>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost("groups/{groupId}")]
        public async Task<ActionResult<List<GroupResponse>>> Create(int groupId, AddMemberCommand request)
        {
            Logger.LogDebug("Add member");
            request.UserRequestedId = _currentUserService.UserId;
            request.GroupId = groupId;
            var response = await Mediator.Send(request);

            return Ok(response);
        }


        /// <summary>
        /// delete  group member
        /// </summary>
        /// <param name="groupId">group Id</param>
        /// /// <param name="memberId">member Id</param>
        /// <response code="200">Returns OK status and Update group and return the one</response>
        /// <response code="400">Failed to create</response>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpDelete("groups/{groupId}/{memberId}")]
        public async Task<ActionResult> Delete(int groupId, int memberId)
        {
            Logger.LogDebug("Removing member from  Group");
            var request = new RemoveMemberCommand() {UserRequestedId = _currentUserService.UserId, GroupId =  groupId, MemberIds = new List<int> { memberId } };
            await Mediator.Send(request);

            return Ok();
        }
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPut("mute")]
        public async Task<ActionResult<BaseResponse<bool>>> SetGroupMute(MuteCommand request)
        {
            request.UserId = _currentUserService.UserId;
            await Mediator.Send(request);

            return Ok(new BaseResponse<bool>(true));
        }
    }
}