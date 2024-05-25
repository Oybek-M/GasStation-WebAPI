using GasStation.Application.DTOs.UserDTOs;
using GasStation.Domain.Entities;

namespace GasStation.Application.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(int id);
    Task<UserDto> GetByEmailAsync(string email);
    Task UpdateAsync(int updaterId, int targetId, UpdateUserDto updateUserDto);
    Task DeleteAsync(int deleterId, int targetId);
}
