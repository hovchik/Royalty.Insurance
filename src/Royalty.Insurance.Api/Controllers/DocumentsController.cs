using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Collections.Generic;
using System.Common.Attributes;
using System.Common.Constants;
using System.Common.Storage;
using System.Common.Storage.Response;
using System.Threading.Tasks;
using Royalty.Insurance.BusinessLayer.Documents;

namespace Royalty.Insurance.Api.Controllers
{
    [AuthorizeByRoles(true)]
    [Route("[controller]")]
    [ApiController]
    public class DocumentsController : BaseController<DocumentsController>
    {
        private readonly IStorageManager _storageManager;

        public DocumentsController(ILogger<DocumentsController> logger, IStorageManager storageManager) : base(logger)
        {
            _storageManager = storageManager;
        }

        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MultimediaType)]
        [ProducesResponseType(typeof(DocumentListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPost("{insuredsId}")]
        public async Task<ActionResult<DocumentListViewModel>> CreateFiles([FromForm] List<IFormFile> files, int? insuredsId)
        {
            Logger.LogDebug("Uploading insured Documents files");
            return Ok(await Mediator.Send(new UploadDocumentCommand {Files = files, InsuredId = insuredsId}));
        }

        /// <summary>
        /// Upload document into sharepoint.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MultimediaType)]
        [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
        [HttpPost("share-point")]
        public async Task<ActionResult<DocumentResponse>> UploadIntoSharePoint([FromForm] UploadDocumentIntoSharePointCommand command)
        {
            Logger.LogDebug("Uploading into share point.");
            return Ok(await Mediator.Send(command));
        }

        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
        [HttpPost("assign-insured")]
        public async Task<ActionResult<DocumentResponse>> AssignInsured(UpdateDocumentCommand command)
        {
            Logger.LogDebug("Assign insured.");

            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Get documents.
        /// </summary>
        /// <response code="200">Returns OK status and documents</response>
        /// <response code="404">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(DocumentPaginationViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<DocumentPaginationViewModel>> Get([FromQuery]GetDocumentsQuery request)
        {
            Logger.LogDebug("Getting user documents");

            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Get documents by type
        /// </summary>
        /// <response code="200">Returns OK status and documents</response>
        /// <response code="404">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(DocumentListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("templates")]
        public async Task<ActionResult<DocumentListViewModel>> Get()
        {
            Logger.LogDebug("Getting document templates");

            return Ok(await Mediator.Send(new GetDocumentTemplatesQuery()));
        }

        /// <summary>
        /// Get documents by insured
        /// </summary>
        /// <response code="200">Returns OK status and documents</response>
        /// <response code="404">Failed to get</response>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MediaType)]
        [ProducesResponseType(typeof(PaginationResponse<DocumentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("insuredId/{pageIndex?}/{pageSize?}")]
        public async Task<ActionResult<PaginationResponse<DocumentResponse>>> Get(int insuredId, int pageIndex = 1, int pageSize = 30)
        {
            Logger.LogDebug("Getting user documents");

            return Ok(await Mediator.Send(new GetByInsuredId {PageSize =  pageSize, PageIndex = pageIndex, InsuredId =  insuredId}));
        }

        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MultimediaType)]
        [ProducesResponseType(typeof(IFormFile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("fileName={fileName}&&insuredId={insuredId}")]
        public async Task<ActionResult<IFormFile>> Download(string fileName, int insuredId)
        {
            Logger.LogDebug("Getting files from documents by name and folder name (dot number)");
            var response = await _storageManager.ReadAsync(Constants.Documents, insuredId.ToString(), fileName);

            return File(response.DataInBytes, response.ContentType);
        }

        [AuthorizeByRoles]
        [Consumes(SystemConstants.MultimediaType)]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpDelete]
        public async Task<ActionResult<DeleteResponse>> Delete([FromQuery]DeleteDocumentCommand request)
        {
            Logger.LogDebug("delete files from documents by name and folder name (dot number)");

            await Mediator.Send(request);

            return NoContent();
        }

        /// <summary>
        /// Generate document and upload into sharepoint
        /// </summary>
        /// <param name="insuredId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [AuthorizeByRoles(true)]
        [Consumes(SystemConstants.MultimediaType)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpPost("generate/{insuredId}/{templateId}")]
        public async Task<ActionResult<DocumentResponse>> GenerateDocumentFromTemplate(int insuredId, int templateId)
        {
            Logger.LogDebug("Getting files from documents by name and folder name (dot number)");


            return Ok(await Mediator.Send(new GenerateDocumentFromTemplateCommand
                {InsuredId = insuredId, TemplateId = templateId}));
        }
    }
}
