using CommissionManagement.DTO.QaSettingDTO;
using CommissionManagement.Models;
using CommissionManagement.Services.QaSettingSer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class QaSettingsController : ControllerBase
{
    private readonly IQaSettingService _service;
    public QaSettingsController(IQaSettingService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    [HttpPost("CreateQaSetting")]
    public async Task<IActionResult> CreateQaSetting([FromBody] QaSettingServiceCreateDTO newQa)
    {
        await _service.Create(newQa);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
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
