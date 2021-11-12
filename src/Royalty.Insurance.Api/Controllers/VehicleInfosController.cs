using System.Collections.Generic;
using System.Common.Attributes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.VehicleInfos;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Api.Controllers
{
    [Authorize]
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class VehicleInfosController : BaseController<VehicleInfosController>
    {

        public VehicleInfosController(ILogger<VehicleInfosController> logger) : base(logger)
        {
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<VehicleInfoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<CoverageResponse>> Get()
        {
            Logger.LogDebug("Getting All Vehicle Info");
            var response = await Mediator.Send(new GetVehicleInfoQuery());

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(VehicleInfoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CoverageResponse>> Get(int id)
        {
            Logger.LogDebug("Getting Vehicle Infos");
            var response = await Mediator.Send(new GetVehicleInfoByIdQuery { Id = id });

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(VehicleInfoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public async Task<ActionResult<VehicleInfoResponse>> Update(int id, UpdateVehicleInfoCommand request)
        {
            Logger.LogDebug("Updating Vehicle Info");
            if (id != request.Id)
            {
                return BadRequest();
            }
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(VehicleInfoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<VehicleInfoResponse>> Create(CreateVehicleInfoCommand request)
        {
            Logger.LogDebug("Creating Vehicle Info");
            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}
