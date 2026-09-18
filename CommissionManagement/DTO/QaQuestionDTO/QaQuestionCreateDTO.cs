using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.QaQuestionDTO
{
    public class QaQuestionCreateDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Question { get; set; }
    }
}
