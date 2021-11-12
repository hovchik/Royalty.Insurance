using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.Cargoes.Queries;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;
using System.Common.Attributes;
using System.Threading.Tasks;
using Royalty.Insurance.BusinessLayer.Cargoes;

namespace Royalty.Insurance.Api.Controllers
{
    [Authorize]
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class CargoController : BaseController<CargoController>
    {
        public CargoController(ILogger<CargoController> logger) : base(logger)
        {
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(CargoListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet]
        //TODO implement paging and filter by user
        public async Task<ActionResult<CargoListViewModel>> Get()
        {
            Logger.LogDebug("Getting cargo");
            var response = await Mediator.Send(new GetCargoesQuery());

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(CargoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CargoResponse>> Get(int id)
        {
            Logger.LogDebug($"Getting cargo by id {id}");
            var response = await Mediator.Send(new GetCargoesByIdQuery { CargoId = id });

            return Ok(response);
        }
    }
}
