using CommissionManagement.DTO.ConfigDTO;
using CommissionManagement.Models;
using CommissionManagement.Services.IndexConfigSer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ConfigsController : ControllerBase
{
    private readonly IConfigService _service;
    public ConfigsController(IConfigService service)
    {
        _service = service;
    }

    // GET: api/Config
    [HttpGet("Notice")]
    public async Task<ActionResult<IEnumerable<Config>>> GetNotice()
    {
        var notice = await _service.GetIndexNotice();  
        if(notice == null)
        {
            return NotFound();
        }
        return Ok(notice);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("UpdateNotice/{id}")]
    public async Task<ActionResult> UpdateNotice(int id , [FromBody]UpdateNoticeDTO updateDto)
    {
        var notice = await _service.UpdateNotice(id, updateDto);
        if (updateDto == null)
        {
            return BadRequest("請輸入委前須知");
        }

        try
        {
            var result = await _service.UpdateNotice(id, updateDto);

            if (result == false)
            {
                return NotFound("委前須知未找到");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"更新委託類型時發生錯誤: {ex.Message}");
        }
    }
}
