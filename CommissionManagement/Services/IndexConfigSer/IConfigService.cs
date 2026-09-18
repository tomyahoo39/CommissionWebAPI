using CommissionManagement.DTO.CommissionTypeDTO;
using CommissionManagement.DTO.ConfigDTO;

namespace CommissionManagement.Services.IndexConfigSer
{
    public interface IConfigService
    {
        Task<IEnumerable<ShowNoticeDTO>> GetIndexNotice();

        Task<bool> UpdateNotice(int id, UpdateNoticeDTO updateDto);
    }
}
