using CommissionManagement.DTO.ImagesDTO;
using CommissionManagement.Models;
using CommissionManagement.Services.ImagesSer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private const long MaxImageUploadBytes = 10 * 1024 * 1024;

    private readonly IImageDatabaseService _service;
    private readonly ILogger<ImagesController> _logger;
    public ImagesController(IImageDatabaseService service, ILogger<ImagesController> logger)
    {
        _service = service;
        _logger = logger;
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
            _logger.LogError(ex, "GetAllImages failed. CommissionTypeId: {CommissionTypeId}", commissionTypeId);
            return BadRequest("查詢圖片失敗，請確認輸入資料");
        }
    }

    [Authorize(Roles = "Admin")]
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
            _logger.LogError(ex, "GetAllImagesAdmin failed. CommissionTypeId: {CommissionTypeId}", commissionTypeId);
            return BadRequest("查詢圖片失敗，請確認輸入資料");
        }
    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    [HttpPost("Upload")]
    [RequestSizeLimit(MaxImageUploadBytes)]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxImageUploadBytes)]
    public async Task<IActionResult> UploadNewImage([FromForm] ImageUploadDTO dto)
    {
        try
        {
            await _service.UploadNewImage(dto);
            return Ok(new {Message = "圖片上傳成功" });
        }
        catch(KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "UploadNewImage category not found. CommissionTypeId: {CommissionTypeId}", dto.CommissionTypeId);
            return BadRequest("圖片上傳失敗，請確認輸入資料");
        }
        catch(ArgumentException ex)
        {
            _logger.LogWarning(ex, "UploadNewImage invalid argument. CommissionTypeId: {CommissionTypeId}", dto.CommissionTypeId);
            return BadRequest("圖片上傳失敗，請確認輸入資料");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UploadNewImage failed. CommissionTypeId: {CommissionTypeId}", dto.CommissionTypeId);
            return StatusCode(500, "圖片上傳失敗，請稍後再試");
        }
    }
}
