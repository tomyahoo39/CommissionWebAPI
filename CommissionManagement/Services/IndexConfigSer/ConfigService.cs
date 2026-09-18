using CommissionManagement.DTO.ConfigDTO;
using CommissionManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CommissionManagement.Services.IndexConfigSer
{
    public class ConfigService : IConfigService
    {
        private readonly CommissionContext _context;

        public ConfigService(CommissionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShowNoticeDTO>> GetIndexNotice()
        {
            var notice = await _context.Configs
                .Select(s => new ShowNoticeDTO
                {
                    Id = s.Id,
                    NoticeContent = s.NoticeContent
                }).ToListAsync();

            return notice;
        }

        public async Task<bool> UpdateNotice(int id, UpdateNoticeDTO updateDto)
        {
            var notice = await _context.Configs.FindAsync(id);
            if (notice == null)
            {
                throw new Exception("委前須知未找到");
            }

            notice.NoticeContent = updateDto.NoticeContent;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
