namespace CommissionManagement.DTO.QaQuestionDTO
{
    public class QaQuestionGetAllDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Question { get; set; }

        public DateOnly CreatedAt { get; set; }
    }
}
