using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommissionManagement.Models;
using CommissionManagement.Services.SocialPlatformSer;
using CommissionManagement.DTO.SocialPlatformDTO;

[Route("api/[controller]")]
[ApiController]
public class SocialPlatformsController : ControllerBase
{
    private readonly ISocialPlatformService _service;
    public SocialPlatformsController(ISocialPlatformService service)
    {
        _service = service;
    }

    [HttpGet("AllSocial")]   
    public async Task<IActionResult> ShowSocial()
    {
        var socials = await _service.ShowSocial();
        return Ok(socials);
    }

    [HttpPost("Social")]
    public async Task<ActionResult> CreateSocial([FromBody] SocialCreateDTO CreateDTO)
    {
        if (CreateDTO == null)
        {
            return BadRequest("請提供有效的社群資料");
        }

        await _service.Create(CreateDTO);
        return Ok();
    }

    [HttpPut("Social/{id}")]
    public async Task<IActionResult> UpdateSocial(int id, [FromBody] SocialUpdateDTO socialUpdateDTO)
    {
        var result = await _service.Update(id, socialUpdateDTO);
        if (result == false)
        {
            return NotFound("社群選項不存在");
        }

        return Ok();
    }
}
