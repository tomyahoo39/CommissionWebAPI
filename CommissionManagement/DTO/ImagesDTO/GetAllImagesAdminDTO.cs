namespace CommissionManagement.DTO.ImagesDTO
{
    public class GetAllImagesAdminDTO
    {
        public int Id { get; set; }

        public int CommissionTypeId { get; set; }

        public string Title { get; set; }

        public string ImagePath { get; set; }

        public string ThumbPath { get; set; }

        public int SortOrder { get; set; }

        public bool IsVisible { get; set; }

        public DateOnly CreatedAt { get; set; }

        public DateOnly UpdatedAt { get; set; }
    }
}
