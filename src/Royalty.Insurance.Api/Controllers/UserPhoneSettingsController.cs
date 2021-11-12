using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.UserPhoneSettings;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;
using System.Collections.Generic;
using System.Common.Attributes;
using System.Threading.Tasks;

namespace Royalty.Insurance.Api.Controllers
{
    [Produces(SystemConstants.MediaType)]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserPhoneSettingsController : BaseController<UserPhoneSettingsController>
    {
        public UserPhoneSettingsController(ILogger<UserPhoneSettingsController> logger) : base(logger)
        {
        }

        [AuthorizeByRoles(nameof(UserRoleType.IT))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(UserPhoneResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<UserPhoneResponse>> Create(CreateUserPhoneCommand request)
        {
            Logger.LogDebug("Creating User Phone info");
            //to do hard coded user id, update when login is implemented
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.IT))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(UserPhoneResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public async Task<ActionResult<UserPhoneResponse>> Update(int id, UpdateUserPhoneCommand request)
        {
            Logger.LogDebug("Updating user phone");
            if (id != request.Request.Id)
            {
                return BadRequest();
            }
            //to do hard coded user id, update when login is implemented
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<UserPhoneResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<UserPhoneResponse>> Get()
        {
            Logger.LogDebug("Getting Insureds");
            var response = await Mediator.Send(new GetUserPhoneQuery());

            return Ok(response);
        }

        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(UserPhoneResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPhoneResponse>> Get(int id)
        {
            Logger.LogDebug("Getting Insured");
            var response = await Mediator.Send(new GetUserPhoneByIdQuery { Id = id });

            return Ok(response);
        }
    }
}
