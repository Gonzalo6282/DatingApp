using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;
    public PhotoService(IOptions<CloudinarySettings> config)//access Cloudinary settings, call config
    {
        var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);

        _cloudinary = new Cloudinary(acc);
    }
    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();
        if (file.Length > 0)//if there is a file
        {
            using var stream = file.OpenReadStream();//open to read uploaded file
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),//pass params
                Transformation = new Transformation()
                    .Height(500).Width(500).Crop("fill").Gravity("face"),//transfrom image
                Folder = "da-net8"//folder for images
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);//after result upload  image to cloudinary
        }

         return uploadResult;
    }
    
    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);

        return await _cloudinary.DestroyAsync(deleteParams);//destroy method to delete photos
    }
}
