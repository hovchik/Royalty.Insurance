using System.Collections.Generic;
using System.Common.Extensions;
using System.Common.Attributes;
using System.Common.Constants;
using System.Common.Exceptions;
using System.Common.Storage;
using System.Threading.Tasks;
using Core.System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.ILogic;
using Royalty.Insurance.BusinessLayer.Users;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.Api.Controllers
{
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : BaseController<UsersController>
    {
        private readonly IStorageManager _storageManager;
        private readonly IExpiryQueryParameterCreator _expiryQueryParameter;
        private readonly ICurrentUserService _currentUserService;

        public UsersController(ILogger<UsersController> logger, IStorageManager storageManager, IExpiryQueryParameterCreator expiryQueryParameter, ICurrentUserService currentUserService) : base(logger)
        {
            _storageManager = storageManager;
            _expiryQueryParameter = expiryQueryParameter;
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// Create a User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST users
        ///     {
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "email": "string",
        ///         "password": "string",
        ///         "cellPhone": "string",
        ///         "workPhone": "string",
        ///         "homePhone": "string"
        ///         "role": 1
        ///     }
        ///
        ///      Role Types:
        /// 
        ///      SuperAdmin = 1,
        ///      Agent = 2,
        ///      Assistant = 3,
        ///      Underwriter = 4,
        ///      Marketing = 5,
        ///      IT = 6,
        ///      Accounting = 7
        ///  
        ///  </remarks>
        /// 
        /// <param name="request">User request</param>
        /// <returns>A updated project</returns>
        /// <response code="200">Returns OK</response>
        /// <response code="400">Failed to create</response>
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [AuthorizeByRoles]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserProfileCommand request)
        {
            Logger.LogDebug("Creating user");
            if (await Mediator.Send(request))
            {
                return Ok();
            }

            return BadRequest(new ApiErrorResponse("User creation failed"));
        }

        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> Get()
        {
            Logger.LogDebug("Get users list");
            var response = await Mediator.Send(new GetUsersQuery());

            return Ok(response);
        }

        [AuthorizeByRoles]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> Get(int id)
        {
            Logger.LogDebug("Get user by id");
            var response = await Mediator.Send(new GetUserByIdQuery { Id = id });

            return Ok(response);
        }

        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult<UserResponse>> Update(UpdatePersonalInfoCommand request)//ok
        {
            Logger.LogDebug($"Updating user by Id");
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [AuthorizeByRoles]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(UserAdminRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPut("{userId}")]
        public async Task<ActionResult<UserResponse>> Update(int userId, UpdateUserByAdminCommand request)
        {
            Logger.LogDebug($"Updating user by Id");
            if (request.UserId != userId)
            {
                return BadRequest(request);
            }
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MultimediaType)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("UploadFile")]
        public async Task<ActionResult<string>> UploadFile([FromForm] IFormFile file)
        {
            Logger.LogDebug($"Updating user avatar by Id");
            var response = await Mediator.Send(new UploadUserAvatarCommand { File = file, FileContainer = Constants.Avatars });

            return Ok(response);
        }


        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MultimediaType)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpDelete("profile")]
        public async Task<ActionResult> DeleteProfile()
        {
            Logger.LogDebug($"Updating user avatar by Id");
            var response = await Mediator.Send(new DeleteUserProfileCommand { FileContainer = Constants.Avatars, UserId = _currentUserService.UserId });

            return Ok();
        }

        [Consumes(SystemConstants.MultimediaType)]
        [ProducesResponseType(typeof(IFormFile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("File/{avatarName}")]
        public async Task<ActionResult<IFormFile>> GetFile(string avatarName, [FromQuery] long expiry, [FromQuery] string hash, [FromQuery] int id)
        {
            Logger.LogDebug($"Getting user avatar by Id: {id}");
            if (!_expiryQueryParameter.IsValidRequest(expiry, hash))
            {
                throw new RestApiResponseException(StatusCodes.Status404NotFound, ResourceCommonMessage.ResourceDoesNotExistsOrExpired);
            }
            var response = await _storageManager.ReadAsync(Constants.Avatars, id.ToString(), avatarName);
            Logger.LogTrace($"Response type: {response.ContentType}");

            return File(response.DataInBytes, response.ContentType);
        }
    }
}