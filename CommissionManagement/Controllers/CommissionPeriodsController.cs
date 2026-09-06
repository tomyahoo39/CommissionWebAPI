using CommissionManagement.DTO.CommissionPeriodDTO;
using CommissionManagement.Models;
using CommissionManagement.Services.CommissionPeriodSer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CommissionPeriodsController : ControllerBase
{
    private readonly ICommissionPeriodService _service;
    public CommissionPeriodsController(ICommissionPeriodService service)
    {
        _service = service;
    }

    [HttpGet("first")]
    public async Task<ActionResult<PeriodDTO>> GetFirstPeriod()
    {
        var period = await _service.GetFirstPeriod();
        return Ok(period);
    }

    [HttpGet("AllPeriods")]
    public async Task<ActionResult<IEnumerable<AllPeriodDTO>>> GetAllPeriods()
    {
        var periods = await _service.AllPeriods();
        return Ok(periods);
    }

    [HttpPost("NewPeriod")]
    public async Task<IActionResult> CreatePeriod([FromBody] CreatePeriodDTO createPeriodDTO)
    {
        if(createPeriodDTO == null)
        {
            return BadRequest("請輸入委託期資料");
        }
        await _service.CreatePeriod(createPeriodDTO);
        return Ok();
    }

}
