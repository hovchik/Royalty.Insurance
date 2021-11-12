using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Common.Attributes;
using System.Common.Authentication;
using System.Common.Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.Api.Controllers
{
    [Authorize]
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class AppSettingsController :  BaseController<AppSettingsController>
    {
        private readonly IOptions<JwtTokenConfig> _jwtTokenConfig;
        private readonly IOptions<AppSetting> _appSetting;
        private readonly IOptions<MicrosoftOfficeSetting> _officeSetting;
        private readonly IOptions<EmailSetting> _emailSetting;

        public AppSettingsController(ILogger<AppSettingsController> logger, IOptions<AppSetting> appSetting, 
            IOptions<MicrosoftOfficeSetting> officeSetting,
            IOptions<EmailSetting> emailSetting,
            IOptions<JwtTokenConfig> jwtTokenConfig) : base(logger)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _appSetting = appSetting;
            _officeSetting = officeSetting;
            _emailSetting = emailSetting;
        }

        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                appSetting = _appSetting,
                officeSetting = _officeSetting,
                jwtTokenConfig = _jwtTokenConfig,
                emailSetting = _emailSetting
            });
        }
    }
}
