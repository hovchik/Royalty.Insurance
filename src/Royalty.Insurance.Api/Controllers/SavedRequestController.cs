using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.SavedRequests;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;
using System.Common.Attributes;
using System.Threading.Tasks;

namespace Royalty.Insurance.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SavedRequestController : BaseController<SavedRequestController>
    {
        public SavedRequestController(ILogger<SavedRequestController> logger) : base(logger)
        {
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(SavedRequestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<SavedRequestResponse>> Create(CreateSavedRequestCommand request)
        {
            Logger.LogDebug("Creating Saved request via controller");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(SavedRequestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public async Task<ActionResult<SavedRequestResponse>> Update(int id, UpdateSavedRequestCommand request)
        {
            Logger.LogDebug("Updating Saved Request via Controller");
            if (id != request.Id)
            {
                return BadRequest();
            }
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(PaginationResponse<SavedRequestResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("GetAllForUser/{pageIndex}/{pageSize}")]
        public async Task<ActionResult<PaginationResponse<SavedRequestResponse>>> Get(int pageIndex, int pageSize)
        {
            Logger.LogDebug("Getting Saved Request for user via Controller");
            var response = await Mediator.Send(new GetSavedRequestsByUserIdQuery { PageIndex = pageIndex, PageSize = pageSize });

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(SavedRequestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<SavedRequestResponse>> Get(int id)
        {
            Logger.LogDebug("Getting Saved Request by Id for user via Controller");
            var response = await Mediator.Send(new GetSavedRequestByIdQuery { Id = id });

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            Logger.LogDebug("Deleting Saved Request for user via Controller");
            await Mediator.Send(new DeleteSavedRequestCommand { Id = id });

            return Ok();
        }
    }
}
