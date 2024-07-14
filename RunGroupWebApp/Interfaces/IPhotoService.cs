using CloudinaryDotNet.Actions;

namespace RunGroupWebApp.Interfaces
{
	public interface IPhotoService
	{
		Task<ImageUploadResult> AddPhotoAsync(IFormFile photo);
		Task<DeletionResult> DeletePhotoAsync(string PublicId); 
	}
}

