using CommissionManagement.DTO.CommissionOrderDTO;

namespace CommissionManagement.Services.CommissionOrderSer
{
    public interface ICommissionOrderService
    {

        Task<DrawResultDTO> DrawOrders(DrawDTO drawDto);

        Task<bool> UpdateOrder(int Id, OrderUpdateDTO updateDto);

        Task<DrawResultDTO> ReDrawOrders(DrawDTO drawDto);

        Task<IEnumerable<ShowAllOrder>> ShowOrderAdmin(int periodId);

        Task<IEnumerable<ShowOrderGuest>> ShowOrderGuest();

        Task CreateNewOrder(CreateOrderDTO createOrderDTO);

    }
}
