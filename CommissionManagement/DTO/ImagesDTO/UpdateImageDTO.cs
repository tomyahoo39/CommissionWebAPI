namespace CommissionManagement.DTO.ImagesDTO
{
    public class UpdateImageDTO
    {
        public int CommissionTypeId { get; set; }

        public string Title { get; set; }
        public int SortOrder { get; set; }

        public bool IsVisible { get; set; }

        public DateOnly UpdatedAt { get; set; }
    }
}
