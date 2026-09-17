using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommissionManagement.Models;
using CommissionManagement.Services.UserSer;
using CommissionManagement.DTO.LoginDTO;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO login)
    {
        var token = await _service.Login(login);
        if(token == null)
        {
            return Unauthorized(new {message = "帳號密碼錯誤"});
        }

        return Ok(new { token });
    }

}
