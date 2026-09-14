using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.CommissionTypeDTO
{
    public class CreateTypeDTO
    {
        [Required]
        public string TypeName { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
