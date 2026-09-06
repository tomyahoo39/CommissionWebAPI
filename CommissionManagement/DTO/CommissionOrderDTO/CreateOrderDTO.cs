using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.CommissionOrderDTO
{
    public class CreateOrderDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int? SocialId { get; set; }

        public string? SocialUrl { get; set; }
        [Required]
        public int CommissionTypeId { get; set; }
        [Required]
        public string CommissionSetting { get; set; }
    }
}
