using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.QaSettingDTO
{
    public class QaSettingServiceDTO
    {
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool IsVisible { get; set; }
    }
}
