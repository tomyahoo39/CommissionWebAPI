using CommissionManagement.DTO.CommissionTypeDTO;

namespace CommissionManagement.Services.CommissionTypeSer
{
    public interface ICommissionTypeService
    {
        Task<IEnumerable<ActiveTypeDTO>> GetActiveType();
        Task<IEnumerable<ShowTypeDTO>> ShowAllType();
        Task Create(CreateTypeDTO createDto);

        Task<bool> UpdateType(int id ,UpdateTypeDTO updateDto);
    }
}
