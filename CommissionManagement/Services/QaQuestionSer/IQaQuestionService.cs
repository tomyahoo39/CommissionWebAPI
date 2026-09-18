using CommissionManagement.DTO.QaQuestionDTO;

namespace CommissionManagement.Services.QaQuestionSer
{
    public interface IQaQuestionService
    {
        Task<IEnumerable<QaQuestionGetAllDTO>> GetAllQaQuestions();

        Task Create(QaQuestionCreateDTO qaQuestion);
    }
}
