using CloudinaryDotNet.Actions;

namespace API.Interfaces;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);//add photo

    Task<DeletionResult> DeletePhotoAsync(string publicId);//delete photo

}
