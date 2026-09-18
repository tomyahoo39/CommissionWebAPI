using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.QaSettingDTO
{
    public class QaSettingServiceClientDTO
    {

        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

    }
}
