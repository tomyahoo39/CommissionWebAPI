using CommissionManagement.DTO.CommissionPeriodDTO;
using CommissionManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CommissionManagement.Services.CommissionPeriodSer
{
    public class CommissionPeriodService : ICommissionPeriodService
    {
        private readonly CommissionContext _context;

        public CommissionPeriodService(CommissionContext context)
        {
            _context = context;
        }

        public async Task<PeriodDTO> GetFirstPeriod()
        {
            var firstPeriod = await _context.CommissionPeriods
                .AsNoTracking()
                .OrderByDescending(p => p.Id)
                .Select(p => new PeriodDTO
                {
                    OpenAt = p.OpenAt,
                    CloseAt = p.CloseAt
                }).FirstOrDefaultAsync();

            if (firstPeriod == null)
            {
                throw new InvalidOperationException("找不到委託期資料");
            }

            return firstPeriod;
        }
        public async Task<IEnumerable<AllPeriodDTO>> AllPeriods()
        {
            var allPeriods = await _context.CommissionPeriods
                .AsNoTracking()
                .OrderByDescending(p => p.Id)
                .Select(p => new AllPeriodDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    OpenAt = p.OpenAt,
                    CloseAt = p.CloseAt,
                    Ststus = p.Ststus,
                    MaxWinners = p.MaxWinners,
                    CreatedAt = p.CreatedAt
                }).ToListAsync();

            return allPeriods;
        }

        public async Task CreatePeriod(CreatePeriodDTO createPeriodDTO)
        {
            var newPeriod = new CommissionPeriod
            {
                Title = createPeriodDTO.Title,
                OpenAt = createPeriodDTO.OpenAt,
                CloseAt = createPeriodDTO.CloseAt,
                Ststus = createPeriodDTO.Ststus,
                MaxWinners = createPeriodDTO.MaxWinners,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            await _context.CommissionPeriods.AddAsync(newPeriod);
            await _context.SaveChangesAsync();
        }
    }
}
