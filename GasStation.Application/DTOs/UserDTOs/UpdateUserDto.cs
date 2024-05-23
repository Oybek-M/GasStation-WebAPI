using GasStation.Domain.Entities;
using GasStation.Domain.Enums;

namespace GasStation.Application.DTOs.UserDTOs;

public class UpdateUserDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string CarNumber { get; set; } = string.Empty;
    public UserRole Role {  get; set; }


    public static implicit operator User(UpdateUserDto updateUserDto)
    {
        return new User()
        {
            FullName = updateUserDto.FullName,
            Email = updateUserDto.Email,
            Password = updateUserDto.Password,
            CarNumber = updateUserDto.CarNumber,
            Role = updateUserDto.Role
        };
    }
}
