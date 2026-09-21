using CommissionManagement.DTO.LoginDTO;
using CommissionManagement.Models;
using CommissionManagement.Services.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CommissionManagement.Services.UserSer
{
    public class UserService : IUserService
    {
        private static readonly TimeSpan LoginLockoutWindow = TimeSpan.FromMinutes(10);
        private const int LoginMaxFailedAttempts = 5;

        private readonly CommissionContext _context;
        private readonly IConfiguration _configuration;
        private readonly IRequestFloodGuardService _floodGuard;

        public UserService(CommissionContext context, IConfiguration configuration, IRequestFloodGuardService floodGuard)
        {
            _context = context;
            _configuration = configuration;
            _floodGuard = floodGuard;
        }

        public async Task<string?> Login(LoginDTO login, string clientIp)
        {
            var username = login.Username?.Trim() ?? string.Empty;

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
            if(user == null)
            {
                return null;
            }

            var failCountKey = $"login:failcount:{clientIp}:{username}";
            var lockKey = $"login:lock:{clientIp}:{username}";

            if (_floodGuard.TryGet<bool>(lockKey, out var isLocked) && isLocked)
            {
                throw new InvalidOperationException("登入失敗次數過多，請 10 分鐘後再試");
            }


            if(!BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                _floodGuard.TryGet<int>(failCountKey, out var failedCount);
                failedCount++;
                _floodGuard.Set(failCountKey, failedCount, LoginLockoutWindow);

                if (failedCount >= LoginMaxFailedAttempts)
                {
                    _floodGuard.Set(lockKey, true, LoginLockoutWindow);
                    _floodGuard.Remove(failCountKey);
                    throw new InvalidOperationException("密碼錯誤次數過多，帳號已暫時鎖定，請 10 分鐘後再試");
                }

                return null;
            }

            _floodGuard.Remove(failCountKey);
            _floodGuard.Remove(lockKey);

            return GenerateJwtToken(user.Username, user.Role);
        }

        private string GenerateJwtToken(string username,string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Key"];

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokens = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience : jwtSettings["Audience"],
                claims:claims,
                expires:DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokens);
        }

    }
}
