using CommissionManagement.DTO.ImagesDTO;

namespace CommissionManagement.Services.ImagesSer
{
    public interface IImageDatabaseService
    {
        Task<IEnumerable<GetFirstImageDTO>> GetFirstImages();
        Task UploadNewImage(ImageUploadDTO dto);


    }
}
