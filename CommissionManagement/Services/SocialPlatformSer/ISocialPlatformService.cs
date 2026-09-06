using CommissionManagement.DTO.SocialPlatformDTO;

namespace CommissionManagement.Services.SocialPlatformSer
{
    public interface ISocialPlatformService
    {
        Task Create(SocialCreateDTO socialCreateDTO);

        Task<bool> Delete(int id);
    }
}
