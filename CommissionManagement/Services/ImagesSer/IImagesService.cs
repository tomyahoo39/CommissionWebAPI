namespace CommissionManagement.Services.ImagesSer
{
    public interface IImagesService
    {
        Task<(string ImagePath,string ThumbPath)> UploadAndProcess(Stream fileStream,string contentType);
    }
}
