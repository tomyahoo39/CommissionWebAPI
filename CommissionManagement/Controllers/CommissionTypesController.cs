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
    public async Task<IActionResult> UpdateType(int id, [FromBody] CreateTypeDTO updateDto)
    {
        if(updateDto == null)
        {
            return BadRequest("請輸入委託類型");
        }

        var result = await _service.UpdateType(id, updateDto);

        if(result == false)
        {
            return NotFound("委託類型未找到");
        }
        return Ok(result);
    }

    [HttpDelete("DeleteType/{id}")]
    public async Task<IActionResult> DeleteType(int id)
    {
        var result = await _service.DeleteType(id);
        if (result == false)
        {
            return NotFound("委託類型未找到");
        }
        return Ok(result);
    }

}
