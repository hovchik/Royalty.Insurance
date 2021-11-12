using Microsoft.AspNetCore.Http;
using System.Common.Storage.Response;
using System.Threading.Tasks;

namespace System.Common.Storage
{
    public interface IStorageManager
    {
        Task<long?> GetContainerSizeAsync(int userId, string blobContainerName);
        Task<UploadResponse> UploadAsync(IFormFile file, string blobName, string folderName, string fileName);
        Task<BaseResponse> ReadAsync(string blobContainerName, string folderName, string fileName);
        Task<DeleteResponse> DeleteAsync(string blobContainerName, string folderName, string fileName,int userId);

    }
}
