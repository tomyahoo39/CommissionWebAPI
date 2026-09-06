using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.CommissionPeriodDTO
{
    public class CreatePeriodDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public DateOnly OpenAt { get; set; }
        [Required]
        public DateOnly CloseAt { get; set; }
        [Required]
        public byte Ststus { get; set; }
        [Required]
        public byte? MaxWinners { get; set; }
    }
}
