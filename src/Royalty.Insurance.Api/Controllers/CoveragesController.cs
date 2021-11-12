using System.Collections.Generic;
using System.Common.Attributes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.Coverages;
using Royalty.Insurance.BusinessLayer.CoverageTypes;
using Royalty.Insurance.BusinessLayer.ILogic;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Api.Controllers
{
    [Authorize]
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class CoveragesController : BaseController<CoveragesController>
    {

        public CoveragesController(ILogger<CoveragesController> logger) : base(logger)
        {
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(CoverageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CoverageResponse>> Get(int id)
        {
            Logger.LogDebug("Getting Coverages");
            var response = await Mediator.Send(new GetCoverageByIdQuery { Id = id });

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<CoverageResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<List<CoverageResponse>>> Get()
        {
            Logger.LogDebug("Getting Coverages");
            var response = await Mediator.Send(new GetCoveragesQuery());

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(CoverageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public async Task<ActionResult<CoverageResponse>> Update(int id, CoverageRequest request)
        {
            Logger.LogDebug("Updating Coverages");
            var response = await Mediator.Send(new UpdateCoverageCommand { Request = request, Id = id });

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(CoverageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<CoverageResponse>> Create(CoverageRequest request)
        {
            Logger.LogDebug("Creating Coverages");
            var response = await Mediator.Send(new CreateCoverageCommand { Request = request });

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<CoverageTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("CoverageType")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60 * 60 * 24)]//talk to front
        public async Task<ActionResult<List<CoverageTypeResponse>>> GetCoverageTypes()
        {
            Logger.LogDebug("Creating Coverages");
            var response = await Mediator.Send(new GetCoverageTypesQuery());

            return Ok(response);
        }
    }
}
