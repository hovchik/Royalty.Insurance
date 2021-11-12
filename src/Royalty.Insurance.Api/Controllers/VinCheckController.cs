using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.ILogic;
using Royalty.Insurance.BusinessLayer.VinCheck;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Royalty.Insurance.Api.Controllers
{
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class VinCheckController : BaseController<VinCheckController>
    {
        public VinCheckController(ILogger<VinCheckController> logger) : base(logger)
        {
        }

        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<VinCheckResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<VinCheckResponse>>> Get(string vin)
        {
            var response = await Mediator.Send(new GetVinInfoQuery { VinNumber = vin });

            return Ok(response);
        }
    }
}
