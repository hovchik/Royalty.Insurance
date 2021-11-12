using System;
using System.Collections.Generic;
using System.Common.Constants;
using System.Common.Exceptions;
using System.Common.Storage;
using System.Common.Storage.Response;
using System.IO;
using System.Linq;
using System.Net;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Domain;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, DocumentListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDocumentMapperService _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStorageManager _storageManager;

        public UploadDocumentCommandHandler(IApplicationDbContext context, IDocumentMapperService mapper, ICurrentUserService currentUserService, IStorageManager storageManager)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _storageManager = storageManager;
        }

        public async Task<DocumentListViewModel> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            if (request.InsuredId.HasValue)
            {
                var insured =
                    await _context.Insureds.FirstOrDefaultAsync(ins => ins.Id == request.InsuredId, cancellationToken);
                if (insured == null)
                {
                    throw new RestApiResponseException((int) HttpStatusCode.NotFound,
                        ResourceCommonMessage.InsuredNotFound);
                }
            }

            var uploadedDocuments = await UploadedDocuments(request);
            var documents = new List<Document>();
            foreach (var uploadedDocument in uploadedDocuments)
            {
                var entity = new Document { CreatedBy = _currentUserService.UserId, UpdatedBy = _currentUserService.UserId, };
                _mapper.UpdateEntity(entity, uploadedDocument.FileName, request.InsuredId);
                await _context.Documents.AddAsync(entity, cancellationToken);
                documents.Add(entity);
            }

            await _context.SaveChangesAsync(cancellationToken);
            var documentsIds = documents.Select(item => item.Id);

            return new DocumentListViewModel
            {
                Documents = await _context.Documents.Where(item => documentsIds.Contains(item.Id)).Select(_mapper.MapResponse).ToListAsync(cancellationToken)
            };
        }

        #region Private Methods

        private async Task<UploadResponse[]> UploadedDocuments(UploadDocumentCommand request)
        {
            var tasks = new List<Task<UploadResponse>>();
            var batchSize = 10;
            var files = request.Files;
            int numberOfBatches = (int) Math.Ceiling((double) files.Count / batchSize);

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentFiles = files.Skip(i * batchSize).Take(batchSize);
                tasks.AddRange(currentFiles.Select(async file =>
                {
                    var uniqueFileName =
                        $"{file.FileName.Split('.')[^2]}({DateTime.UtcNow.Ticks}){Path.GetExtension(file.FileName)}";
                    return await _storageManager.UploadAsync(file, Constants.Documents,
                        request.InsuredId.ToString(), uniqueFileName);
                }));
            }

            var uploadedDocuments = await Task.WhenAll(tasks);

            return uploadedDocuments;
        }

        #endregion
    }
}
