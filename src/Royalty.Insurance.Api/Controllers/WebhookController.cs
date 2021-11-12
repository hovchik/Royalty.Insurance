using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.FlagmanWebHook;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;
using System;
using System.Threading.Tasks;

namespace Royalty.Insurance.Api.Controllers
{
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class WebhookController : BaseController<WebhookController>
    {
        public WebhookController(ILogger<WebhookController> logger) : base(logger)
        {
        }

        [AllowAnonymous]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<bool>> Get([FromQuery] string callename, [FromQuery] string callernumber, [FromQuery] string callid, [FromQuery] string extension2, [FromQuery] string type)
        {
            int.TryParse(extension2, out int extensionNumber);
            var userPhoneId = await Mediator.Send(new GetExtensionOwnerQuery { UserExtensionId = extensionNumber });
            Enum.TryParse(type, true, out CallTypeCode callType);

            CreateCallRecordCommand request = new CreateCallRecordCommand
            {
                CallNumber = callernumber,
                Extension = extensionNumber,
                CallType = callType,
                UserPhoneId = userPhoneId,
                CallId = callid,
                CallerName = callename
            };
            Logger.LogDebug("Getting request from Yealink ");
            var response = await Mediator.Send(request);

            return Ok(response);
        }


    }
}
