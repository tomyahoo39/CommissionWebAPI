using CommissionManagement.DTO.SocialPlatformDTO;
using CommissionManagement.Models;
using CommissionManagement.Services.SocialPlatformSer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class SocialPlatformsController : ControllerBase
{
    private readonly ISocialPlatformService _service;
    public SocialPlatformsController(ISocialPlatformService service)
    {
        _service = service;
    }

    [HttpGet("ActiveSocial")]
    public async Task<ActionResult<ActiveSocialDTO>> ShowActiveSocial()
    {
        var social = await _service.ShowActiveSocial();
        if(social == null)
        {
            return NotFound();
        }
        return Ok(social);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("AllSocial")]   
    public async Task<IActionResult> ShowSocial()
    {
        var socials = await _service.ShowSocial();
        return Ok(socials);
    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
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
