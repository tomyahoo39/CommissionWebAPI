using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.CommissionOrderDTO
{
    public class OrderUpdateDTO
    {
        [Required]
        public DateOnly? ScheduledDate { get; set; }
        [Required]
        public int PaymentStatus { get; set; }
        [Required]
        public int WorkStatus { get; set; }

        public string? AdminNote { get; set; }
    }

}
