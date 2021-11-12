using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Common.Attributes;
using System.Common.Extensions;
using System.Common.Storage;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.Api.Messaging;
using Royalty.Insurance.BusinessLayer.Messages;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.Api.Controllers
{
    [AuthorizeByRoles(true)]
    [Produces(SystemConstants.MediaType)]
    [Route("[controller]")]
    [ApiController]
    public class FileUploadsController : BaseController<FileUploadsController>
    {
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly IStorageManager _storageManager;

        public FileUploadsController(ILogger<FileUploadsController> logger, IHubContext<MessageHub> hubContext, IStorageManager storageManager) : base(logger)
        {
            _hubContext = hubContext;
            _storageManager = storageManager;
        }

        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<IActionResult> UploadFiles([FromForm] CreateMessageWithAttachmentCommand request)
        {
            Logger.LogDebug($"Uploading message files, file count is {request.Files.Count}");
            var currentRoute = Url.RouteUrl(RouteData.Values);
            List<Task> tasks = new List<Task>();
            List<string> filePaths = new List<string>();
            foreach (var file in request.Files.Where(item => item.Length > 0))
            {
                var uploadResponse =_storageManager.UploadAsync(file, BlobConstants.MessageFileBlobName, BlobConstants.MessageFileFolderName, file.FileName);
                filePaths.Add($"{currentRoute}/{file.FileName}" );
                tasks.Add(uploadResponse);
            }
            await Task.WhenAll(tasks);
            int userId = User.UserId();
            request.UserId= userId;
            var response = await Mediator.Send(request);
            ReceiveMessageResponse sender = new ReceiveMessageResponse(response.MessageId, response.Content, response.GroupTypeId,
                response.GroupId, userId, response.SentDate, response.AttachmentsPath, response.MessageAuthorId);
            var hubTasks = new List<Task>(2)
            {

                _hubContext.Clients.Group(request.GroupId.ToString())
                    .SendAsync(nameof(IMessageClient.OnFileMessageReceive), sender)
            };
            if (!string.IsNullOrWhiteSpace(sender.Content))
            {
                hubTasks.Add(_hubContext.Clients.Group(request.GroupId.ToString())
                    .SendAsync(nameof(IMessageClient.ReceiveMessage), sender));
            }

            await Task.WhenAll(hubTasks);

            return Ok(filePaths);
        }


        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MultimediaType)]
        [ProducesResponseType(typeof(IFormFile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{fileName}")]
        public async Task<ActionResult<IFormFile>> GetFile(string fileName)
        {
            Logger.LogDebug($"Getting file, file name is{fileName}");
            var response = await _storageManager.ReadAsync(BlobConstants.MessageFileBlobName, BlobConstants.MessageFileFolderName, fileName);

            return File(response.DataInBytes, response.ContentType);
        }

    }
}
