using GasStation.Application.DTOs.UserDTOs;

namespace GasStation.Application.Interfaces;

public interface IAccountService
{
    Task<bool> RegisterAsync(AddUserDto addUserDto);
    Task<string> LoginAsync(LoginDto loginDto);
    Task SendCodeAsync(string email);
    Task<bool> CheckCodeAsync(string email, string code);
}
