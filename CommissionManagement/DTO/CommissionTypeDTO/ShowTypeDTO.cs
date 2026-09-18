namespace CommissionManagement.DTO.CommissionTypeDTO
{
    public class ShowTypeDTO
    {
        public int Id { get; set; }

        public string TypeName { get; set; }

        public bool IsActive { get; set; }

        public bool IsHomeVisible { get; set; }

        public int HomeSortOrder { get; set; }

        public int? BasePrice { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }
    }
}
