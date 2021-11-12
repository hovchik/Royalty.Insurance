using System.Common.Attributes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.UserActivityLogs;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Api.Controllers
{
    [Produces(SystemConstants.MediaType)]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsersActivityLogsController : BaseController<UsersActivityLogsController>
    {

        public UsersActivityLogsController(ILogger<UsersActivityLogsController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Get all users  User Activity Log
        /// </summary>
        /// <returns>Returns List<UserActivityLogResponse/></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(PaginationResponse<UserActivityLogResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{pageIndex}/{pageSize}")]
        public async Task<ActionResult<PaginationResponse<UserActivityLogResponse>>> GetAsync(int pageIndex, int pageSize)
        {
            //todo super user
            Logger.LogDebug("Creating user");
            return Ok(await Mediator.Send(new GetUserActivityLogQuery { PageIndex = pageIndex, PageSize = pageSize }));
        }
    }
}
