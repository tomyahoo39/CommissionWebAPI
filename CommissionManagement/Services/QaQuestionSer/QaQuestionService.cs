using CommissionManagement.DTO.QaQuestionDTO;
using CommissionManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CommissionManagement.Services.QaQuestionSer
{
    public class QaQuestionService : IQaQuestionService
    {
        private readonly CommissionContext _context;

        public QaQuestionService(CommissionContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<QaQuestionGetAllDTO>> GetAllQaQuestions()
        {
            var query = await _context.QaQuestions
                .AsNoTracking()
                .OrderByDescending(q=> q.Id)
                .Select(q => new QaQuestionGetAllDTO
                {
                    Id = q.Id,
                    Email = q.Email,
                    Question = q.Question,
                    CreatedAt = q.CreatedAt,
                    IsFinish = q.IsFinish,
                }).ToListAsync();

            return query;
        }

        public async Task Create(QaQuestionCreateDTO qaQuestion)
        {
            var qa = new QaQuestion
            {
                Email = qaQuestion.Email,
                Question = qaQuestion.Question,
                CreatedAt = DateOnly.FromDateTime(DateTime.Today)
            };

            await _context.QaQuestions.AddAsync(qa);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> QaFinish(int id, QaQuestionFinishDTO dto)
        {
            var qaQuestion = await _context.QaQuestions.FindAsync(id);

            if(qaQuestion == null)
            {
                return false;
            }

            qaQuestion.IsFinish = dto.IsFinish;
            await _context.SaveChangesAsync();

            return true;

        }

    }
}
