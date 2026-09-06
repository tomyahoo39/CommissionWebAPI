using CommissionManagement.DTO.CommissionPeriodDTO;

namespace CommissionManagement.Services.CommissionPeriodSer
{
    public interface ICommissionPeriodService
    {
        Task<PeriodDTO> GetFirstPeriod();

        Task<IEnumerable<AllPeriodDTO>> AllPeriods();

        Task CreatePeriod(CreatePeriodDTO createPeriodDTO);
    }
}
