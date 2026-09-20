using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommissionManagement.Models;
using CommissionManagement.DTO.QaQuestionDTO;
using CommissionManagement.Services.QaQuestionSer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Cryptography;
using System.Text;
using CommissionManagement.Services.Security;

[Route("api/[controller]")]
[ApiController]
public class QaQuestionsController : ControllerBase
{
    private readonly IQaQuestionService _service;
    private readonly IRequestFloodGuardService _floodGuard;

    public QaQuestionsController(IQaQuestionService service, IRequestFloodGuardService floodGuard)
    {
        _service = service;
        _floodGuard = floodGuard;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("AllQa")]
    public async Task<ActionResult<IEnumerable<QaQuestionGetAllDTO>>> GetAllQaQuestion()
    {
        var question = await _service.GetAllQaQuestions();

        return Ok(question);
    }

    [EnableRateLimiting("AnonWrite")]
    [HttpPost("QaQuestions")]
    public async Task<IActionResult> Create([FromBody] QaQuestionCreateDTO qaQuestion)
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var raw = $"{ip}|{qaQuestion.Email}|{qaQuestion.Question?.Trim()}";
        var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(raw)));

        if (_floodGuard.IsDuplicate($"qa:{hash}", TimeSpan.FromSeconds(30)))
            return StatusCode(429, "請勿重複送出，稍後再試");

        await _service.Create(qaQuestion);
        return Created();
    }

}
