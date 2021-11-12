using System.Collections.Generic;
using System.Common.Attributes;
using System.Common.Constants;
using System.Common.Exceptions;
using System.Common.Storage;
using System.Linq;
using System.Threading.Tasks;
using Core.System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.Api.Messaging;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.Files;
using Royalty.Insurance.BusinessLayer.Files.Queries;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Api.Controllers
{
    [AuthorizeByRoles(true)]
    [Route("me/[controller]")]
    [ApiController]
    public class FilesController : BaseController<AccountsController>
    {
        private readonly IStorageManager _storageManager;
        private readonly IApplicationDbContext _context;
        private readonly IExpiryQueryParameterCreator _expiryQueryParameter;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHubContext<MessageHub> _hubContext;

        public FilesController(ILogger<AccountsController> logger, IStorageManager storageManager, IApplicationDbContext context, IExpiryQueryParameterCreator expiryQueryParameter, ICurrentUserService currentUserService, IHubContext<MessageHub> hubContext) : base(logger)
        {
            _storageManager = storageManager;
            _context = context;
            _expiryQueryParameter = expiryQueryParameter;
            _currentUserService = currentUserService;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Get file formats
        /// </summary>
        /// <response code="200">Returns OK status and user files in garage </response>
        /// <response code="404">Failed to get</response>
        /// <returns></returns>
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(PaginationResponse<UserFileResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("format")]
        public async Task<ActionResult<PaginationResponse<UserFileResponse>>> Get()
        {
            Logger.LogDebug("Getting file format");
            return Ok(await _context.FileFormats.Select(item => new FileFormatResponse
            {
                Id = item.Id,
                Name = item.Name,
                CodeType = item.CodeType
            })
                .ToListAsync());
        }

        /// <summary>
        /// Get my files
        /// </summary>
        /// <response code="200">Returns OK status and user files in garage </response>
        /// <response code="404">Failed to get</response>
        /// <returns></returns>
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(PaginationResponse<UserFileResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<PaginationResponse<UserFileResponse>>> Get([FromQuery] GetFilesWithPaginationQuery query)
        {
            Logger.LogDebug("Getting user files");
            return await Mediator.Send(query);
        }


        /// <summary>
        /// Check if file name exists
        /// </summary>
        /// <response code="200">Returns OK status and user files in garage </response>
        /// <response code="404">Failed to get</response>
        /// <returns></returns>
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("check-file-name")]
        public async Task<ActionResult<BaseResponse<bool>>> CheckFileExists([FromQuery] CheckUserFileExistsQuery query)
        {
            Logger.LogDebug("Check if user file exists");
            return await Mediator.Send(query);
        }

        /// <summary>
        /// Forward to messaging
        /// </summary>
        /// <response code="200">Returns OK status and user files in garage </response>
        /// <response code="404">Failed to get</response>
        /// <returns></returns>
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("file-forward/{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> ForwardFile(int id, int groupId)
        {
            Logger.LogDebug("Check if user file exists");
            var response = await Mediator.Send(new FileForwardToMessageCommand { Id = id, UserId = _currentUserService.UserId, GroupId = groupId });
            ReceiveMessageResponse sender = new ReceiveMessageResponse(response.MessageId, response.Content, response.GroupTypeId,
                response.GroupId, _currentUserService.UserId, response.SentDate, response.AttachmentsPath, response.MessageAuthorId);

            await _hubContext.Clients.Group(groupId.ToString())
                .SendAsync(nameof(IMessageClient.OnFileMessageReceive), sender);

            return Ok();
        }

        /// <summary>
        /// Get my files
        /// </summary>
        /// <response code="200">Returns OK status and user files in garage </response>
        /// <response code="404">Failed to get</response>
        /// <returns></returns>
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Logger.LogDebug("Deleting user files");
            await Mediator.Send(new DeleteFileCommand { Id = id });
            return NoContent();
        }


        [Consumes(SystemConstants.MultimediaType)]
        [ProducesResponseType(typeof(UserFileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPost("Upload")]
        public async Task<ActionResult<UserFileResponse>> Upload([FromForm] UploadFileCommand command)
        {
            Logger.LogDebug("Uploading user file");
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Update assign - re-assign
        /// </summary>
        /// /// <param name="id">Id</param>
        /// <param name="command">command</param>
        /// <returns></returns>
        [AuthorizeByRoles(nameof(UserRoleType.Assistant), nameof(UserRoleType.Agent))]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(UserFileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserFileResponse>> Update(int id, UpdateFileCommand command)
        {
            Logger.LogDebug("Update assigned Insured");
            if (id != command.Id)
            {
                return BadRequest();
            }
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [AllowAnonymous]
        [Consumes(SystemConstants.MultimediaType)]
        [ProducesResponseType(typeof(IFormFile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{fileName}")]
        public async Task<ActionResult<IFormFile>> GetFile(string fileName, [FromQuery] long expiry, [FromQuery] string hash, [FromQuery] int id)
        {
            Logger.LogDebug($"Getting user file: {id}");
            if (!_expiryQueryParameter.IsValidRequest(expiry, hash))
            {
                throw new RestApiResponseException(StatusCodes.Status404NotFound, ResourceCommonMessage.ResourceDoesNotExistsOrExpired);
            }
            var response = await _storageManager.ReadAsync(Constants.Garage, id.ToString(), fileName);
            Logger.LogTrace($"Response type: {response.ContentType}");
            Response.Headers.Add("Accept-Ranges", "bytes");//convince chrome that video is streamed by chunk

            return File(response.DataInBytes, response.ContentType);
        }


        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MultimediaType)]
        [ProducesResponseType(typeof(IFormFile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("user-file/{fileName}")]
        public async Task<ActionResult<IFormFile>> GetFileStream(string fileName, int id)
        {
            Logger.LogDebug($"Getting file, file name is{fileName}");
            var response = await _storageManager.ReadAsync(Constants.Garage, id.ToString(), fileName);

            return File(response.DataInBytes, response.ContentType);
        }
    }
}
