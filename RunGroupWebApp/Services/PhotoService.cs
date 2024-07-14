using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RunGroupWebApp.Helpers;
using RunGroupWebApp.Interfaces;

namespace RunGroupWebApp.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile photo)
        {
            var UploadResult = new ImageUploadResult();
            if(photo.Length > 0)
            {
                using var stream = photo.OpenReadStream();
                var Uploadparam = new ImageUploadParams
                {
                    File = new FileDescription(photo.Name, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")

                };

                UploadResult = await _cloudinary.UploadAsync(Uploadparam);
            }

            return UploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string PublicId)
        {
            var DeleteParam = new DeletionParams(PublicId);
            var result = await _cloudinary.DestroyAsync(DeleteParam);

            return result;
        }
    }
}

