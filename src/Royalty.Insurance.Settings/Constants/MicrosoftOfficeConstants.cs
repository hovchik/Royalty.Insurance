
namespace Royalty.Insurance.Settings.Constants
{
    public  static class MicrosoftOfficeConstants
    {
        public const string GraphApiEmailSelector =
            "subject,uniqueBody,From,ConversationId,ParentFolderId,HasAttachments,SentDateTime,ToRecipients,Subject,ReceivedDateTime,CcRecipients,IsRead";

        public const string GraphApiOfficeHeaderKey = "Prefer";
        public const string GraphApiHeaderValue = "outlook.body-content-type=\"text\"";

        public const string ParentFolderFilter = "parentFolderId eq '{0}'";
        public const string ConversationIdFilter = "conversationId eq '{0}'";
    }
}
