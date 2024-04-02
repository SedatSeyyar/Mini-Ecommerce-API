using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ECommerce_be.Application.Abstractions.Storage.Local;
using ECommerce_be.Infrastructure.StaticServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ECommerce_be.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : ILocalStorage
    {
        readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;

        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
        }

        public async Task DeleteAsync(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Select(x => x.Name).ToList();
        }

        public bool HasFile(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Any(x => x.Name == fileName);
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, List<IFormFile> files)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string pathOrContainerName)> datas = new();
            foreach (IFormFile file in files)
            {
                BlobClient blobClient = _blobContainerClient.GetBlobClient(file.Name);
                await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((file.Name, containerName));
            }
            return datas;
        }
    }
}
