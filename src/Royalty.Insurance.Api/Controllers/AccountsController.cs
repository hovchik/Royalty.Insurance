using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Attributes;
using System.Common.Authentication.Models;

using System.Threading.Tasks;
using Royalty.Insurance.BusinessLayer.Account;
using Royalty.Insurance.BusinessLayer.Account.Commands.RegisterTrustedDevice;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Api.Controllers
{
    [Produces(SystemConstants.MediaType)]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : BaseController<AccountsController>
    {
        
        public AccountsController(ILogger<AccountsController> logger) : base(logger)
        {
        }

        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status428PreconditionRequired)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status302Found)]
        [AllowAnonymous]
        [Consumes(SystemConstants.MediaType)]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginCommand request)
        {
            Logger.LogDebug("Login user");
            request.UserIpAddress = GetUserIpAddress();

            return Ok(await Mediator.Send(request));
        }

        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [Consumes(SystemConstants.MediaType)]
        [HttpPost("twofactorlogin")]
        public async Task<IActionResult> TwoFactorLoginAsync(TwoFactorLoginCommand request)
        {
            request.UserIpAddress = GetUserIpAddress();

            return Ok(await Mediator.Send(request));
        }

        [ProducesResponseType(typeof(TotpResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [Consumes(SystemConstants.MediaType)]
        [HttpPost("setupAuthenticator")]
        public async Task<ActionResult<TotpResult>> SetupAuthenticator(SetupAuthenticatorCommand request)
        {
            return Ok(await Mediator.Send(request));
        }


        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        [Consumes(SystemConstants.MediaType)]
        [HttpPost("trusteddevice")]
        public async Task<ActionResult<TotpResult>> SetupAuthenticator(RegisterTrustedDeviceCommand request)
        {
            await Mediator.Send(request);
            return Ok();
        }

        [ProducesResponseType(typeof(ResultResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        [Consumes(SystemConstants.MediaType)]
        [HttpPost("verifytwofactor")]
        public async Task<ActionResult<ResultResponse>> TwoFactorAuthentication(SetTwoFactorEnabledCommand request)
        {
            if (await Mediator.Send(request))
            {
                return Ok(new ResultResponse(ResourceCommonMessage.AuthenticatorVerifiedMessage, Status.Success));
            }

            return BadRequest(new ResultResponse(ResourceCommonMessage.AuthenticatorFailedMessage, Status.Error));
        }

        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [Authorize]
        [Consumes(SystemConstants.MediaType)]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutCommand request)
        {
            await Mediator.Send(request);

            return Ok();
        }

        /// <summary>
        /// Activate a User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT users/activation
        ///     {
        ///         "email": "email",
        ///         "password": "string",
        ///         "NewPassword": "string",
        ///     }
        ///
        /// </remarks>
        /// <param name="request">User activation request</param>
        /// <returns>Activate User</returns>
        /// <response code="200">Returns OK status</response>
        /// <response code="400">Failed to activate</response>
        [AllowAnonymous]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPut("activation")]
        public async Task<IActionResult> UserActivation([FromBody] ActivateUserCommand request)
        {
            Logger.LogDebug("Activating user");
            if (await Mediator.Send(request))
            {
                return Ok();
            }

            return BadRequest(new ApiErrorResponse("User activation failed"));
        }

        /// <summary>
        /// User send forget password request
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT users/forgetpassword
        ///     {
        ///         "Email": "Email",
        ///     }
        ///
        /// </remarks>
        /// <param name="request">User forget password request</param>
        /// <returns>Forget password request</returns>
        /// <response code="200">Returns OK status</response>
        /// <response code="400">Failed to activate</response>
        [AllowAnonymous]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPut("forgetpassword")]
        public async Task<IActionResult> ForgetPasswordAsync([FromBody] ForgetPasswordCommand request)
        {
            Logger.LogDebug("user forget password request");
            if (await Mediator.Send(request))
            {
                return Ok();
            }

            return BadRequest(new ApiErrorResponse("User forget failed"));
        }

        /// <summary>
        /// User send reset password request
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT users/resetpassword
        ///     {
        ///         "Password": "password",
        ///         "Code" : "Code"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">User forget password request</param>
        /// <returns>Reset the password</returns>
        /// <response code="200">Returns OK status</response>
        /// <response code="400">Failed to reset</response>
        [AllowAnonymous]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPut("resetpassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordCommand request)
        {
            Logger.LogDebug("user reset password request");
            if (await Mediator.Send(request))
            {
                return Ok();
            }

            return BadRequest(new ApiErrorResponse("User reset failed"));
        }

        /// <summary>
        /// User send change password request
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT users/changepassword
        ///     {
        ///         "Password": "password",
        ///         "Code" : "Code"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">User change password request</param>
        /// <returns>Change the password</returns>
        /// <response code="200">Returns OK status</response>
        /// <response code="400">Failed to reset</response>
        [Authorize]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPut("changepassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordCommand request)
        {
            Logger.LogDebug("user change password request");
            await Mediator.Send(request);

            return Ok();
        }

        /// <summary>
        /// Deactivate  User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT users/deactivate
        ///     {
        ///         "UserId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Deactivate User </param>
        /// <returns>Deactivate the user</returns>
        /// <response code="200">Returns OK status</response>
        /// <response code="400">Failed to reset</response>
        [AuthorizeByRoles]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPut("deactivate")]
        public async Task<IActionResult> DeactivateAsync([FromBody] DeactivateUserCommand request)
        {
            Logger.LogDebug("Deactivate user ");
            if (await Mediator.Send(request))
            {
                return Ok();
            }

            return BadRequest(new ApiErrorResponse("Deactivate User failed"));
        }

        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [Consumes(SystemConstants.MediaType)]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand request)
        {
            request.UserIpAddress = GetUserIpAddress();
            request.ExpiredAccessToken = Request.Headers[SystemConstants.Authorization].ToString();
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Block user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [AuthorizeByRoles(nameof(UserRoleType.SuperAdmin))]
        [Consumes(SystemConstants.MediaType)]
        [HttpPost("block")]
        public async Task<IActionResult> Block(BlockAccountCommand request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }


        /// <summary>
        /// Unblock user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [AuthorizeByRoles(nameof(UserRoleType.SuperAdmin))]
        [Consumes(SystemConstants.MediaType)]
        [HttpPost("unblock")]
        public async Task<IActionResult> UnBlock(UnBlockAccountCommand request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        #region Private methods

        private string GetUserIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else if (HttpContext.Connection.RemoteIpAddress != null)
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            return null;
        }

        #endregion

    }
}
