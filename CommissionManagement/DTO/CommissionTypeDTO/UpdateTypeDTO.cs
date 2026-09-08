namespace CommissionManagement.DTO.CommissionTypeDTO
{
    public class UpdateTypeDTO
    {
        public string TypeName { get; set; }

        public bool IsActive { get; set; }

        public bool IsHomeVisible { get; set; }

        public int HomeSortOrder { get; set; }
    }
}
