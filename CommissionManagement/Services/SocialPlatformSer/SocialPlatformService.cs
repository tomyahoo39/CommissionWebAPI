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

        public async Task<IEnumerable<ShowSocialDTO>> ShowSocial()
        {
            var social = await _context.SocialPlatforms
                .Select(s => new ShowSocialDTO
                {
                    Id = s.Id,
                    SocialName = s.SocialName
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

        public async Task<bool> Delete(int id)
        {
            var social = await _context.SocialPlatforms.FindAsync(id);

            if(social == null)
            {
                return false;
            }

            _context.SocialPlatforms.Remove(social);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
