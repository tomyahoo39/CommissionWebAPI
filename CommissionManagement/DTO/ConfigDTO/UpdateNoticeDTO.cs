using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.ConfigDTO
{
    public class UpdateNoticeDTO
    {
        [Required]
        public string NoticeContent { get; set; }
    }
}
