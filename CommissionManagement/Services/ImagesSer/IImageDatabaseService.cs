using CommissionManagement.DTO.ImagesDTO;

namespace CommissionManagement.Services.ImagesSer
{
    public interface IImageDatabaseService
    {
        Task UploadNewImage(ImageUploadDto dto);
    }
}
