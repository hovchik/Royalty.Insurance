using System.Common.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.System.Delta;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.Delta;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Api.Controllers
{
    [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
    [Route("[controller]")]
    [ApiController]
    public class DeltaFinancesController : BaseController<DeltaFinancesController>
    {
        public DeltaFinancesController(ILogger<DeltaFinancesController> logger) : base(logger)
        {
        }

        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(DeltaBillingAccountViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPost("new-billing-account")]
        public async Task<ActionResult<DeltaBillingAccountViewModel>> CreateBillingAccount(NewBillingAccountCommand request)
        {
            Logger.LogDebug("Create new billing account");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(PremiumEndorsementViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPost("premium-endorsement")]
        public async Task<ActionResult<PremiumEndorsementViewModel>> PremiumEndorsement(PremiumEndorsementCommand request)
        {
            Logger.LogDebug("Create premium endorsement");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(PremiumEndorsementViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPost("return-premium-endorsement")]
        public async Task<ActionResult<ReturnPremiumEndorsementViewModel>> ReturnPremiumEndorsement(ReturnPremiumEndorsementCommand request)
        {
            Logger.LogDebug("Return premium endorsement");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(PremiumEndorsementViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPost("policy-cancellation")]
        public async Task<ActionResult<ReturnPremiumEndorsementViewModel>> PolicyCancellation(PolicyCancellationCommand request)
        {
            Logger.LogDebug("Policy cancellation");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(PremiumEndorsementViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPost("policy-reinstatement")]
        public async Task<ActionResult<ReturnPremiumEndorsementViewModel>> PolicyReinstatement(PolicyReinstatementCommand request)
        {
            Logger.LogDebug("Policy Reinstatement");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(PremiumEndorsementViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPost("billing-account-information")]
        public async Task<ActionResult<ReturnPremiumEndorsementViewModel>> BillingAccountInformation(BillingAccountInformationCommand request)
        {
            Logger.LogDebug("Billing account information");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(PremiumEndorsementViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPost("agent-information-change")]
        public async Task<ActionResult<AgentInformationChangeViewModel>> AgentInformationChange(AgentInformationChangeCommand request)
        {
            Logger.LogDebug("Agent information change");
            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}
