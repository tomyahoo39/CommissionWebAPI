using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommissionManagement.Models;
using CommissionManagement.DTO.QaSettingDTO;
using CommissionManagement.Services.QaSettingSer;

[Route("api/[controller]")]
[ApiController]
public class QaSettingsController : ControllerBase
{
    private readonly IQaSettingService _service;
    public QaSettingsController(IQaSettingService service)
    {
        _service = service;
    }

    [HttpGet("AdminQaSetting")]
    public async Task<ActionResult<IEnumerable<QaSettingServiceDTO>>> GetAllQaForAdmin()
    {
        var qa = await _service.GetAllQaForAdmin();

        return Ok(qa);
    }

    [HttpGet("ClientQaSetting")]
    public async Task<ActionResult<IEnumerable<QaSettingServiceClientDTO>>> GetAllQaForClient()
    {
        var qa = await _service.GetAllQaForClient();
        return Ok(qa);
    }

    [HttpPost("CreateQaSetting")]
    public async Task<IActionResult> CreateQaSetting([FromBody] QaSettingServiceCreateDTO newQa)
    {
        await _service.Create(newQa);
        return Ok();
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateQaSetting([FromBody] QaSettingServiceDTO updatedQa)
    {
        var qa = await _service.Update(updatedQa);
        if(qa == false)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteQaSetting(int id)
    {
        var qa = await _service.Delete(id);
        if (qa == false)
        {
            return NotFound();
        }
        return Ok();

    }
}
