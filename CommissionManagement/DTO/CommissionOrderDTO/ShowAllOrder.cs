namespace CommissionManagement.DTO.CommissionOrderDTO
{
    public class ShowAllOrder
    {
        public int Id { get; set; }

        public string OrderCode { get; set; }

        public string Title { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public string SocialName { get; set; }

        public string SocialUrl { get; set; }

        public string TypeName { get; set; }

        public string CommissionSetting { get; set; }

        public int PaymentStatus { get; set; }

        public int WorkStatus { get; set; }

        public int SelectionStatus { get; set; }

        public string AdminNote { get; set; }

        public DateOnly? ScheduledDate { get; set; }

        public DateOnly CreatedAt { get; set; }
    }

    public class ShowOrderGuest
    {
        public string OrderCode { get; set; }

        public string Nickname { get; set; }

        public string SocialName { get; set; }

        public string TypeName { get; set; }

        public int PaymentStatus { get; set; }

        public int WorkStatus { get; set; }

        public DateOnly? ScheduledDate { get; set; }
    }
}
