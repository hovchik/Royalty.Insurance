using System.Common.Exceptions;
using System.Common.Storage.Response;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Http;
using Royalty.Insurance.Settings;


namespace System.Common.Storage
{
    public class StorageManager : IStorageManager
    {
        private readonly BlobServiceClient _blobServiceClient;

        public StorageManager(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<long?> GetContainerSizeAsync(int userId, string blobContainerName)
        {
            BlobContainerClient blobClient = _blobServiceClient.GetBlobContainerClient($"{blobContainerName}");
            await blobClient.CreateIfNotExistsAsync();
            var resultSegment = blobClient.GetBlobsAsync()
            .AsPages(default, 10);
            long? size = 0;

            await foreach (var bl in resultSegment)
            {
                foreach (var blob in bl.Values)
                {
                    if (blob.Name.StartsWith($"{userId}"))
                    {
                        size = size + blob.Properties.ContentLength;
                    }
                }
            }

            return size;
        }

        public async Task<UploadResponse> UploadAsync(IFormFile file, string blobName, string folderName, string fileName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(blobName);
            await container.CreateIfNotExistsAsync().ConfigureAwait(false);

            var blockBlob = container.GetBlockBlobClient($"{folderName}/{fileName}");

            await blockBlob.DeleteIfExistsAsync();

            var uploadResult = await blockBlob.UploadAsync(file.OpenReadStream(), new BlobHttpHeaders { ContentType = file.ContentType });
            int statusCode = uploadResult.GetRawResponse().Status;

            if (statusCode != (int)HttpStatusCode.Created)
            {
                throw new RestApiResponseException(statusCode, ResourceCommonMessage.UploadFailed);
            }

            return new UploadResponse
            {
                FileName = fileName,
                LastModifiedDate = DateTime.Parse(uploadResult.GetRawResponse().Headers.Date.ToString())
            };
        }
        public async Task<BaseResponse> ReadAsync(string blobContainerName, string folderName, string fileName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient($"{blobContainerName}");
            BlobClient blobClient = containerClient.GetBlobClient($"{folderName}/{fileName}");

            var responseFile = await blobClient.DownloadAsync();
            BaseResponse response = new BaseResponse
            {
                DataInBytes = await ReadFully(responseFile.Value.Content),
                ContentType = (await blobClient.GetPropertiesAsync()).Value.ContentType
            };

            return response;
        }

        public async Task<DeleteResponse> DeleteAsync(string blobContainerName, string folderName, string fileName, int userId)
        {
            var container = _blobServiceClient.GetBlobContainerClient(blobContainerName);
            var blockBlob = container.GetBlockBlobClient($"{folderName}/{fileName}");
            var deleteResponse = await blockBlob.DeleteIfExistsAsync();

            var statusCode = deleteResponse.GetRawResponse().Status;
            if (statusCode != (int)HttpStatusCode.Accepted)
            {
                throw new RestApiResponseException(statusCode, ResourceCommonMessage.DeleteFailed);
            }

            return new DeleteResponse
            {
                FileName = fileName,
                UserId = userId,
                DeleteDatetime = DateTime.Parse(deleteResponse.GetRawResponse().Headers.Date.ToString())
            };
        }

        public async Task<byte[]> ReadFully(Stream input)
        {
            byte[] buffer = new byte[25 * 1024];
            await using MemoryStream ms = new MemoryStream();
            int read;
            while ((read = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await ms.WriteAsync(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }
}
