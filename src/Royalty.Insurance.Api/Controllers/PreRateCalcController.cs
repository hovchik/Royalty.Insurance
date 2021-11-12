using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.ILogic;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;
using System.Common.Attributes;
using System.Threading.Tasks;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.BusinessLayer.ProRateCalculator;

namespace Royalty.Insurance.Api.Controllers
{
    [Produces(SystemConstants.MediaType)]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PreRateCalcController : BaseController<PreRateCalcController>
    {
        public PreRateCalcController(ILogger<PreRateCalcController> logger) : base(logger)
        {
        }
        /// <summary>
        /// {
        ///     "coverages": {
        ///         "1": 8486,
        ///         "5": 30000,
        ///         "3": 1765
        ///     },
        ///     "from": "2021-03-11T16:00:44.107Z",
        ///     "to": "2021-10-17T16:00:44.107Z",
        ///     "percentage": 4.1,
        ///     "brokerfee":300
        /// }
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(ProRateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost("calc")]
        public async Task<ActionResult<ProRateResponse>> Calc(GetProRateCalcQuery request)
        {
            Logger.LogDebug("Getting Pro rate");
            var response =  await Mediator.Send(request);

            return Ok(response);
        }
    }
}
