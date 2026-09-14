using CommissionManagement.DTO.CommissionTypeDTO;
using CommissionManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CommissionManagement.Services.CommissionTypeSer
{
    public class CommissionTypeService : ICommissionTypeService
    {
        private readonly CommissionContext _context;

        public CommissionTypeService(CommissionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShowTypeDTO>> ShowAllType()
        {
            var Type = await _context.CommissionTypes.Select(x => new ShowTypeDTO
            {
                Id = x.Id,
                TypeName = x.TypeName,
                IsActive = x.IsActive,
                IsHomeVisible = x.IsHomeVisible,
                HomeSortOrder = x.HomeSortOrder
            }).ToListAsync();

            return Type;
        }
        public async Task Create(CreateTypeDTO createDto)
        {
            var newType = new CommissionType
            {
                TypeName = createDto.TypeName,
                IsActive = true

            };

            await _context.CommissionTypes.AddAsync(newType);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateType(int id, UpdateTypeDTO updateDto)
        {
            var type = await _context.CommissionTypes.FindAsync(id);
            if(type == null)
            {
                throw new Exception("委託類型未找到");
            }

            if(!type.IsHomeVisible && updateDto.IsHomeVisible)
            {
                var currentVisibleCount = await _context.CommissionTypes
                    .CountAsync(x => x.IsHomeVisible);
                if (currentVisibleCount >= 4)
                {
                    throw new Exception("最多只能有四個委託類型顯示在首頁");
                }
            }

            type.TypeName = updateDto.TypeName;
            type.IsActive = updateDto.IsActive;
            type.IsHomeVisible = updateDto.IsHomeVisible;
            type.HomeSortOrder = updateDto.HomeSortOrder;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
