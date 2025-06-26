using Altitude.Bussiness.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Services
{
    public class CloudinaryImageUploadService : IImageUploadService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryImageUploadService(IConfiguration configuration)
        {
            var cloudName = configuration["Cloudinary:CloudName"];
            var apiKey = configuration["Cloudinary:ApiKey"];
            var apiSecret = configuration["Cloudinary:ApiSecret"];
            _cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
        }

        public async Task<string?> UploadImageAsync(IFormFile file, string folder = "")
        {
            if (file == null || file.Length == 0)
                return null;

            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folder
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl?.ToString();
        }
    }
}