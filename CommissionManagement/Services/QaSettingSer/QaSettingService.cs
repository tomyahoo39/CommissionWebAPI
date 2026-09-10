using CommissionManagement.DTO.QaSettingDTO;
using CommissionManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommissionManagement.Services.QaSettingSer
{
    public class QaSettingService : IQaSettingService
    {

        private readonly CommissionContext _context;

        public QaSettingService(CommissionContext commissionContext)
        {
            _context = commissionContext;
        }   
        public async Task<IEnumerable<QaSettingServiceDTO>> GetAllQaForAdmin()
        {
            var query = await _context.QaSettings.OrderBy(q => q.SortOrder)
                .Select(q => new QaSettingServiceDTO
            {
                Id = q.Id,
                Question = q.Question,
                Answer = q.Answer,
                SortOrder = q.SortOrder,
                IsVisible = q.IsVisible
            }).ToListAsync();

            return query;
        }

        public async Task<IEnumerable<QaSettingServiceClientDTO>> GetAllQaForClient()
        {
            var query = await _context.QaSettings
                .AsNoTracking()
                .Where(q => q.IsVisible == true)
                .OrderBy(q => q.SortOrder)
                .Select(q => new QaSettingServiceClientDTO
                {
                    Question = q.Question,
                    Answer = q.Answer,
                })
                .ToListAsync();

            return query;
        }

        public async Task Create(QaSettingServiceCreateDTO newQa)
        {
            var sort = await _context.QaSettings
                .OrderByDescending(q => q.SortOrder)
                .Select(q => q.SortOrder).FirstOrDefaultAsync();

            var qaSetting = new QaSetting
            {
                Question = newQa.Question,
                Answer = newQa.Answer,
                SortOrder = sort + 1,
                IsVisible = true
            };

            await _context.QaSettings.AddAsync(qaSetting);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Update(int id, QaSettingServiceDTO updatedQa)
        {
            var query = await _context.QaSettings.FindAsync(id);
            if(query == null)
            {
               return false;
            }

            query.Question = updatedQa.Question;
            query.Answer = updatedQa.Answer;
            query.SortOrder = updatedQa.SortOrder;
            query.IsVisible = updatedQa.IsVisible;

            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> Delete(int id)
        {
            var query = await _context.QaSettings.FindAsync(id);

            if(query == null)
            {
                return false;
            }

            _context.QaSettings.Remove(query);
            await _context.SaveChangesAsync();
            return true;

        }

    }
}
