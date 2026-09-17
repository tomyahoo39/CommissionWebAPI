using CommissionManagement.DTO.LoginDTO;

namespace CommissionManagement.Services.UserSer
{
    public interface IUserService
    {
        Task<string> Login(LoginDTO login);
    }
}
