namespace CommissionManagement.DTO.ImagesDTO
{
    public class GetAllImageDTO
    {
        public int CommissionTypeId { get; set; }

        public string Title { get; set; }

        public string ImagePath { get; set; }

        public string ThumbPath { get; set; }

        public int SortOrder { get; set; }
    }
}
