namespace CommissionManagement.DTO.CommissionPeriodDTO
{
    public class PeriodDTO
    {
        public string Title { get; set; }

        public DateOnly OpenAt { get; set; }

        public DateOnly CloseAt { get; set; }

        public byte? MaxWinners { get; set; }
    }
}
