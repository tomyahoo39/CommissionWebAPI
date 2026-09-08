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

    [HttpGet("FirstThumbs")]
    public async Task<ActionResult<IEnumerable<GetFirstImageDTO>>> GetFirstThumbs()
    {
        var firstImages = await _service.GetFirstImages();
        return Ok(firstImages);
    }

    [HttpGet("AllImages")]
    public async Task<ActionResult<IEnumerable<GetAllImageDTO>>> GetAllImages([FromQuery] int? commissionTypeId)
    {
        try
        {
            var images = await _service.ShowAllImages(commissionTypeId);
            return Ok(images);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("AllImagesAdmin")]
    public async Task<ActionResult<IEnumerable<GetAllImagesAdminDTO>>> GetAllImagesAdmin([FromQuery] int? commissionTypeId)
    {
        try
        {
            var images = await _service.ShowAllImagesAdmin(commissionTypeId);
            return Ok(images);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("UpdateImage/{id}")]
    public async Task<IActionResult> UpdateImage(int id, [FromBody]UpdateImageDTO dto)
    {
        if(dto == null)
        {
            return BadRequest("請輸入完整資料");
        }

        var result = await _service.Update(id, dto);
        if(result == false)
        {
            return NotFound("找不到該圖片");
        }
        return Ok(new { message = "圖片修改成功" });
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
