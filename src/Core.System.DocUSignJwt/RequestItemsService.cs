using System;
using System.Collections.Generic;
using System.Common.Authentication.Models;
using System.IO;
using System.Linq;
using System.Text;
using Core.System.DocUSignJwt.Extensions;
using Core.System.DocUSignJwt.Model;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using Account = DocuSign.eSign.Client.Auth.OAuth.UserInfo.Account;
using User = Core.System.DocUSignJwt.Model.User;
using DocuSign.eSign.Client.Auth;
using DocuSign.eSign.Model;
using Microsoft.Extensions.Options;

namespace Core.System.DocUSignJwt
{
    public class RequestItemsService : IRequestItemsService
    {
        private readonly ApiClient _apiClient;
        private readonly AppSetting _appSetting;
        private const string StartingView = "tagging";


        public RequestItemsService(IOptions<AppSetting> options)
        {
            _appSetting = options.Value;
            _apiClient = new ApiClient(_appSetting.DocUSignJwt.BasePath);
        }

        public User User { get; private set; }

        public string EnvelopeId { get; set; }
        public string DocumentId { get; set; }
        public string Status { get; set; }

        #region Publuc interface methods

        public string GetDocumentSignerRedirectUrl(DocumentSigner agent, DocumentSigner insured)
        {
            SetUser();
            _apiClient.SetAuthorization(User);
            var envelopesApi = new EnvelopesApi(_apiClient);

            // Step 1. Make the envelope with "created" (draft) status            
            Status = "created";
            //controller2 = new Eg002SigningViaEmailController(Config, RequestItemsService);
            EnvelopeSummary results = MakeEnvelope(agent, insured);
            string envelopeId = results.EnvelopeId;

            // Step 2. create the sender view
            // Call the CreateSenderView API
            // Exceptions will be caught by the calling function
            ReturnUrlRequest viewRequest = new ReturnUrlRequest
            {
                ReturnUrl = _appSetting.DocUSignJwt.ReturnUrl
            };
            ViewUrl viewUrl = envelopesApi.CreateSenderView(User.AccountId, envelopeId, viewRequest);
            // Switch to Recipient and Documents view if requested by the user
            string redirectUrl = viewUrl.Url;
            if ("recipient".Equals(StartingView))
            {
                redirectUrl = redirectUrl.Replace("send=1", "send=0");
            }
            return redirectUrl;
        }

        public Stream DownloadDocument(string envelopeId, string documentId)
        {
            SetUser();
            var envelopesApi = new EnvelopesApi(_apiClient);

            // EnvelopeDocuments::get.
            // Exceptions will be caught by the calling function
            return envelopesApi.GetDocument(User.AccountId, envelopeId, documentId);
        }

        #endregion

        #region Private Methods

        private void SetUser()
        {
            if (User?.ExpireIn != null && User.ExpireIn.Value < DateTime.Now)
            {
                return;
            }
            var scopes = new List<string>
            {
                "signature",
                "impersonation",
            };
            var authToken = _apiClient.RequestJWTUserToken(_appSetting.DocUSignJwt.ClientId,
                _appSetting.DocUSignJwt.ImpersonatedUserId,
                _appSetting.DocUSignJwt.AuthServer,
                Encoding.UTF8.GetBytes(_appSetting.DocUSignJwt.PrivateKey), 1, scopes);
            Account account = GetAccountInfo(authToken);
            User = new User
            {
                Name = account.AccountName,
                AccessToken = authToken.access_token,
                ExpireIn = authToken.expires_in.HasValue ? DateTime.Now.AddSeconds(authToken.expires_in.Value) : (DateTime?)null,
                AccountId = account.AccountId
            };
        }

        private EnvelopeSummary MakeEnvelope(DocumentSigner agent, DocumentSigner insured)
        {
            // The envelope has two recipients.
            // recipient 1 - agent signer
            // recipient 2 - insured signer
            // The envelope will be sent first to the insured signer.
            // After it is signed,sent to agent
            EnvelopeDefinition env = new EnvelopeDefinition
            {
                EmailSubject = "Please sign this document set",
                Documents = new List<Document> {CreateDocument(agent, insured)},
                EmailBlurb = "Note: Please be advised that insurance coverage cannot be added, deleted or otherwise changed until it is confirmed in writing by Royalty Insurance Services Inc"
            };

            // create a signer recipient to sign the document, identified by name and email
            // We're setting the parameters via the object creation
            // routingOrder (lower means earlier) determines the order of deliveries
            // to the recipients. Parallel routing order is supported by using the
            // same integer as the order for two or more recipients.

            Recipients recipients = new Recipients
            {
                Signers = new List<Signer> { CreateSigner(insured, "1"), CreateSigner(agent, "2") },
                CarbonCopies = new List<CarbonCopy>()
            };
            env.Recipients = recipients;
            // Request that the envelope be sent by setting |status| to "sent".
            // To request that the envelope be created as a draft, set to "created"
            env.Status = Status;
            var envelopesApi = new EnvelopesApi(_apiClient);
            EnvelopeSummary results = envelopesApi.CreateEnvelope(User.AccountId, env);
            EnvelopeId = results.EnvelopeId;

            return results;
        }

        private Document CreateDocument(DocumentSigner agent, DocumentSigner insured)
        {
            // Create document objects, one per document
            Document document = new Document();
            string b64 = Convert.ToBase64String(GenerateSampleDocument(agent.Email, agent.FullName, insured.Email, insured.FullName));
            document.DocumentBase64 = b64;
            document.Name = "Order acknowledgement"; // can be different from actual file name
            document.FileExtension = "html"; // Source data format. Signed docs are always pdf.
            document.DocumentId = "1"; // a label used to reference the doc
            // The order in the docs array determines the order in the envelope

            return document;
        }

        private Signer CreateSigner(DocumentSigner signer, string order)
        {
            return new Signer
            {
                Email = signer.Email,
                Name = signer.FullName,
                RecipientId = order,
                RoutingOrder = order
            };
        }

        private Account GetAccountInfo(OAuth.OAuthToken authToken)
        {
            _apiClient.SetOAuthBasePath(_appSetting.DocUSignJwt.AuthServer);
            OAuth.UserInfo userInfo = _apiClient.GetUserInfo(authToken.access_token);
            Account acct = userInfo.Accounts.FirstOrDefault();
            if (acct == null)
            {
                throw new Exception("The user does not have access to account");
            }

            return acct;
        }

        //TODO: need to removed
        private byte[] GenerateSampleDocument(string signerEmail, string signerName, string ccEmail, string ccName)
        {
            // Data for this method
            // signerEmail
            // signerName
            // ccEmail
            // ccName

            return Encoding.UTF8.GetBytes(
            " <!DOCTYPE html>\n" +
                "    <html>\n" +
                "        <head>\n" +
                "          <meta charset=\"UTF-8\">\n" +
                "        </head>\n" +
                "        <body style=\"font-family:sans-serif;margin-left:2em;\">\n" +
                "        <h1 style=\"font-family: 'Trebuchet MS', Helvetica, sans-serif;\n" +
                "            color: darkblue;margin-bottom: 0;\">World Wide Corp</h1>\n" +
                "        <h2 style=\"font-family: 'Trebuchet MS', Helvetica, sans-serif;\n" +
                "          margin-top: 0px;margin-bottom: 3.5em;font-size: 1em;\n" +
                "          color: darkblue;\">Order Processing Division</h2>\n" +
                "        <h4>Ordered by " + signerName + "</h4>\n" +
                "        <p style=\"margin-top:0em; margin-bottom:0em;\">Email: " + signerEmail + "</p>\n" +
                "        <p style=\"margin-top:0em; margin-bottom:0em;\">Copy to: " + ccName + ", " + ccEmail + "</p>\n" +
                "        <p style=\"margin-top:3em;\">\n" +
                "  Candy bonbon pastry jujubes lollipop wafer biscuit biscuit. Topping brownie sesame snaps sweet roll pie. Croissant danish biscuit soufflé caramels jujubes jelly. Dragée danish caramels lemon drops dragée. Gummi bears cupcake biscuit tiramisu sugar plum pastry. Dragée gummies applicake pudding liquorice. Donut jujubes oat cake jelly-o. Dessert bear claw chocolate cake gummies lollipop sugar plum ice cream gummies cheesecake.\n" +
                "        </p>\n" +
                "        <!-- Note the anchor tag for the signature field is in white. -->\n" +
                "        <h3 style=\"margin-top:3em;\">Agreed: <span style=\"color:white;\">**signature_1**/</span></h3>\n" +
                "        </body>\n" +
                "    </html>"
                );
        }

        #endregion
    }
}
