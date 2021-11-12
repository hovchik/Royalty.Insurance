using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.DriverInfo;
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
    public class DriverInfosController : BaseController<DriverInfosController>
    {
        public DriverInfosController(ILogger<DriverInfosController> logger) : base(logger)
        {
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<DriverInfoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<DriverInfoResponse>> Get()
        {
            Logger.LogDebug("Getting All driver Info");
            var response = await Mediator.Send(new GetDriverInfoQuery());

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(DriverInfoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverInfoResponse>> Get(int id)
        {
            Logger.LogDebug("Getting driver Info");
            var response = await Mediator.Send(new GetDriverInfoByIdQuery { Id = id });

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(DriverInfoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public async Task<ActionResult<DriverInfoResponse>> Update(int id, UpdateDriverInfoCommand command)
        {
            Logger.LogDebug("Updating driver Info");
            if (id != command.Id)
            {
                return BadRequest();
            }
            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}
