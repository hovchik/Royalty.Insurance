using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentTaskTypesController :  BaseController<AgentTaskStatusesController>
    {
        private readonly IApplicationDbContext _context;

        public AgentTaskTypesController(ILogger<AgentTaskStatusesController> logger, IApplicationDbContext context) : base(logger)
        {
            _context = context;
        }

        /// <summary>
        /// Get Agent Task types
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Produces(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<AgentTaskTypeResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<List<AgentTaskTypeResponse>>> GetAsync()
        {
            Logger.LogDebug("Getting  Agent Task Type");

            var response = await _context.AgentTaskTypes.Select(item => new AgentTaskTypeResponse
            {
                Id = item.Id,
                Name = item.Name
            }).ToListAsync();

            return Ok(response);
        }
    }
}
