using System;
using System.Common.Storage.Response;
using System.Linq.Expressions;
using Core.System.MicrosoftGraph;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public interface IDocumentMapperService
    {
        void UpdateEntity(Document entity, string fileName, int? insuredId);
        void UpdateEntity(Document entity, DeleteResponse response);
        Expression<Func<Document, DocumentResponse>> MapResponse { get; }
        DocumentResponse UpdateModel(UploadResponse response, Document entity, int userId, int? insuredId);
        void UpdateEntity(Document entity, int? insuredId, string documentName, UploadDocumentResponse response, int userId);
    }
}
