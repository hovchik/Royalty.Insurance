using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.PhoneBooks;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;
using System.Collections.Generic;
using System.Common.Attributes;
using System.Common.Extensions;
using System.Threading.Tasks;

namespace Royalty.Insurance.Api.Controllers
{
    [Authorize]
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class PhoneBookController : BaseController<PhoneBookController>
    {
        public PhoneBookController(ILogger<PhoneBookController> logger) : base(logger)
        {
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(PhoneBookResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost()]
        public async Task<ActionResult<PhoneBookResponse>> Create(CreatePhoneCommand request)
        {
            Logger.LogDebug("Creating PhoneBook via controller");
            PhoneBookResponse response = await Mediator.Send(request);

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(PhoneBookResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public async Task<ActionResult<PhoneBookResponse>> Update(int id, UpdatePhoneCommand request)
        {
            Logger.LogDebug("Updating PhoneBook via Controller");
            if (id != request.Id)
            {
                return BadRequest();
            }
            PhoneBookResponse response = await Mediator.Send(request);

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<PhoneBookResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<List<PhoneBookResponse>>> Get()
        {
            Logger.LogDebug("Getting PhoneBook for user via Controller");
            var response = await Mediator.Send(new GetPhoneByUserIdQuery());

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<PhoneBookResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<PhoneBookResponse>>> Get(int id)
        {
            Logger.LogDebug("Getting PhoneBook for user via Controller");
            var response = await Mediator.Send(new GetPhonesQuery { Id = id });

            return Ok(response);
        }
    }
}
