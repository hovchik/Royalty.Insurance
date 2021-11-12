namespace Core.System.DocUSignJwt.Model
{
    public class DocumentSigner
    {
        public DocumentSigner(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }

        public string Email { get; }

        public string FullName { get; }
    }
}
