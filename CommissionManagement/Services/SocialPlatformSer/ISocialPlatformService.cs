using CommissionManagement.DTO.SocialPlatformDTO;

namespace CommissionManagement.Services.SocialPlatformSer
{
    public interface ISocialPlatformService
    {
        Task<IEnumerable<ShowSocialDTO>> ShowSocial();

        Task Create(SocialCreateDTO socialCreateDTO);

        Task<bool> Update(int id, SocialUpdateDTO socialUpdateDTO);
    }
}
