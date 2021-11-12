using System;
using System.Common.Exceptions;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Core.System.DocumentManagement.GenerateResolver;
using Core.System.DocumentManagement.Mediator;
using Core.System.MicrosoftGraph;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Core.System.DocumentManagement;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Domain;
using Royalty.Insurance.BusinessLayer.Extensions;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class GenerateDocumentFromTemplateCommandHandler : IRequestHandler<GenerateDocumentFromTemplateCommand, DocumentResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDocumentMapperService _mapper;
        private readonly IDownloadDocument _downloadDocument;
        private readonly IUploadDocument _uploadDocument;
        private readonly ICurrentUserService _currentUser;
        private readonly IGenerateDocumentMediator _documentMediator;

        public GenerateDocumentFromTemplateCommandHandler(IApplicationDbContext context, IDocumentMapperService mapper, IDownloadDocument downloadDocument, ICurrentUserService currentUser, IUploadDocument uploadDocument, IGenerateDocumentMediator documentMediator)
        {
            _context = context;
            _mapper = mapper;
            _downloadDocument = downloadDocument;
            _currentUser = currentUser;
            _uploadDocument = uploadDocument;
            _documentMediator = documentMediator;
        }

        public async Task<DocumentResponse> Handle(GenerateDocumentFromTemplateCommand request, CancellationToken cancellationToken)
        {
            var template =
                await _context.Documents.FirstOrDefaultAsync(item => !item.IsDeleted && item.Id.Equals(request.TemplateId),
                    cancellationToken);
            if (template == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.TemplateNotFound);
            }
            var insured =
                await _context.Insureds
                    .Include(x => x.MailingCity)
                    .Include(x => x.MailingState)
                    .Include(x => x.MailingZipCode)
                    .FirstOrDefaultAsync(item => item.Id.Equals(request.InsuredId), cancellationToken);
            if (template == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.InsuredNotFound);
            }

            var stream = await _downloadDocument.Handle(
                new DownloadDocumentRequest {DriveItemId = template.DriveItemId, GroupId = template.GroupId},
                cancellationToken);
            var documentName =
                    $"{insured.MailingName}-{template.DocumentName.Substring(0, template.DocumentName.Length - 5)}-{DateTime.Now.Ticks}.docx";
            var uploadDocument = await _uploadDocument.Handle(
                new UploadDocumentRequest
                {
                    FileName = documentName,

                    DocumentStream = await _documentMediator.SendAsync(await GetResolver(stream, template.DocumentName, insured, cancellationToken), cancellationToken),
                    GroupId = template.GroupId
                }, cancellationToken);
            var entity = new Document {CreatedBy = _currentUser.UserId};
            _mapper.UpdateEntity(entity, request.InsuredId, documentName, uploadDocument, _currentUser.UserId);
            await _context.Documents.AddAsync(entity, cancellationToken);

            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }

        private async Task<IGenerateResolver> GetResolver(Stream templateStream, string templateName, Insured insured, CancellationToken cancellationToken)//todo change to object
        {
            switch (templateName)
            {
                case "CC Authorization.dotx":
                case "Fleet Transportation.dotx":
                    var insuredName = !string.IsNullOrEmpty(insured.Dba) ? $"{insured.MailingName}:{insured.Dba}" : insured.MailingName;
                    return new CreditCArdAuthorizationResolver(new InsuredFullNameRequest(){Name = insuredName }, templateStream);
                case "Accord 101.dotx":
                    return new Accord101Resolver(await _context.GetAccord101FormData(insured, _currentUser.UserFullName, cancellationToken), templateStream);
                case "Acord 25.dotx":
                    return new Accord25Resolver(await _context.GetAccord25FormData(insured, _currentUser.UserEmail, cancellationToken), templateStream);
                case "Acord 36.dotx":
                    return new Accord36Resolver(
                        await _context.GetAccord36FormData(insured, _currentUser.UserEmail, _currentUser.UserFullName,
                            cancellationToken), templateStream);
                default:
                    throw new ArgumentOutOfRangeException(nameof(templateName));
            }
        }

    }
}
