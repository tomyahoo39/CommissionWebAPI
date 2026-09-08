using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommissionManagement.Models;
using CommissionManagement.Services.ImagesSer;
using CommissionManagement.DTO.ImagesDTO;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly IImageDatabaseService _service;
    public ImagesController(IImageDatabaseService service)
    {
        _service = service;
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> UploadNewImage([FromForm] ImageUploadDTO dto)
    {
        try
        {
            await _service.UploadNewImage(dto);
            return Ok(new {Message = "圖片上傳成功" });
        }
        catch(KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
