using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommissionManagement.Models;
using CommissionManagement.DTO.QaQuestionDTO;
using CommissionManagement.Services.QaQuestionSer;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class QaQuestionsController : ControllerBase
{
    private readonly IQaQuestionService _service;
    public QaQuestionsController(IQaQuestionService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("AllQa")]
    public async Task<ActionResult<IEnumerable<QaQuestionGetAllDTO>>> GetAllQaQuestion()
    {
        var question = await _service.GetAllQaQuestions();

        return Ok(question);
    }

    [HttpPost("QaQuestions")]
    public async Task<IActionResult> Create([FromBody] QaQuestionCreateDTO qaQuestion)
    {
        await _service.Create(qaQuestion);
        return Created();
    }

}
