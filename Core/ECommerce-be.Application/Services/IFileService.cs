using Microsoft.AspNetCore.Http;

namespace ECommerce_be.Application.Services
{
    public interface IFileService
    {
        Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);
    }
}
