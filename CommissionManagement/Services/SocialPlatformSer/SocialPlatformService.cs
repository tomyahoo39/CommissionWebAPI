using CommissionManagement.DTO.SocialPlatformDTO;
using CommissionManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CommissionManagement.Services.SocialPlatformSer
{
    public class SocialPlatformService : ISocialPlatformService
    {
        private readonly CommissionContext _context;

        public SocialPlatformService(CommissionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ActiveSocialDTO>> ShowActiveSocial()
        {
            var social = await _context.SocialPlatforms
                .Where(s => s.IsActive)
                .Select(s => new ActiveSocialDTO
                {
                    Id = s.Id,
                    SocialName = s.SocialName,
                }).ToListAsync();

            return social;
        }

        public async Task<IEnumerable<ShowSocialDTO>> ShowSocial()
        {
            var social = await _context.SocialPlatforms
                .Select(s => new ShowSocialDTO
                {
                    Id = s.Id,
                    SocialName = s.SocialName,
                    IsActive = s.IsActive
                })
                .ToListAsync();

            return social;
        }

        public async Task Create(SocialCreateDTO socialCreateDTO)
        {
            var newSocial = new SocialPlatform
            {
                SocialName = socialCreateDTO.SocialName,
            };

            await _context.SocialPlatforms.AddAsync(newSocial);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Update(int id, SocialUpdateDTO socialUpdateDTO)
        {
            var social = await _context.SocialPlatforms.FindAsync(id);

            if(social == null)
            {
                return false;
            }

            social.SocialName = socialUpdateDTO.SocialName;
            social.IsActive = socialUpdateDTO.IsActive;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
