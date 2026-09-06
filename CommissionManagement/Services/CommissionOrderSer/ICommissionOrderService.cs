using CommissionManagement.DTO.CommissionOrderDTO;

namespace CommissionManagement.Services.CommissionOrderSer
{
    public interface ICommissionOrderService
    {

        public Task<DrawResultDTO> DrawOrdersAsync(DrawDTO drawDto);

        public Task<bool> UpdateOrder(int Id, OrderUpdateDTO updateDto);

        public Task<IEnumerable<ShowAllOrder>> ShowOrderAdmin(int periodId);

        public Task<IEnumerable<ShowOrderGuest>> ShowOrderGuest();

    }
}
