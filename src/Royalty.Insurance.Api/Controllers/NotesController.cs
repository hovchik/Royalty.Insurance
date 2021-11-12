using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.Notes;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;
using System;
using System.Common.Attributes;
using System.Threading.Tasks;

namespace Royalty.Insurance.Api.Controllers
{
    [Produces(SystemConstants.MediaType)]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class NotesController : BaseController<NotesController>
    {
        public NotesController(ILogger<NotesController> logger) : base(logger)
        {
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<NoteResponse>> Create(CreateNoteCommand request)
        {
            Logger.LogDebug("Create Note");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteNoteCommand request)
        {
            Logger.LogDebug("Delete Note");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult<NoteResponse>> Update(UpdateNoteCommand request)
        {
            Logger.LogDebug("Update Note");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteResponse>> GetByNoteId(int id)
        {
            Logger.LogDebug("Get Note by Id");
            var response = await Mediator.Send(new GetNoteByIdQuery { Id = id });

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(NoteResponseListView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("Insured/{insuredId}")]
        public async Task<ActionResult<NoteResponseListView>> GetByInsuredId(int insuredId)
        {
            Logger.LogDebug("Get Note by Insured Id");
            var response = await Mediator.Send(new GetByInsuredIdQuery { InsuredId = insuredId });

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(NoteResponseListView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{pageIndex}/{pageSize}")]
        public async Task<ActionResult<NoteResponseListView>> Get(int pageIndex, int pageSize)
        {
            Logger.LogDebug("Get all Notes");
            var response = await Mediator.Send(new GetNotesQuery { PageIndex = pageIndex, PageSize = pageSize });

            return Ok(response);
        }

        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(NoteResponseListView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{pageIndex}/{pageSize}/{from}/{to}")]
        public async Task<ActionResult<NoteResponseListView>> GetByDateRange(int pageIndex, int pageSize, DateTime from, DateTime to)
        {
            Logger.LogDebug("Get all Notes");
            var response = await Mediator.Send(new GetNotesByDateRangeQuery { PageIndex = pageIndex, PageSize = pageSize, From = from, To = to });

            return Ok(response);
        }
    }
}
