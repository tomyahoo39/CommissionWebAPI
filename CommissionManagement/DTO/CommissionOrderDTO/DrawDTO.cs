using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.CommissionOrderDTO
{
    public class DrawDTO
    {
        [Required]
        public int PeriodId { get; set; }
        [Required]
        public int DrawCount { get; set; }
    }

    public class DrawResultDTO
    {
        public int TotalNumber { get; set; }

        public int SelectedNumber { get; set; }

        public List<int> SelectedOrderIds { get; set; } = new ();
    }
}
