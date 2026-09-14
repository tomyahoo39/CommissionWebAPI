using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.CommissionTypeDTO
{
    public class UpdateTypeDTO
    {
        [Required]
        public string TypeName { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsHomeVisible { get; set; }
        [Required]
        public int HomeSortOrder { get; set; }
    }
}
