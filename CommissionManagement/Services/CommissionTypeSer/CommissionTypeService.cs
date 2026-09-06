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
                TypeName = x.TypeName
            }).ToListAsync();

            return Type;
        }
        public async Task Create(CreateTypeDTO createDto)
        {
            var newType = new CommissionType
            {
                TypeName = createDto.TypeName
            };

            await _context.CommissionTypes.AddAsync(newType);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateType(int id, CreateTypeDTO updateDto)
        {
            var type = await _context.CommissionTypes.FindAsync(id);
            if(type == null) return false;

            type.TypeName = updateDto.TypeName;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteType(int id)
        {
            var type = await _context.CommissionTypes.FindAsync(id);
            if(type == null) return false;

            _context.CommissionTypes.Remove(type);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
