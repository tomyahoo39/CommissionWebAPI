using CommissionManagement.DTO.ImagesDTO;

namespace CommissionManagement.Services.ImagesSer
{
    public interface IImageDatabaseService
    {
        Task<IEnumerable<GetFirstImageDTO>> GetFirstImages();

        Task<IEnumerable<GetAllImageDTO>> ShowAllImages(int? commissionTypeId);

        Task<IEnumerable<GetAllImagesAdminDTO>> ShowAllImagesAdmin(int? commissionTypeId);

        Task<bool> Update(int id, UpdateImageDTO dto);
        Task UploadNewImage(ImageUploadDTO dto);


    }
}
