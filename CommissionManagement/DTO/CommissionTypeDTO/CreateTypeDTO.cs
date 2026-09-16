using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.CommissionTypeDTO
{
    public class CreateTypeDTO
    {
        [Required]
        public string TypeName { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int? BasePrice { get; set; }

        public string? ShortDescription { get; set; }

        public string? FullDescription { get; set; }
    }
}
