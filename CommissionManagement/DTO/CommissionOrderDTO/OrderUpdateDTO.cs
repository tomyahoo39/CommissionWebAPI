using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.CommissionOrderDTO
{
    public class OrderUpdateDTO
    {
        public DateOnly? ScheduledDate { get; set; }
        [Required]
        public int PaymentStatus { get; set; }
        [Required]
        public int WorkStatus { get; set; }

        [Required]
        public int SelectionStatus { get; set; }

        public string? AdminNote { get; set; }
    }

}
