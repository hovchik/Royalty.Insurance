namespace Core.System.MicrosoftGraph
{
    public class UploadDocumentResponse
    {
        public UploadDocumentResponse(string groupId, string driveItemId, string path)
        {
            GroupId = groupId;
            DriveItemId = driveItemId;
            Path = path;
        }

        public string GroupId { get; }

        public string DriveItemId { get; }

        public string Path { get; }
    }
}
