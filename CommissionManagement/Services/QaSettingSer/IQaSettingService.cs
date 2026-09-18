using CommissionManagement.DTO.QaSettingDTO;

namespace CommissionManagement.Services.QaSettingSer
{
    public interface IQaSettingService
    {
        Task<IEnumerable<QaSettingServiceClientDTO>> GetAllQaForClient();
        Task<IEnumerable<QaSettingServiceDTO>> GetAllQaForAdmin();

        Task Create(QaSettingServiceCreateDTO newQa);

        Task<bool> Update(QaSettingServiceDTO updatedQa);

        Task<bool> Delete(int id);

    }
}
