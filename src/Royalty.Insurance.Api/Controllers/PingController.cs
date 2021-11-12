using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.Api.Controllers
{
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class PingController : BaseController<PingController>
    {
        private readonly IApplicationDbContext _context;

        public PingController(ILogger<PingController> logger, IApplicationDbContext context):base(logger)
        {
            _context = context;
        }

        /// <summary>
        /// Ping to check API is up
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Ping()
        {
            Logger.LogDebug("Starting Ping");
            return Ok(new PingResponse(true));
        }

        [HttpGet("deep")]
        public async Task<IActionResult> DeepPing()
        {
            Logger.LogDebug("Starting Deep Ping");
            string dbVersion = await _context.DatabaseVersions.Select(item => item.DbVersion).FirstOrDefaultAsync();
            // To Implement deep ping to database
            return Ok(new DeepPingResponse(!string.IsNullOrWhiteSpace(dbVersion), dbVersion));
        }
    }
}
