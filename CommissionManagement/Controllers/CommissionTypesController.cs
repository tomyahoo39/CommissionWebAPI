using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommissionManagement.Models;
using CommissionManagement.Services.CommissionTypeSer;
using CommissionManagement.DTO.CommissionTypeDTO;

[Route("api/[controller]")]
[ApiController]
public class CommissionTypesController : ControllerBase
{
    private readonly ICommissionTypeService _service;
    public CommissionTypesController(ICommissionTypeService service)
    {
        _service = service;
    }

    [HttpGet("ActiveType")]
    public async Task<ActionResult<IEnumerable<ActiveTypeDTO>>> ShowActiveType()
    {
        var type = await _service.GetActiveType();
        if(type == null)
        {
            return NotFound();
        }
        return Ok(type);
    }

    [HttpGet("Type")]
    public async Task<ActionResult<IEnumerable<ShowTypeDTO>>> ShowAllTypes()
    {
        var type = await _service.ShowAllType();
        if(type == null)
        {
            return NotFound();
        }
        return Ok(type);
    }

    [HttpGet("IndexType")]
    public async Task<ActionResult<IEnumerable<IndexTypeDTO>>> IndexTypes()
    {
        var type = await _service.IndexType();
        if(type == null)
        {
            return NotFound();
        }
        return Ok(type);
    }

    [HttpPost("NewType")]
    public async Task<IActionResult> CreateType([FromBody] CreateTypeDTO createDto)
    {
        if(createDto == null)
        {
            return BadRequest();
        }
        await _service.Create(createDto);
        return Ok();
    }

    [HttpPut("UpdateType/{id}")]
    public async Task<IActionResult> UpdateType(int id, [FromBody] UpdateTypeDTO updateDto)
    {
        if (updateDto == null)
        {
            return BadRequest("請輸入委託類型");
        }

        try
        {
            var result = await _service.UpdateType(id, updateDto);

            if (result == false)
            {
                return NotFound("委託類型未找到");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"更新委託類型時發生錯誤: {ex.Message}");
        }
    }
}
