namespace CommissionManagement.DTO.ImagesDTO
{
    public class GetFirstImageDTO
    {
        public int CommissionTypeId { get; set; }
        public string TypeName { get; set; }
        public int HomeSortOrder { get; set; }
        public string? Title { get; set; }
        public string? ThumbPath { get; set; }
    }
}
