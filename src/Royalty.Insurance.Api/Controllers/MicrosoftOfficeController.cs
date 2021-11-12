using System.Collections.Generic;
using System.Common.Attributes;
using System.Common.Extensions;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.MicrosoftOffice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.MicrosoftOffice;
using Royalty.Insurance.BusinessLayer.MicrosoftOffice.Commands;
using Royalty.Insurance.BusinessLayer.MicrosoftOffice.Queries;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.Api.Controllers
{
    [AuthorizeByRoles(true)]
    [Route("[controller]")]
    [Produces(SystemConstants.MediaType)]
    [ApiController]
    public class MicrosoftOfficeController : BaseController<MicrosoftOfficeController>
    {

        public MicrosoftOfficeController(ILogger<MicrosoftOfficeController> logger) : base(logger)
        {
        }

        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(MicrosoftOfficeUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("user")]
        public async Task<ActionResult<List<MicrosoftOfficeUserResponse>>> Get()
        {
            Logger.LogDebug("Getting User");
            var response = await Mediator.Send(new GetUserQuery {Email = User.UserEmail()});

            return Ok(response);
        }

        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(MicrosoftOfficeUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("folders")]
        public async Task<ActionResult<List<MicrosoftOfficeUserResponse>>> GetUserEmailFolders()
        {
            Logger.LogDebug("Getting User Email Folders");
            var response = await Mediator.Send(new GetMailFoldersQuery { Email = User.UserEmail() });

            return Ok(response);
        }

        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(MicrosoftOfficeMessageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("Conversations/{id}")]
        public async Task<ActionResult<List<MicrosoftOfficeMessageResponse>>> GetConversations(string id)
        {
            Logger.LogDebug($"Getting mail conversation, conversation id {id}");

            var response = await Mediator.Send(new GetConversationQuery { Email = User.UserEmail(), ConversationId = id});

            return Ok(response);
        }

        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(MicrosoftOfficeMessageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet("emailcontent/{folderId}")]
        public async Task<ActionResult<List<MicrosoftOfficeMessageResponse>>> GetFolderContent(string folderId)
        {
            Logger.LogDebug($"Getting mail conversation, folder Id '{folderId}'");

            var response = await Mediator.Send(new GetFolderContentQuery { Email = User.UserEmail(), ParentFolderId = folderId });

            return Ok(response);
        }
        
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult> SendingEmail(MicrosoftOfficeMessageRequest request)
        {
            Logger.LogDebug("Sending mail");
            SendEmailCommand command = (SendEmailCommand)request;
            await Mediator.Send(command);

            return Ok();
        }
    }
}
