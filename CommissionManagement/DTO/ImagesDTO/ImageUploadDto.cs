using System.ComponentModel.DataAnnotations;

namespace CommissionManagement.DTO.ImagesDTO
{
    public class ImageUploadDto
    {
        [Required(ErrorMessage = "請選擇分類")]
        public int CommissionTypeId { get; set; }

        [Required(ErrorMessage = "請輸入標題")]
        [StringLength(100, ErrorMessage = "標題長度不能超過 100 字")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "請選擇要上傳的圖片檔案")]
        public IFormFile File { get; set; } = null!;
    }
}
