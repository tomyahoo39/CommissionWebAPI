namespace CommissionManagement.DTO.CommissionPeriodDTO
{
    public class AllPeriodDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateOnly OpenAt { get; set; }

        public DateOnly CloseAt { get; set; }

        public byte Ststus { get; set; }

        public byte? MaxWinners { get; set; }

        public DateOnly CreatedAt { get; set; }
    }
}
