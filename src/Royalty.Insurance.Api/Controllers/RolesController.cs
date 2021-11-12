using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.Roles;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Collections.Generic;
using System.Common.Attributes;
using System.Threading.Tasks;

namespace Royalty.Insurance.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : BaseController<RolesController>
    {
        public RolesController(ILogger<RolesController> logger) : base(logger)
        {
        }

        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<RoleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<List<RoleResponse>>> Get()
        {
            Logger.LogDebug("Getting Roles");
            var response = await Mediator.Send(new GetRoleQuery());

            return Ok(response);
        }
    }
}
