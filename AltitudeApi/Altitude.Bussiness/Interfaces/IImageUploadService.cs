using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Interfaces
{
    public interface IImageUploadService
    {
        Task<string?> UploadImageAsync(IFormFile file, string folder = "");
    }
}