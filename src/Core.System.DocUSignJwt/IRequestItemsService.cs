
using System.IO;
using Core.System.DocUSignJwt.Model;

namespace Core.System.DocUSignJwt
{
    public interface IRequestItemsService
    {
        string EnvelopeId { get; set; }
        string DocumentId { get; set; }
        string Status { get; set; }
        string GetDocumentSignerRedirectUrl(DocumentSigner agent, DocumentSigner insured);

        Stream DownloadDocument(string envelopeId, string documentId);
    }
}
