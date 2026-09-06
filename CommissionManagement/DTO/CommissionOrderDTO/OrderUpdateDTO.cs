namespace CommissionManagement.DTO.CommissionOrderDTO
{
    public class OrderUpdateDTO
    {
        public DateOnly? ScheduledDate { get; set; }

        public int PaymentStatus { get; set; }

        public int WorkStatus { get; set; }
    }

}
