using Domain;
using Royalty.Insurance.Proxy.Response;
using System;
using System.Common.Storage.Response;
using System.Linq.Expressions;
using Core.System.MicrosoftGraph;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class DocumentMapperService : IDocumentMapperService
    {
        public Expression<Func<Document, DocumentResponse>> MapResponse => entity => new DocumentResponse
        {
            Id = entity.Id,
            Path = entity.Path,
            TemplateId = entity.DocumentTypeId,
            DocumentName = entity.DocumentName,
            InsuredsId = entity.InsuredId,
            UserId = entity.CreatedBy,
            CreatedDatetime = entity.CreateDatetimeUtc
        };

        public void UpdateEntity(Document entity, string fileName, int? insuredId)
        {
            entity.Path = fileName;
            entity.DocumentName = fileName;
            entity.InsuredId = insuredId;
            entity.DocumentTypeId = (byte)DocumentTypeCode.StorageUploaded;
            entity.CreateDatetimeUtc = DateTime.UtcNow;
        }

        public void UpdateEntity(Document entity, DeleteResponse response)
        {
            entity.DeleteDatetimeUtc = response.DeleteDatetime;
            entity.IsDeleted = true;
            entity.DeletedBy = response.UserId;
        }

        public DocumentResponse UpdateModel(UploadResponse response, Document entity, int userId, int? insuredId)
        {
            return new DocumentResponse
            {
                UserId = userId,
                InsuredsId = insuredId,
                CreatedDatetime = response.LastModifiedDate,
                Path = response.FileName,
                DocumentName = response.FileName
            };
        }

        public void UpdateEntity(Document entity, int? insuredId, string documentName, UploadDocumentResponse response, int userId)
        {
            entity.DocumentTypeId = insuredId.HasValue ? (byte)DocumentTypeCode.GeneratedDocuments : (byte)DocumentTypeCode.SharepointShared;
            entity.InsuredId = insuredId;
            entity.Path = response.Path;
            entity.GroupId = response.GroupId;
            entity.DriveItemId = response.DriveItemId;
            entity.DocumentName = documentName;
            entity.CreateDatetimeUtc = DateTime.UtcNow;
            entity.UpdatedBy = userId;
        }
    }
}