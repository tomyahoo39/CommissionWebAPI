using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommissionManagement.Models;
using CommissionManagement.Services.CommissionOrderSer;
using CommissionManagement.DTO.CommissionOrderDTO;

[Route("api/[controller]")]
[ApiController]
public class CommissionOrdersController : ControllerBase
{
    private readonly ICommissionOrderService _service;
    public CommissionOrdersController(ICommissionOrderService service)
    {
        _service = service;
    }

    [HttpPost("draw")]
    public async Task<ActionResult<DrawResultDTO>> DrawOrders([FromBody] DrawDTO drawDto)
    {
        if (drawDto.DrawCount <= 0)
        {
            return BadRequest("抽籤數量必須大於0");
        }

        try
        {
            var reuslt = await _service.DrawOrdersAsync(drawDto);
            return Ok(reuslt);
        }
        catch (Exception ex)
        {
            {
                return StatusCode(500, $"抽籤過程中發生錯誤: {ex.Message}");
            }

        }
    }


    [HttpPut("Date/{id}")]
    public async Task<IActionResult> UpdateOrderDate(int id, [FromBody] OrderUpdateDTO updateDto)
    {
        if (updateDto == null)
        {
            return BadRequest("更新資料不能為空");
        }

        var result = await _service.UpdateOrder(id, updateDto);
        if (result == false)
        {
            return BadRequest($"訂單輸入狀態錯誤");
        }

        return Ok();
    }

    [HttpGet("Orders/{periodId}")]
    public async Task<ActionResult<IEnumerable<ShowAllOrder>>> ShowOrderAdmin(int periodId)
    {
        var orders = await _service.ShowOrderAdmin(periodId);
        if (orders == null || !orders.Any())
        {
            return NotFound($"找不到 PeriodId 為 {periodId} 的委託單");
        }
        return Ok(orders);
    }

    [HttpGet("Guest")]
    public async Task<ActionResult<IEnumerable<ShowOrderGuest>>> ShowOrderGuest()
    {
        try
        {
            var orders = await _service.ShowOrderGuest();
            if (orders == null || !orders.Any())
            {
                return NotFound("找不到任何委託單");
            }
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"查詢過程中發生錯誤: {ex.Message}");
        }
    }

    [HttpPost("NewOrder")]
    public async Task<IActionResult> CreateNewOrder([FromBody] CreateOrderDTO createOrderDTO)
    {
        if (createOrderDTO == null)
        {
            return BadRequest("委託單資料不能為空");
        }
        try
        {
            await _service.CreateNewOrder(createOrderDTO);
            return Ok("委託單建立成功");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"建立委託單過程中發生錯誤: {ex.Message}");
        }
    }
}
